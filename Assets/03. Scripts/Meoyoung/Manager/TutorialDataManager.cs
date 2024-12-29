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
    string filePath;

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
        filePath = Path.Combine(Application.dataPath + "/05. Database/", "database.json");
        Debug.Log("Platform : PC");

        #elif UNITY_ANDROID
        filePath = Path.Combine(Application.persistentDataPath, "database.json");
        Debug.Log("Platform : Android");

        #elif UNITY_IOS
        filePath = Path.Combine(Application.persistentDataPath, "database.json");
        Debug.Log("Platform : IOS");

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
            Debug.Log("파일이 존재하지 않음");
            JsonSave();
            return;
        }

        string dataAsJson;

        #if UNITY_EDITOR || UNITY_IOS
        dataAsJson = File.ReadAllText(filePath);

        #elif UNITY_ANDROID
        WWW reader = new WWW(filePath);
        while(!reader.isDone){
        }
        dataAsJson = reader.text;

        #endif

        TutorialGameData gameData = new TutorialGameData();
        gameData = JsonUtility.FromJson<TutorialGameData>(dataAsJson);

        /*if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            string filePathMobile = Path.Combine(Application.persistentDataPath, "tutorialDatabase.json");

            if (!File.Exists(filePathMobile))
            {
                Debug.Log("파일이 존재하지 않음");
                JsonSave();
                return;
            }

            string json = File.ReadAllText(filePathMobile);
            gameData = JsonUtility.FromJson<TutorialGameData>(json);   
            
            *//*WWW www = new WWW(filePathMobile);
            while (!www.isDone) { }
            string dataAsJson = www.text;

            if (dataAsJson != "")
            {
                Debug.Log(dataAsJson);
                gameData = JsonUtility.FromJson<TutorialGameData>(dataAsJson);
            }*//*


        }
        else
        {
            if (!File.Exists(filePathPC))
            {
                Debug.Log("파일이 존재하지 않음");
                JsonSave();
                return;
            }

            string loadJson = File.ReadAllText(filePathPC);
            gameData = JsonUtility.FromJson<TutorialGameData>(loadJson);
        }*/

        if (gameData == null)
            return;

        TutorialChecker.instance.flag = gameData.flag;
    }


    public void JsonSave()
    {
        TutorialGameData gameData = new TutorialGameData();

        gameData.flag = TutorialChecker.instance.flag;

        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            string dataAsJson = JsonUtility.ToJson(gameData);
            File.WriteAllText(filePath, dataAsJson);
        }
        else
        {
            string json = JsonUtility.ToJson(gameData, true);
            File.WriteAllText(filePath, json);
        }
    }
}