using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using AYellowpaper.SerializedCollections;
using static Constant;

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
    public DrawInfo[] drawInfo = new DrawInfo[DRAW_THEME_COUNT];
    public SerializedDictionary<string, FurniturePlacementData> gardenInfo = new();
}

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    //public UnityEngine.UI.Slider progressBar;  // 진행 상황을 표시할 Slider

    string filePath;   // System.IO 용 경로 (file:// 제외)

    private void Awake()
    {
        if (instance == null)
            instance = this;

#if UNITY_EDITOR
        filePath = Path.Combine(Application.dataPath + "/05. Database/", "database.json");
        Debug.Log("Platform : PC");

#elif UNITY_ANDROID || UNITY_IOS
        filePath = Path.Combine(Application.persistentDataPath, "database.json");
        Debug.Log("Platform : " + (Application.platform == RuntimePlatform.Android ? "Android" : "iOS"));
#endif

        JsonLoadAsync();
    }

    // 비동기적으로 데이터를 불러오는 함수
    public void JsonLoadAsync()
    {
        // 파일 존재 여부 확인
        if (!File.Exists(filePath))
        {
            Debug.Log("데이터매니저 파일이 존재하지 않음");
            JsonSaveAsync();  // 파일이 없으면 저장
            return;
        }

        long totalBytes = new FileInfo(filePath).Length;  // 파일 크기 (바이트 단위)
        long bytesRead = 0;  // 읽은 바이트 수

        string dataAsJson = "";  // 파일 내용을 담을 변수

        // 비동기적으로 파일을 읽으면서 진행 상황 표시
        using (StreamReader reader = new StreamReader(filePath))
        {
            char[] buffer = new char[1024];  // 읽을 버퍼
            int bytesReadInChunk;  // 한 번에 읽은 데이터 크기

            while ((bytesReadInChunk = reader.Read(buffer, 0, buffer.Length)) > 0)
            {
                // 읽은 데이터를 추가
                dataAsJson += new string(buffer, 0, bytesReadInChunk);
                bytesRead += bytesReadInChunk;

                // 진행 상황 계산
                float progress = (float)bytesRead / totalBytes;
                Debug.Log(progress*100 + "%");
            }
        }

        // 파일 데이터를 JSON으로 변환
        GameData gameData = JsonUtility.FromJson<GameData>(dataAsJson);

        if (gameData == null)
            return;

        MainManager.instance.gameInfo.playTime = gameData.gameInfo.playTime;
        MainManager.instance.gameInfo.likeabilityFlag = gameData.gameInfo.likeabilityFlag;
        MainManager.instance.gameInfo.lastConnectTime = gameData.gameInfo.lastConnectTime;
        MainManager.instance.gameInfo.inGameTime = gameData.gameInfo.inGameTime + Utility.instance.GetIntervalDateTime() * INGAME_TIME_MULTIFLIER;
        MainManager.instance.gameInfo.likeabilityTimer = gameData.gameInfo.likeabilityTimer + Utility.instance.GetIntervalDateTime();
        MainManager.instance.gameInfo.dew = gameData.gameInfo.dew;
        MainManager.instance.gameInfo.totalDayCounter = gameData.gameInfo.totalDayCounter;
        MainManager.instance.gameInfo.cycleFlag = gameData.gameInfo.cycleFlag;
        MainManager.instance.gameInfo.showerFlag = gameData.gameInfo.showerFlag;
        MainManager.instance.gameInfo.showerTimer = gameData.gameInfo.showerTimer + Utility.instance.GetIntervalDateTime();
        MainManager.instance.gameInfo.mealFlag = gameData.gameInfo.mealFlag;
        MainManager.instance.gameInfo.mealTimer = gameData.gameInfo.mealTimer + Utility.instance.GetIntervalDateTime();


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

        DrawManager.instance.drawInfo = gameData.drawInfo;

        GardenManager.instance.gardenInfo = gameData.gardenInfo;
    }

    // 비동기적으로 데이터를 저장하는 함수
    public void JsonSaveAsync()
    {
        GameData gameData = new GameData();

        gameData.gameInfo = MainManager.instance.gameInfo;
        gameData.flowerInfo = FlowerManager.instance.flowerInfo;
        gameData.fieldWorkInfo = FieldWorkManager.instance.fieldWorkInfo;
        gameData.presentInfo = PresentManager.instance.presentInfo;
        gameData.pureInfo = PureStat.instance.pureInfo;
        gameData.diaryInfo = DiaryManager.instance.diaryInfo;
        gameData.autoGrowInfo = AutoGrowManager.instance.autoGrowInfo;
        gameData.drawInfo = DrawManager.instance.drawInfo;
        gameData.gardenInfo = GardenManager.instance.gardenInfo;

        string json = JsonUtility.ToJson(gameData, true);

        // 비동기적으로 파일에 저장
        // await Task.Run(() => File.WriteAllText(filePath, json));
        File.WriteAllText(filePath, json);
        Debug.Log("파일 저장 경로: " + filePath);
    }
}
