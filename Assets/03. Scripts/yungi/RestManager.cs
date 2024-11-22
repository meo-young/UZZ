using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using DG.Tweening;

public class RestManager : MonoBehaviour
{
    [SerializeField] private GameObject ui_rest_panel;
    //[SerializeField] private GameObject ui_rest_background1;
    //[SerializeField] private GameObject ui_rest_background2;

    //private int presentCharacterID;
    private GardenerManager gardenerManager;
    

    public string[] rest_names_data;        //엑셀 파일에서 읽어온 이름 데이터
    public string[] rest_description_data;  //엑셀 파일에서 읽어온 설명 데이터
    public float[] rest_leadTime_data;      //엑셀 파일에서 읽어온 소요 시간 데이터

    private string rest_name_system;        //시스템에서 픽스한 휴식의 이름
    private float rest_leadTime_system;     //시스템에서 픽스한 휴식의 소요 시간
    private float accumulatedTime_system;   //시간 측정에 쓰이는 변수

    private int selectNum;
    private bool isRestSystemON;            //휴식 시스템이 활성화중인가?

    [SerializeField] private TextMeshProUGUI text_RestName;
    [SerializeField] private TextMeshProUGUI text_RestDescription;

    [SerializeField] private GameObject rewardsButton;
    [SerializeField] private GameObject startButton;


    [SerializeField] private GameObject timeLeft_gameObject;
    private TextMeshProUGUI timeLeft;

    void Start()
    {
        ui_rest_panel.SetActive(false);
        //ui_rest_background1.SetActive(false);
        //ui_rest_background2.SetActive(false);

        //presentCharacterID = 0;

        selectNum = 0;

        timeLeft = timeLeft_gameObject.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        /*
        if (isRestSystemON) 
        {
            accumulatedTime_system += Time.deltaTime;

            if (accumulatedTime_system > rest_leadTime_system)
            {
                Debug.Log("휴식 끝~" + rest_name_system + "가 끝났습니다.");
                reset_RestSystem();
                onRewardsButton();
                
            }
        }
        */
        
    }

    public void set_RestSystem(GardenerManager _gardenerManager)
    {
        gardenerManager = _gardenerManager;
        onRestUI();
    }

    public void onRestUI()
    {
        ui_rest_panel.SetActive(true);

        if (isRestSystemON) { timeLeft_gameObject.SetActive(true); }
        //ui_rest_background1.SetActive(true);
    }

    public void offRestUI()
    {
        ui_rest_panel.SetActive(false);
        timeLeft_gameObject.SetActive(false);
        //ui_rest_background1.SetActive(false);
    }

    public void touchButton(int num)
    {
        if (gardenerManager.getGardnerState() == GardenerManager.State.Idle)
        {
            //ui_rest_background2.SetActive(true);
            selectNum = num;
            //text_RestName.text = rest_names_data[selectNum];
            //text_RestDescription.text = rest_description_data[selectNum];

            start_RestSystem();
        }
    
    }

    public void touchSuperButton(int num)
    {
        if(gardenerManager.getGardnerState() == GardenerManager.State.Rest && isRestSystemON == true)
        {
            StopCoroutine("restSystem");
            rest_leadTime_system = 0f;

        }
    }


    public void start_RestSystem(bool isSuper = false)
    {
        //ui_rest_panel.SetActive(false);
        //ui_rest_background1.SetActive(false);

        //ui_rest_background2.SetActive(false);

        rest_name_system = rest_names_data[selectNum];
        rest_leadTime_system = rest_leadTime_data[selectNum];

        if (isSuper) { rest_leadTime_system = 0f; }

        isRestSystemON = true;
        StartCoroutine(restSystem());
        gardenerManager.setGardnerState(GardenerManager.State.Rest);

        startButton.GetComponent<Image>().color = Color.gray;
        startButton.GetComponent<Button>().enabled = false;

        timeLeft_gameObject.SetActive(true);
    }

    private void reset_RestSystem()
    {
        isRestSystemON = false;
        accumulatedTime_system = 0f;

        if (gardenerManager != null) { gardenerManager.setGardnerState(GardenerManager.State.Idle); }

        timeLeft_gameObject.SetActive(false);
        
    }

    public void onRewardsButton()
    {
        rewardsButton.SetActive(true);
        startButton.SetActive(false);
    }

    public void offRewardsButton()
    {
        rewardsButton.SetActive(false);
        startButton.SetActive(true);
        startButton.GetComponent<Image>().color = Color.white;
        startButton.GetComponent<Button>().enabled = true;
    }

    private IEnumerator restSystem()
    {
        int minutes = (int)rest_leadTime_system / 60;
        int seconds = (int)rest_leadTime_system % 60;
        string min = minutes.ToString();
        string sec = seconds.ToString();
        if (minutes < 10) { min = "0" + minutes; }
        if (seconds < 10) { sec = "0" + seconds; }
        timeLeft.text = min + ":" + sec;

        while (rest_leadTime_system > 0f)
        {
            yield return new WaitForSeconds(1.0f);
            rest_leadTime_system--;
            minutes = (int)rest_leadTime_system / 60;
            seconds = (int)rest_leadTime_system % 60;
            min = minutes.ToString();
            sec = seconds.ToString();
            if (minutes < 10) { min = "0" + minutes; }
            if (seconds < 10) { sec = "0" + seconds; }
            timeLeft.text = min + ":" + sec;

        }
        reset_RestSystem();
        onRewardsButton();
    }
}