﻿using System;
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

    string dataPath;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        dataPath = Path.Combine(Application.dataPath + "/05. Database/", "database.json");
    }

    private void Start()
    {
        JsonLoad();
    }


    public void JsonLoad()
    {
        GameData gameData = new GameData();
        if (!File.Exists(dataPath))
        {
            Debug.Log("파일이 존재하지 않음");
            JsonSave();
            return;
        }

        string loadJson = File.ReadAllText(dataPath);
        gameData = JsonUtility.FromJson<GameData>(loadJson);

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

        PresentManager.instance.presentInfo.presentTimer = gameData.presentInfo.presentTimer;

        PureStat.instance.pureInfo.level = gameData.pureInfo.level;
        PureStat.instance.pureInfo.likeability = gameData.pureInfo.likeability;

        DiaryUI.instance.diaryInfo = gameData.diaryInfo;

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

        gameData.diaryInfo = DiaryUI.instance.diaryInfo;

        gameData.autoGrowInfo = AutoGrowManager.instance.autoGrowInfo;

        string json = JsonUtility.ToJson(gameData, true);
        File.WriteAllText(dataPath, json);
    }
}
