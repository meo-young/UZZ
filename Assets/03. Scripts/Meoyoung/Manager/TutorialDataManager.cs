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
    string dataPath;


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

        dataPath = Path.Combine(Application.dataPath + "/05. Database/", "tutorialDatabase.json");
    }

    private void Start()
    {
        JsonLoad();
    }

    public void JsonLoad()
    {
        TutorialGameData gameData = new TutorialGameData();
        if (!File.Exists(dataPath))
        {
            Debug.Log("파일이 존재하지 않음");
            JsonSave();
            return;
        }

        string loadJson = File.ReadAllText(dataPath);
        gameData = JsonUtility.FromJson<TutorialGameData>(loadJson);

        if (gameData == null)
            return;

        TutorialChecker.instance.flag = gameData.flag;
    }


    public void JsonSave()
    {
        TutorialGameData gameData = new TutorialGameData();

        gameData.flag = TutorialChecker.instance.flag;

        string json = JsonUtility.ToJson(gameData, true);
        File.WriteAllText(dataPath, json);
    }
}