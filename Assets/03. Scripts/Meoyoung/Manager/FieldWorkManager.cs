using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class FieldWorkInfo
{
    public int level;
    public float[] coolTimeList;
    // 생성자에서 배열 크기 초기화
    public FieldWorkInfo()
    {
        coolTimeList = new float[5]; // 배열 크기를 5로 초기화
    }
}

public class FieldWorkManager : MonoBehaviour
{
    public static FieldWorkManager instance;

    [Header("# FieldWork UI")]
    public FieldWorkUI[] fieldWorkUIArray;
    [SerializeField] List<int> fieldWorkCooltime;

    [Header("# FieldWork Info")]
    [SerializeField] TextAsset fieldWorkDataTable;
    FieldWork[][] fieldWorkData; // CSV 파일에서 가져온 데이터
    [SerializeField] TMP_Text[] fieldWorkHelpText;
    [SerializeField] TMP_Text[] fieldWorkHelpSuccessText;
    public FieldWorkInfo fieldWorkInfo;
    public FieldWork[] fieldWorkArray;
    public FieldWork noneWork;

    [Header("# FieldWork Stat")]
    public int likeability;
    [Space(10)]
    public float helpProbabilityCheckTime;
    [Range(0, 100)] public int helpProbability;
    [Space(10)]
    public float autoWorkCheckTime;
    [Range(0, 100)] public int autoWorkProbability;

    private void Awake()
    {
        if(instance == null)
            instance = this;

        fieldWorkArray = new FieldWork[5];
        for (int i = 0; i < fieldWorkArray.Length; i++)
            fieldWorkArray[i] = new FieldWork();
    }

    private void Start()
    {
        UpdateFieldWorkData();
        InitFieldWorkData();
    }


    // 도움작업 확률 체크
    public bool CheckHelpProbability()
    {
        int randomNum = Random.Range(0, 100);
        if (randomNum < helpProbability)
            return true;

        return false;
    }

    // 자동작업 확률 체크
    public bool CheckAutoWorkProbability()
    {
        int randNum = Random.Range(0, 100);

        if (randNum < autoWorkProbability)
        {
            return true;
        }

        return false;
    }

    // 자동작업시 가능한 일 부여
    public FieldWork CheckAvailableFieldWork()
    {
        while (true)
        {
            int randNum = Random.Range(0, 5);
            if (fieldWorkArray[randNum].available)
            {
                fieldWorkUIArray[randNum].DoFieldWork();
                return fieldWorkArray[randNum];
            }
        }
    }

    void NewFieldWorkDataArray()
    {
        fieldWorkData = new FieldWork[5][];
        for (int i = 0; i < 5; i++)
        {
            fieldWorkData[i] = new FieldWork[10]; // 각 배열에 10개의 요소 할당
            for (int j = 0; j < 10; j++)
            {
                fieldWorkData[i][j] = new FieldWork(); // 각 요소에 FieldWork 인스턴스 생성
            }
        }

    }

    public void UpdateFieldWorkData()
    {
        NewFieldWorkDataArray();

        StringReader reader = new StringReader(fieldWorkDataTable.text);
        bool head = false;
        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            if (!head)
            {
                head = true;
                continue;
            }
            string[] values = line.Split('\t');

            switch (values[0])
            {
                case "Watering":
                    AddFieldWorkData(0, values);
                    break;
                case "Spade":
                    AddFieldWorkData(1, values);
                    break;
                case "Fertilizer":
                    AddFieldWorkData(2, values);
                    break;
                case "Scissor":
                    AddFieldWorkData(3, values);
                    break;
                case "Nutritional":
                    AddFieldWorkData(4, values);
                    break;
            }
        }
    }

    void AddFieldWorkData(int _index, string[] _values)
    {
        fieldWorkData[_index][int.Parse(_values[4]) - 1].helpText = _values[1];
        fieldWorkData[_index][int.Parse(_values[4]) - 1].helpSuccessText = _values[2];
        if (_values[5] == "")
            fieldWorkData[_index][int.Parse(_values[4]) - 1].point = float.Parse(_values[6]);
        else
            fieldWorkData[_index][int.Parse(_values[4]) - 1].point = float.Parse(_values[5]);
        fieldWorkData[_index][int.Parse(_values[4]) - 1].price = int.Parse(_values[7]);
    }

    void InitFieldWorkData()
    {
        for(int i =0; i<fieldWorkInfo.coolTimeList.Length; i++)
            fieldWorkInfo.coolTimeList[i] -= DataManager.instance.GetIntervalDateTime();
        if (fieldWorkInfo.level == 0)
            return;

        int index = fieldWorkInfo.level / 10;
        int remainIndex = fieldWorkInfo.level % 10;
        for (int i = 0; i < index+1; i++)
        {
            if (i == 5)
                return;

            if(i == index)
                fieldWorkArray[i].level = remainIndex;
            else
                fieldWorkArray[i].level = 10;

            if (fieldWorkArray[i].level == 0)
                return;

            fieldWorkUIArray[i].gameObject.SetActive(true);
            fieldWorkArray[i].state = (FieldWorkState)i;
            fieldWorkArray[i].type = (FieldWorkType)(i % 2);
            fieldWorkArray[i].point = fieldWorkData[i][fieldWorkArray[i].level - 1].point;
            fieldWorkArray[i].price = fieldWorkData[i][fieldWorkArray[i].level - 1].price;
            fieldWorkArray[i].helpText = fieldWorkData[i][fieldWorkArray[i].level - 1].helpText;
            fieldWorkArray[i].helpSuccessText = fieldWorkData[i][fieldWorkArray[i].level - 1].helpSuccessText;
            fieldWorkArray[i].available = true;
            fieldWorkArray[i].coolTime = fieldWorkCooltime[i];

            fieldWorkHelpText[i].text = fieldWorkData[i][fieldWorkArray[i].level - 1].helpText;
            fieldWorkHelpSuccessText[i].text = fieldWorkData[i][fieldWorkArray[i].level - 1].helpSuccessText;
        }
    }
}

#region Enum, Class
[System.Serializable]
public enum FieldWorkState
{
    Watering = 0,
    Spade = 1,
    Fertilizer = 2,
    Scissor = 3,
    Nutritional = 4,
    None = 5
}

public enum FieldWorkType
{
    Growth = 0,
    Dew = 1
}
[System.Serializable]
public class FieldWork
{
    public FieldWorkState state;
    public FieldWorkType type;
    public int level = 0;
    public string helpText;
    public string helpSuccessText;
    public int price;
    public int coolTime;
    public float point;
    public bool available = false;
}
#endregion