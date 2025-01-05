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
    string filePath;  // WWW용 경로 (file:// 포함)
    string rawPath;   // System.IO 용 경로 (file:// 제외)

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
        rawPath = Path.Combine(Application.dataPath + "/05. Database/", "tutodatabase.json");
        filePath = rawPath;
        Debug.Log("Platform : PC");

#elif UNITY_ANDROID || UNITY_IOS
        rawPath = Path.Combine(Application.persistentDataPath, "tutodatabase.json");
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
            Debug.Log("튜토리얼 데이터 파일이 존재하지 않음");
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

        Debug.Log("GameData Flag 저장: " + TutorialChecker.instance.flag);

#if UNITY_EDITOR || UNITY_IOS || UNITY_ANDROID
        // 파일 쓰기 (rawPath 사용)
        string json = JsonUtility.ToJson(gameData, true);
        File.WriteAllText(rawPath, json);
        Debug.Log("파일 저장 경로: " + rawPath);

#endif
    }
}