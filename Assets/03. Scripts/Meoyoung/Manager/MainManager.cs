using System;
using UnityEngine;
using static Constant;

[System.Serializable]
public class GameInfo
{
    public int totalDayCounter;
    public float playTime;
    public float inGameTime;
    public TimeType timeType;
    public float likeabilityTimer;
    public bool likeabilityFlag;
    public string lastConnectTime;
    public float dew;
    public bool cycleFlag;
    public bool showerFlag;
    public float showerTimer;
    public bool mealFlag;
    public float mealTimer;
}


public class MainManager : MonoBehaviour
{
    public static MainManager instance;

    [Header("# Goods")]
    [HideInInspector] public DewUI dewUI;

    [Header("# Pure")]
    [HideInInspector] public GameObject pure;
    [HideInInspector] public PureStat pureStat;
    [HideInInspector] public PureAnimationSet pureAnimationSet;
    [HideInInspector] public PureController pureController;
    [HideInInspector] public PureInteractionText pureInteractionText;
    [HideInInspector] public SpeechBubbleSet speechBubbleSet;
    [HideInInspector] public AutoText autoText;

    [Header("# Present")]
    [HideInInspector] public PresentManager presentManager;
    [HideInInspector] public PresentUI presentUI;

    [Header("# FieldWork")]
    [HideInInspector] public FieldWorkManager fieldWorkManager;

    [Header("# Flower")]
    [HideInInspector] public FlowerManager flowerManager;
    [HideInInspector] public FlowerUI flowerUI;

    [Header("# VFX")]
    [HideInInspector] public VFXManager vfxManager;



    [Header("# Database GameInfo")]
    public GameInfo gameInfo;
    public GameObject hungry;



    private LightColorController lightController;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        if (instance == null)
            instance = this;

        #region init
        pure =                  GameObject.FindWithTag("Player");
        pureStat =              FindFirstObjectByType<PureStat>();
        pureAnimationSet =      FindFirstObjectByType<PureAnimationSet>();
        pureController =        FindFirstObjectByType<PureController>();
        speechBubbleSet =       FindFirstObjectByType<SpeechBubbleSet>();
        pureInteractionText =   FindFirstObjectByType<PureInteractionText>();
        autoText =              FindFirstObjectByType<AutoText>();

        presentManager =        FindFirstObjectByType<PresentManager>();
        presentUI =             FindFirstObjectByType<PresentUI>();

        fieldWorkManager =      FindFirstObjectByType<FieldWorkManager>();

        vfxManager =            FindFirstObjectByType<VFXManager>();

        flowerManager =         FindFirstObjectByType<FlowerManager>();
        flowerUI =              FindFirstObjectByType<FlowerUI>();

        lightController =       FindFirstObjectByType<LightColorController>();

        dewUI =                 FindFirstObjectByType<DewUI>();

        #endregion
    }

    private void Start()
    {
        if (GetDateDifference())
            gameInfo.totalDayCounter++;

        hungry.SetActive(gameInfo.mealFlag);
    }

    private void Update()
    {
        UpdatePlayTime();
        UpdateInGameTime();
        UpdateLikeabilityTimer();
        UpdateShowerTimer();
        UpdateMealTimer();
    }

    void UpdateMealTimer()
    {
        if (gameInfo.mealFlag)
            return;

        gameInfo.mealTimer += Time.deltaTime;

        if (gameInfo.mealTimer > PURE_MEAL_TIME)
        {
            gameInfo.mealFlag = true;
            gameInfo.mealTimer = 0;
            hungry.SetActive(true);
        }
    }

    void UpdateLikeabilityTimer()
    {
        if (!gameInfo.likeabilityFlag)
            return;

        gameInfo.likeabilityTimer += Time.deltaTime;
        if (gameInfo.likeabilityTimer > INTERACTION_LIKEABILITY_COOLTIME)
        {
            gameInfo.likeabilityFlag = false;
            gameInfo.likeabilityTimer = 0;
        }
    }

    void UpdateShowerTimer()
    {
        if (gameInfo.showerFlag)
            return;

        gameInfo.showerTimer += Time.deltaTime;
        if (gameInfo.showerTimer > PURE_SHOWER_TIME)
        {
            Debug.Log("푸르 샤워시간 됨");
            gameInfo.showerFlag = true;
            gameInfo.showerTimer = 0;
        }

    }

    void UpdatePlayTime()
    {
        gameInfo.playTime += Time.unscaledDeltaTime;
    }

    void UpdateInGameTime()
    {
        gameInfo.inGameTime += Time.unscaledDeltaTime * INGAME_TIME_MULTIFLIER;

        if (gameInfo.inGameTime >= INGAME_ONEDAYSECONDS)
            gameInfo.inGameTime %= INGAME_ONEDAYSECONDS;

        if (gameInfo.inGameTime >= 3600 * 6 && gameInfo.inGameTime < 3600 * 11)
            gameInfo.timeType = TimeType.Morning;
        else if (gameInfo.inGameTime >= 3600 * 11 && gameInfo.inGameTime < 3600 * 16)
            gameInfo.timeType = TimeType.Afternoon;
        else if (gameInfo.inGameTime >= 3600 * 16 && gameInfo.inGameTime < 3600 * 20)
            gameInfo.timeType = TimeType.Night;
        else
            gameInfo.timeType = TimeType.Evening;

        lightController.time = gameInfo.inGameTime / INGAME_ONEDAYSECONDS;

    }

    bool GetDateDifference()
    {
        //Debug.Log(MainManager.instance.gameInfo.lastConnectTime);
        if (MainManager.instance.gameInfo.lastConnectTime == "" || MainManager.instance.gameInfo.lastConnectTime == "0")
            return true;

        string lastAccessTime = DateTime.Parse(MainManager.instance.gameInfo.lastConnectTime).ToString("yyyy-MM-dd");
        string currentAccessTime = DateTime.Now.ToString("yyyy-MM-dd");

        if (lastAccessTime != currentAccessTime)
            return true;

        return false;
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            Debug.Log("앱이 백그라운드로 전환됨");
            // 비동기적으로 게임 데이터 저장
            SaveGameDataAsync();
        }
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            DataManager.instance.JsonLoadAsync();
            AutoGrowManager.instance.GetOfflineGrowth(Utility.instance.GetIntervalDateTime());
            Debug.Log("어플 포커스");
        }
    }

    private void OnApplicationQuit()
    {
        Debug.Log("앱 종료");
        // 비동기적으로 게임 데이터 저장
        SaveGameDataAsync();
    }

    private void SaveGameDataAsync()
    {
        if (gameInfo != null)
        {
            gameInfo.lastConnectTime = DateTime.Now.ToString();
            DataManager.instance.JsonSaveAsync();  // 비동기 저장 메서드 호출
            Debug.Log("데이터 저장 완료");
        }
    }
}

public enum TimeType
{
    Morning = 0,
    Afternoon = 1,
    Night = 2,
    Evening = 3
}