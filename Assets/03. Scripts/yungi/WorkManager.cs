using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class WorkManager : MonoBehaviour
{
    [SerializeField] private GameObject ui_work_panel;
    [SerializeField] private GameObject ui_work_background1;
    [SerializeField] private GameObject ui_work_background2;

    //private int presentCharacterID;
    private GardenerManager gardenerManager;
    

    public string[] work_names_data;        //엑셀 파일에서 읽어온 이름 데이터
    public string[] work_description_data;  //엑셀 파일에서 읽어온 설명 데이터
    public float[]  work_leadTime_data;      //엑셀 파일에서 읽어온 소요 시간 데이터

    private string work_name_system;        //시스템에서 픽스한 휴식의 이름
    private float  work_leadTime_system;     //시스템에서 픽스한 휴식의 소요 시간
    private float accumulatedTime_system;   //시간 측정에 쓰이는 변수

    private int selectNum;
    private bool isWorkSystemON;            //휴식 시스템이 활성화중인가?
    private bool needToReceiveRewards;      

    [SerializeField] private TextMeshProUGUI text_WorkName;
    [SerializeField] private TextMeshProUGUI text_WorkDescription;

    [SerializeField] private GameObject goWindow;

    [SerializeField] private GameObject[] startButtons;
    [SerializeField] private GameObject[] rewardsButtons;

    [SerializeField] private GameObject timeLeft_gameObject;
    private TextMeshProUGUI timeLeft;

    [SerializeField] private TextMeshProUGUI[] timeLeft_Buttons;

    [Space (10f)]
    [SerializeField] private GoodsManager goodsManager;


    void Start()
    {
        ui_work_panel.SetActive(false);
        ui_work_background1.SetActive(false);
        ui_work_background2.SetActive(false);


        //presentCharacterID = 0;
        selectNum = 0;

        timeLeft = timeLeft_gameObject.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        /*
        if (isWorkSystemON) 
        {
            accumulatedTime_system += Time.deltaTime;

            if (accumulatedTime_system > work_leadTime_system)
            {
                Debug.Log("작업 끝~" + work_name_system + "가 끝났습니다.");
                
                
            }
        }
        */
    }


    public void onWorkUI()
    {
        ui_work_panel.SetActive(true);
        ui_work_background1.SetActive(true);

        goWindow.SetActive(false);

        timeLeft_gameObject.SetActive(false);
    }

    public void offWorkUI()
    {
        ui_work_panel.SetActive(false);
        ui_work_background1.SetActive(false);
        ui_work_background2.SetActive(false);

        goWindow.SetActive(true);
        if (isWorkSystemON) { timeLeft_gameObject.SetActive(true); }
        
    }

    public void touchStartButton(int index)
    {
        if (isWorkSystemON == false && needToReceiveRewards == false && gardenerManager.getGardnerState() == GardenerManager.State.Idle)
        {
            //ui_work_background2.SetActive(true);
            selectNum = index;

            //text_WorkName.text = work_names_data[selectNum];
            //text_WorkDescription.text = work_description_data[selectNum];

            start_WorkSystem();
        }
    }

    public void start_WorkSystem()
    {
        work_name_system = work_names_data[selectNum];
        work_leadTime_system = work_leadTime_data[selectNum];

        
        StartCoroutine(workSystem(work_leadTime_system));
        isWorkSystemON = true;
        gardenerManager.setGardnerState(GardenerManager.State.Work);

        startButtons[selectNum].GetComponent<Image>().color = Color.gray;
        startButtons[selectNum].GetComponent<Button>().enabled = false;
        timeLeft_Buttons[selectNum].gameObject.SetActive(true);
    }

    private void reset_WorkSystem()
    {
        isWorkSystemON = false;
        needToReceiveRewards = true;
        accumulatedTime_system = 0f;

        timeLeft_gameObject.SetActive(false);
        timeLeft_Buttons[selectNum].gameObject.SetActive(false);
        timeLeft_Buttons[selectNum].text = "";
        onRewardsButton();
        if (gardenerManager != null) { gardenerManager.setGardnerState(GardenerManager.State.Idle); }
    }

    public void selectCharacter(GameObject gameObject)
    {
        gardenerManager = gameObject.GetComponent<GardenerManager>();
        ui_work_background2.SetActive(true);
    }

    public void onRewardsButton()
    {
        startButtons[selectNum].SetActive(false);
        rewardsButtons[selectNum].SetActive(true);
        
    }

    public void offRewardsButton()
    {
        rewardsButtons[selectNum].SetActive(false);

        startButtons[selectNum].SetActive(true);
        startButtons[selectNum].GetComponent<Image>().color = Color.white;
        startButtons[selectNum].GetComponent<Button>().enabled = true;

        needToReceiveRewards = false;
        timeLeft_gameObject.SetActive(false);
    }

    private IEnumerator workSystem(float leadtime)
    {
        int minutes = (int)leadtime / 60;
        int seconds = (int)leadtime % 60;
        string min = minutes.ToString();
        string sec = seconds.ToString();
        if (minutes < 10) { min = "0" + minutes; }
        if (seconds < 10) { sec = "0" + seconds; }
        timeLeft.text = min + ":" + sec;
        timeLeft_Buttons[selectNum].text = min + ":" + sec;

        while (leadtime > 0f) 
        {
            yield return new WaitForSeconds(1.0f);
            leadtime--;
            minutes = (int)leadtime / 60;
            seconds = (int)leadtime % 60;
            min = minutes.ToString(); 
            sec = seconds.ToString();
            if (minutes < 10) { min = "0" + minutes; }
            if (seconds < 10) { sec = "0" + seconds; }
            timeLeft.text = min + ":" + sec;
            timeLeft_Buttons[selectNum].text = min + ":" + sec;

        }
        reset_WorkSystem();
    }

    public bool isOnWorkSystem()
    {
        return isWorkSystemON;
    }
}