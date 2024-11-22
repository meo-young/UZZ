using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

using UnityEngine.UI;



public class CameraController : MonoBehaviour
{ 
    public List<Transform> CameraPos;
    public int PageNumber;
    public bool isWindow;
    public bool isFurniture;
    public Transform targetPostion;
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    public float smoothSpeed = 0.125f; // 부드러운 이동을 조절하는 값
    public GameObject UI_panel;
    public GameObject UI_goWindow;
    public GameObject UI_goGarret;
    [SerializeField] private GameObject UI_Start_UnderBar;
    [SerializeField] private GameObject UI_Start_SideBar;
    [SerializeField] private TMP_Text location_background_Text;

    [SerializeField] private GameObject atticPanel;

    public LightColorController lightColorController;

    private bool isRestricted;
    private bool validStartTouch;

    private int pointerID = 0;

    private bool isShop = false;

    [SerializeField] private GardenerManager gardenerManager;
    [SerializeField] private SpiritManager spiritManager;
    private bool[] bools = new bool[4];

    [SerializeField] private GameObject timeLeft_gameObject;
    [SerializeField] private WorkManager workManager;

    [Space(10f)]
    public GameObject miniGamePossible;
    [SerializeField] private ADManager adManager;

    public GameObject miniGamePossible_work;
    public GameObject miniGamePossible_ogurae;

    [SerializeField] private RewardManager rewardManager;


    [SerializeField] private GameObject UI_Furniture;
    public bool isStory = false;

    [SerializeField] private GameObject ather;
    [SerializeField] private GameObject honyange;
    [SerializeField] private GameObject peri;
    [SerializeField] private GameObject osudal;

    private void Start()
    {
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
        pointerID = -1;
#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
        pointerID = 0;
#endif
    }
    public void OffUnderBar()
    {
        UI_Start_UnderBar.SetActive(false);
    }
    public void OnUnderBar()
    {
        UI_Start_UnderBar.SetActive(true);
    }
    private void Update()
    {
        if(!isStory)
        {
            transform.position = Vector3.Lerp(transform.position, targetPostion.position, smoothSpeed * Time.deltaTime);

        }


        /*
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Right();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Left();
        }
        */

        if (Input.touchCount > 0 && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

            //해당 좌표에 있는 오브젝트 찾기
            RaycastHit hit;
            Physics.Raycast(pos, transform.forward, out hit, 100f);

            if (hit.collider == null && (isRestricted == false) && (EventSystem.current.IsPointerOverGameObject(pointerID) == false)) { startTouchPosition = Input.GetTouch(0).position; validStartTouch = true; }
            else { validStartTouch = false; }
        }
        else if (validStartTouch && Input.touchCount > 0 && (Input.GetTouch(0).phase == TouchPhase.Ended))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

            //해당 좌표에 있는 오브젝트 찾기
            RaycastHit hit;
            Physics.Raycast(pos, transform.forward, out hit, 100f);

            if (hit.collider == null && (isRestricted == false) && (EventSystem.current.IsPointerOverGameObject(pointerID) == false))
            {
                endTouchPosition = Input.GetTouch(0).position;

                if (endTouchPosition.x < startTouchPosition.x && Vector2.Distance(startTouchPosition, endTouchPosition) > 300f)
                {
                    Right();
                }

                if (endTouchPosition.x > startTouchPosition.x && Vector2.Distance(startTouchPosition, endTouchPosition) > 300f)
                {
                    Left();
                }
            }
        }


