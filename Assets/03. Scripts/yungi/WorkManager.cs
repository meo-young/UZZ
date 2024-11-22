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
    

    public string[] work_names_data;        //���� ���Ͽ��� �о�� �̸� ������
    public string[] work_description_data;  //���� ���Ͽ��� �о�� ���� ������
    public float[]  work_leadTime_data;      //���� ���Ͽ��� �о�� �ҿ� �ð� ������

    private string work_name_system;        //�ý��ۿ��� �Ƚ��� �޽��� �̸�
    private float  work_leadTime_system;     //�ý��ۿ��� �Ƚ��� �޽��� �ҿ� �ð�
    private float accumulatedTime_system;   //�ð� ������ ���̴� ����

    private int selectNum;
    private bool isWorkSystemON;            //�޽� �ý����� Ȱ��ȭ���ΰ�?
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
                Debug.Log("�۾� ��~" + work_name_system + "�� �������ϴ�.");
                
                
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