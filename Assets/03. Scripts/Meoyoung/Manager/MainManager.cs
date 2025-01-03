using System;
using UnityEngine;

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

}


public class MainManager : MonoBehaviour
{
    public static MainManager instance;

    [Header("# Game State")]
    public float cycleTime;
    public GameInfo gameInfo;
    [SerializeField] float oneDaySeconds;
    public float timeMultifiler;

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

    [Header("# Likeability")]
    public int interactionLikeability;
    public float likeabilityIntervalTime;


    private LightColorController lightController;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        if (instance == null)
            instance = this;

        #region init
        pure = GameObject.FindWithTag("Player");
        pureStat = FindFirstObjectByType<PureStat>();
        pureAnimationSet = FindFirstObjectByType<PureAnimationSet>();
        pureController = FindFirstObjectByType<PureController>();
        speechBubbleSet = FindFirstObjectByType<SpeechBubbleSet>();
        pureInteractionText = FindFirstObjectByType<PureInteractionText>();
        autoText = FindFirstObjectByType<AutoText>();

        presentManager = FindFirstObjectByType<PresentManager>();
        presentUI = FindFirstObjectByType<PresentUI>();

        fieldWorkManager = FindFirstObjectByType<FieldWorkManager>();

        vfxManager = FindFirstObjectByType<VFXManager>();

        flowerManager = FindFirstObjectByType<FlowerManager>();
        flowerUI = FindFirstObjectByType<FlowerUI>();

        lightController = FindFirstObjectByType<LightColorController>();

        dewUI = FindFirstObjectByType<DewUI>();

        #endregion
    }

    private void Start()
    {
        if (GetDateDifference())
            gameInfo.totalDayCounter++;
    }
    // 호감도 얻은지 얼마나 됐는지 시간 체크
    private void Update()
    {
        UpdatePlayTime();
        UpdateInGameTime();
        if (!gameInfo.likeabilityFlag)
            return;

        gameInfo.likeabilityTimer += Time.deltaTime;
        if (gameInfo.likeabilityTimer > likeabilityIntervalTime)
        {
            gameInfo.likeabilityFlag = false;
            gameInfo.likeabilityTimer = 0;
        }
    }

    void UpdatePlayTime()
    {
        gameInfo.playTime += Time.unscaledDeltaTime;
    }

    void UpdateInGameTime()
    {
        gameInfo.inGameTime += Time.unscaledDeltaTime * timeMultifiler;

        if (gameInfo.inGameTime >= oneDaySeconds)
            gameInfo.inGameTime %= oneDaySeconds;

        if (gameInfo.inGameTime >= 3600 * 6 && gameInfo.inGameTime < 3600 * 11)
            gameInfo.timeType = TimeType.Morning;
        else if (gameInfo.inGameTime >= 3600 * 11 && gameInfo.inGameTime < 3600 * 16)
            gameInfo.timeType = TimeType.Afternoon;
        else if (gameInfo.inGameTime >= 3600 * 16 && gameInfo.inGameTime < 3600 * 20)
            gameInfo.timeType = TimeType.Night;
        else
            gameInfo.timeType = TimeType.Evening;

        lightController.time = gameInfo.inGameTime / oneDaySeconds;

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

    private void OnApplicationQuit()
    {
        gameInfo.lastConnectTime = DateTime.Now.ToString();
        DataManager.instance.JsonSave();
    }
}

public enum TimeType
{
    Morning = 0,
    Afternoon = 1,
    Night = 2,
    Evening = 3
}