        //SetUIWindow();
        set_GardenerFirstTalk();
    }


    void SetBGMWindow()
    {
        if(isShop)
        {
            if (AudioManager.instance.bgmPlayer.clip != null && AudioManager.instance.bgmPlayer.clip.name == "store")
                return;
            AudioManager.instance.FadeOutBgm();
            StartCoroutine(PlayBgmDelayed(AudioManager.Bgm.store, 1f));
        }
         else if (lightColorController.time >= 1 * (5 / 24f) && lightColorController.time < 1 * (11 / 24f))
         {
             if (AudioManager.instance.bgmPlayer.clip != null && AudioManager.instance.bgmPlayer.clip.name == "morning")
                 return;
             AudioManager.instance.FadeOutBgm();
             StartCoroutine(PlayBgmDelayed(AudioManager.Bgm.morning, 1f));
            
         }
         else if (lightColorController.time >= 1 * (11 / 24f) && lightColorController.time < 1 * (16 / 24f))
         {
             if (AudioManager.instance.bgmPlayer.clip != null && AudioManager.instance.bgmPlayer.clip.name == "afternoon")
                 return;
            AudioManager.instance.FadeOutBgm();
            StartCoroutine(PlayBgmDelayed(AudioManager.Bgm.afternoon, 1f));
        }
         else if (lightColorController.time >= 1 * (16 / 24f) && lightColorController.time < 1 * (20 / 24f))
         {
             if (AudioManager.instance.bgmPlayer.clip != null && AudioManager.instance.bgmPlayer.clip.name == "evening")
                 return;
            AudioManager.instance.FadeOutBgm();

            StartCoroutine(PlayBgmDelayed(AudioManager.Bgm.evening, 1f));
        }
         else if (lightColorController.time >= 1 * (20 / 24f) || lightColorController.time < 1 * (5 / 24f))
         {
             if (AudioManager.instance.bgmPlayer.clip != null && AudioManager.instance.bgmPlayer.clip.name == "night")
                 return;
            AudioManager.instance.FadeOutBgm();

            StartCoroutine(PlayBgmDelayed(AudioManager.Bgm.night, 1f));
        }

        
    }
    IEnumerator PlayBgmDelayed(AudioManager.Bgm bgm, float delay)
    {
        yield return new WaitForSeconds(delay); // 지정된 시간(초)만큼 대기합니다.
        AudioManager.instance.PlayBgm(bgm); // BGM을 재생합니다.
    }

    void SetUIWindow()
    {

        if (lightColorController.time >= 1 * (5 / 24f) && lightColorController.time < 1 * (13 / 24f))
        {

          
            
        }
        else if (lightColorController.time >= 1 * (16 / 24f) && lightColorController.time < 1 * (19 / 24f))
        {
 
          
            
        }
        else if (lightColorController.time >= 1 * (19 / 24f) || lightColorController.time < 1 * (5 / 24f))
        {
  

           
            
        }

    }

    void set_GardenerFirstTalk()
    {
        //if(lightColorController.time >= 1 * (7 / 24f) && lightColorController.time < 1 * (9 / 24f))
        //{
        //    gardenerManager.setGardnerState(GardenerManager.State.Greet);

        //}
        //else if(lightColorController.time >= 1 * (22 / 24f) || lightColorController.time < 1 * (24 / 24f))
        //{
        //    gardenerManager.setGardnerState(GardenerManager.State.Greet);
        //}
        
        if (lightColorController.time >= 1 * (5 / 24f) && lightColorController.time < 1 * (11 / 24f) && bools[0] == false)
        {
            ather.SetActive(true);
            honyange.SetActive(false);
            peri.SetActive(false);
            osudal.SetActive(false);
            Debug.Log("아침");
            bools[0] = true;
            bools[1] = false;
            bools[2] = false;
            bools[3] = false;
            gardenerManager.set_FirstConversation(true, 0);
           
          
        }
        else if (lightColorController.time >= 1 * (11 / 24f) && lightColorController.time < 1 * (16 / 24f) && bools[1] == false)
        {

            ather.SetActive(false);
            honyange.SetActive(true);
            peri.SetActive(false);
            osudal.SetActive(false);
            Debug.Log("점심");
            bools[0] = false;
            bools[1] = true;
            bools[2] = false;
            bools[3] = false;
            gardenerManager.set_FirstConversation(true, 1);
            gardenerManager.setGardnerState(GardenerManager.State.Idle);
            

        }
        else if (lightColorController.time >= 1 * (16 / 24f) && lightColorController.time < 1 * (20 / 24f) && bools[2] == false)
        {
            ather.SetActive(false);
            honyange.SetActive(false);
            peri.SetActive(true);
            osudal.SetActive(false);
            Debug.Log("오후");
            bools[0] = false;
            bools[1] = false;
            bools[2] = true;
            bools[3] = false;
            gardenerManager.set_FirstConversation(true, 2);
            gardenerManager.setGardnerState(GardenerManager.State.Idle);
           
        }
        else if (lightColorController.time >= 1 * (20 / 24f) || lightColorController.time < 1 * (5 / 24f) && bools[3] == false)
        {
            ather.SetActive(false);
            honyange.SetActive(false);
            peri.SetActive(false);
            osudal.SetActive(true);
            if (bools[3] == false)
            {
                Debug.Log("밤");
                bools[0] = false;
                bools[1] = false;
                bools[2] = false;
                bools[3] = true;
                gardenerManager.set_FirstConversation(true, 3);
                
               
            }
            
        }

    }

    void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        //Screen.SetResolution(1440,2960, true);
        //Invoke("SetBGMWindow", 1f);
        InvokeRepeating("SetBGMWindow", 1.5f, 1.5f);

    }

    private void Right()
    {
        if (!isWindow)
            return;
        if (PageNumber >= 1)
            return;
        if (Camera.main != null)
        {
            // Camera.main이 null이 아닌 경우에만 사용합니다.
            PageNumber++;
            targetPostion.position = CameraPos[PageNumber].position;
        }
        else
        {
            Debug.LogError("메인 카메라가 없습니다.");
        }
    }

    private void Left()
    {
        if (!isWindow)
            return;
        if (PageNumber <= 0)
            return;
        if (Camera.main != null)
        {
            // Camera.main이 null이 아닌 경우에만 사용합니다.
            PageNumber--;
            targetPostion.position = CameraPos[PageNumber].position;

        }
        else
        {
            Debug.LogError("메인 카메라가 없습니다.");
        }
    }

    public void goWorkPlace()
    {

        isWindow = false;
        PageNumber = 5;
        targetPostion.position = CameraPos[PageNumber].position;
        Camera.main.transform.position = CameraPos[PageNumber].position;
        //UI_panel.SetActive(false);
        UI_Start_UnderBar.SetActive(false);
        UI_goWindow.SetActive(true);
        location_background_Text.text = "작업실";
        if (workManager.isOnWorkSystem() == true) { timeLeft_gameObject.SetActive(true); }

        miniGamePossible.SetActive(false);
        miniGamePossible_work.SetActive(true);
      
    }

    public void goGarret()
    {
     

        isWindow = false;
        PageNumber = 6;
        targetPostion.position = CameraPos[PageNumber].position;
        Camera.main.transform.position = CameraPos[PageNumber].position;
        //UI_panel.SetActive(false);
        UI_Start_UnderBar.SetActive(false);
        UI_goWindow.SetActive(true);
        UI_goGarret.SetActive(false);
        atticPanel.SetActive(true);
        location_background_Text.text = "다락방";


        miniGamePossible.SetActive(true);
        miniGamePossible_ogurae.SetActive(false);
    }

   
    public void goWindow()
    {
        

        isWindow = true;
        PageNumber = 0;
        targetPostion.position = CameraPos[PageNumber].position;
        Camera.main.transform.position = CameraPos[PageNumber].position;
        //UI_panel.SetActive(true);
        UI_Start_UnderBar.SetActive(true);
        UI_goWindow.SetActive(false);
        atticPanel.SetActive(false);
        location_background_Text.text = "창가";
        timeLeft_gameObject.SetActive(false);
        miniGamePossible.SetActive(false);
        miniGamePossible_work.SetActive(false);

        UI_Furniture.SetActive(false);
       
    }


    public void goGarret_Outside()
    {
        
        isWindow = false;
        PageNumber = 7;
        targetPostion.position = CameraPos[PageNumber].position;
        Camera.main.transform.position = CameraPos[PageNumber].position;
        //UI_panel.SetActive(false);
        UI_goGarret.SetActive(true);
        UI_goWindow.SetActive(false);
        miniGamePossible.SetActive(false);
        miniGamePossible_ogurae.SetActive(true);
        atticPanel.SetActive(false);

    }

    public void goBook()
    {
       

        isWindow = false;
        PageNumber = 8;
        targetPostion.position = CameraPos[PageNumber].position;
        Camera.main.transform.position = CameraPos[PageNumber].position;
        UI_panel.SetActive(false);
        UI_goWindow.SetActive(true); 
        miniGamePossible.SetActive(false);

    }

    public void goLibrary()
    {
       

        isWindow = false;
        PageNumber = 9;
        targetPostion.position = CameraPos[PageNumber].position;
        Camera.main.transform.position = CameraPos[PageNumber].position;
        UI_panel.SetActive(false);
        UI_goWindow.SetActive(false);
        UI_goGarret.SetActive(false);
        miniGamePossible.SetActive(false);

    }
    public void goTree()
    {
        isWindow = false;
        PageNumber = 10;
        targetPostion.position = CameraPos[PageNumber].position;
        Camera.main.transform.position = CameraPos[PageNumber].position;
        UI_Start_UnderBar.SetActive(false);
        UI_goWindow.SetActive(true);
        UI_goGarret.SetActive(false);
        miniGamePossible.SetActive(false);
        
    }
    public void goShop()
    {
        isWindow = false;
        isShop = !isShop;
        //isWindow = false;
        //PageNumber = 10;
        //targetPostion.position = CameraPos[PageNumber].position;
        //Camera.main.transform.position = CameraPos[PageNumber].position;
        //UI_panel.SetActive(false);
        //UI_goWindow.SetActive(false);
        //UI_goGarret.SetActive(false);
        //miniGamePossible.SetActive(false);
        UI_Furniture.SetActive(false);
    }

    public void goFurniture()
    {
        isWindow = false;
        isFurniture = true;
        PageNumber = 11;
        targetPostion.position = CameraPos[PageNumber].position;
        Camera.main.transform.position = CameraPos[PageNumber].position;
        UI_Start_UnderBar.SetActive(false);
        
        UI_goGarret.SetActive(false);
        miniGamePossible.SetActive(false);
        UI_Furniture.SetActive(true);
    }
    public void setCameraRestriction(bool val)
    {
        isRestricted = val;
    }

    public void goStory()
    {
        isWindow = false;
        isStory = true;
    }

    public void OffUIShop()
    {
        if(isFurniture)
        {
            UI_Furniture.SetActive(true);
        }
        else if(!isFurniture)
        {
            UI_Start_UnderBar.SetActive(true);
            UI_Start_SideBar.SetActive(true);

        }
        
    }

    public void GoQuiz()
    {
        PageNumber = 12;
        targetPostion.position = CameraPos[PageNumber].position;
        Camera.main.transform.position = CameraPos[PageNumber].position;
        atticPanel.SetActive(false);
        UI_goWindow.SetActive(false);
      
    }

    public void GoHonyang()
    {
        atticPanel.SetActive(false);
        UI_goWindow.SetActive(false);
    }

}
