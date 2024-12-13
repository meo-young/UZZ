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
    [SerializeField] LightColorController lightController;

    [Header("# Goods")]
    public DewUI dewUI;

    [Header("# Pure")]
    public GameObject pure;
    public PureStat pureStat;
    public PureAnimationSet pureAnimationSet;
    public PureController pureController;
    public PureInteractionText pureInteractionText;
    public SpeechBubbleSet speechBubbleSet;
    public AutoText autoText;

    [Header("# Present")]
    public PresentManager presentManager;

    [Header("# FieldWork")]
    public FieldWorkManager fieldWorkManager;

    [Header("# Flower")]
    public FlowerManager flowerManager;

    [Header("# VFX")]
    public VFXManager vfxManager;

    [Header("# Likeability")]
    public int interactionLikeability;
    public float likeabilityIntervalTime;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        if (instance == null)
            instance = this;

        #region init
        if (presentManager == null)
            presentManager = FindFirstObjectByType<PresentManager>();
        if(fieldWorkManager == null)
            fieldWorkManager = FindFirstObjectByType<FieldWorkManager>();
        if(pureStat == null)
            pureStat = FindFirstObjectByType<PureStat>();
        if(pureAnimationSet == null)
            pureAnimationSet = FindFirstObjectByType<PureAnimationSet>();
        if(pureController == null)
            pureController = FindFirstObjectByType<PureController>();
        if(vfxManager == null)
            vfxManager = FindFirstObjectByType<VFXManager>();
        if(speechBubbleSet == null)
            speechBubbleSet = FindFirstObjectByType<SpeechBubbleSet>();
        if(flowerManager == null)
            flowerManager = FindFirstObjectByType<FlowerManager>();
        if(pureInteractionText == null)
            pureInteractionText = FindFirstObjectByType<PureInteractionText>();

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