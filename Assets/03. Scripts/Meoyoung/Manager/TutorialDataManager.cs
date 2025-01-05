using UnityEngine;
using System.IO;
using System;

[System.Serializable]
public class TutorialGameData
{
    public bool flag;
}
public class TutorialDataManager : MonoBehaviour
{
    public static TutorialDataManager instance;
    string filePath;   // System.IO 용 경로 (file:// 제외)

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

#if UNITY_EDITOR
        filePath = Path.Combine(Application.dataPath + "/05. Database/", "tutodatabase.json");
        Debug.Log("Platform : PC");

#elif UNITY_ANDROID || UNITY_IOS
        filePath = Path.Combine(Application.persistentDataPath, "tutodatabase.json");
        Debug.Log("Platform : " + (Application.platform == RuntimePlatform.Android ? "Android" : "iOS"));
#endif

    }

    private void Start()
    {
        JsonLoad();
    }

    public void JsonLoad()
    {
        if (!File.Exists(filePath))
        {
            Debug.Log("튜토리얼 데이터 파일이 존재하지 않음");
            JsonSave();
            return;
        }

        string dataAsJson;

        // 파일 읽기 (System.IO)
        dataAsJson = File.ReadAllText(filePath);

        // JSON 파싱
        TutorialGameData gameData = JsonUtility.FromJson<TutorialGameData>(dataAsJson);

        if (gameData == null)
        {
            Debug.LogError("GameData가 Null입니다.");
            return;
        }

        // 데이터 로드
        TutorialChecker.instance.flag = gameData.flag;
        Debug.Log("GameData 로드 성공: " + gameData.flag);
    }


    public void JsonSave()
    {
        TutorialGameData gameData = new TutorialGameData
        {
            flag = TutorialChecker.instance.flag
        };

        string json = JsonUtility.ToJson(gameData, true);
        File.WriteAllText(filePath, json);
    }
}