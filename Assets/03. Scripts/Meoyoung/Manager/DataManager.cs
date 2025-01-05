using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public GameInfo gameInfo = new();
    public FlowerInfo flowerInfo = new();
    public PresentInfo presentInfo = new();
    public PureInfo pureInfo = new();
    public FieldWorkInfo fieldWorkInfo = new();
    public DiaryInfo diaryInfo = new();
    public AutoGrowInfo autoGrowInfo = new();
}

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    string filePath;  // WWW용 경로 (file:// 포함)
    string rawPath;   // System.IO 용 경로 (file:// 제외)

    private void Awake()
    {
        if (instance == null)
            instance = this;

#if UNITY_EDITOR
        rawPath = Path.Combine(Application.dataPath + "/05. Database/", "database.json");
        filePath = rawPath;
        Debug.Log("Platform : PC");

#elif UNITY_ANDROID || UNITY_IOS
        rawPath = Path.Combine(Application.persistentDataPath, "database.json");
        filePath = "file://" + rawPath;
        Debug.Log("Platform : " + (Application.platform == RuntimePlatform.Android ? "Android" : "iOS"));
#endif
    }


    private void Start()
    {
        JsonLoad();
    }


    public void JsonLoad()
    {
        // 파일 존재 여부 확인 (rawPath 사용)
        if (!File.Exists(rawPath))
        {
            Debug.Log("데이터매니저 파일이 존재하지 않음");
            JsonSave();
            return;
        }

        string dataAsJson;

#if UNITY_EDITOR || UNITY_IOS
        // 파일 읽기 (System.IO)
        dataAsJson = File.ReadAllText(rawPath);

#elif UNITY_ANDROID
        // 파일 읽기 (WWW)
        WWW reader = new WWW(filePath);  // file:// 포함 경로 사용
        while (!reader.isDone)
        {
            // 대기
        }
        if (!string.IsNullOrEmpty(reader.error))
        {
            Debug.LogError("파일 읽기 오류: " + reader.error);
            return;
        }
        dataAsJson = reader.text;

#endif

        GameData gameData = new GameData();
        gameData = JsonUtility.FromJson<GameData>(dataAsJson);


        if (gameData == null)
            return;

        MainManager.instance.gameInfo.playTime = gameData.gameInfo.playTime;
        MainManager.instance.gameInfo.likeabilityFlag = gameData.gameInfo.likeabilityFlag;
        MainManager.instance.gameInfo.lastConnectTime = gameData.gameInfo.lastConnectTime;
        MainManager.instance.gameInfo.inGameTime = gameData.gameInfo.inGameTime + Utility.instance.GetIntervalDateTime() * MainManager.instance.timeMultifiler;
        MainManager.instance.gameInfo.likeabilityTimer = gameData.gameInfo.likeabilityTimer + Utility.instance.GetIntervalDateTime();
        MainManager.instance.gameInfo.dew = gameData.gameInfo.dew;
        MainManager.instance.gameInfo.totalDayCounter = gameData.gameInfo.totalDayCounter;
        MainManager.instance.gameInfo.cycleFlag = gameData.gameInfo.cycleFlag;

        FlowerManager.instance.flowerInfo.level = gameData.flowerInfo.level;
        FlowerManager.instance.flowerInfo.exp = gameData.flowerInfo.exp;
        FlowerManager.instance.flowerInfo.isStepUp = gameData.flowerInfo.isStepUp;
        FlowerManager.instance.flowerInfo.totalGetDew = gameData.flowerInfo.totalGetDew;
        FlowerManager.instance.flowerInfo.dewCounter = gameData.flowerInfo.dewCounter + Utility.instance.GetIntervalDateTime();

        FieldWorkManager.instance.fieldWorkInfo = gameData.fieldWorkInfo;

        PresentManager.instance.presentInfo = gameData.presentInfo;


        PureStat.instance.pureInfo.level = gameData.pureInfo.level;
        PureStat.instance.pureInfo.likeability = gameData.pureInfo.likeability;

        DiaryManager.instance.diaryInfo = gameData.diaryInfo;

        AutoGrowManager.instance.autoGrowInfo = gameData.autoGrowInfo;
    }



    public void JsonSave()
    {
        GameData gameData = new GameData();

        gameData.gameInfo = MainManager.instance.gameInfo;

        gameData.flowerInfo = FlowerManager.instance.flowerInfo;

        gameData.fieldWorkInfo = FieldWorkManager.instance.fieldWorkInfo;

        gameData.presentInfo = PresentManager.instance.presentInfo;

        gameData.pureInfo = PureStat.instance.pureInfo;

        gameData.diaryInfo = DiaryManager.instance.diaryInfo;

        gameData.autoGrowInfo = AutoGrowManager.instance.autoGrowInfo;

#if UNITY_EDITOR || UNITY_IOS || UNITY_ANDROID
        // 파일 쓰기 (rawPath 사용)
        string json = JsonUtility.ToJson(gameData, true);
        File.WriteAllText(rawPath, json);
        Debug.Log("파일 저장 경로: " + rawPath);

#endif
    }
}
