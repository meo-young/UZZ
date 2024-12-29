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
    string filePathPC = Path.Combine(Application.dataPath + "/05. Database/", "tutorialDatabase.json");

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

    }

    private void Start()
    {
        JsonLoad();
    }

    public void JsonLoad()
    {
        TutorialGameData gameData = new TutorialGameData();
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
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
            
            /*WWW www = new WWW(filePathMobile);
            while (!www.isDone) { }
            string dataAsJson = www.text;

            if (dataAsJson != "")
            {
                Debug.Log(dataAsJson);
                gameData = JsonUtility.FromJson<TutorialGameData>(dataAsJson);
            }*/


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
        }

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
            string filePathMobile = Path.Combine(Application.persistentDataPath, "tutorialDatabase.json");
            string dataAsJson = JsonUtility.ToJson(gameData);
            File.WriteAllText(filePathMobile, dataAsJson);
        }
        else
        {
            string json = JsonUtility.ToJson(gameData, true);
            File.WriteAllText(filePathPC, json);
        }
    }
}