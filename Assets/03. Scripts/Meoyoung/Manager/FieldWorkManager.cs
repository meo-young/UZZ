using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;


public class FieldWorkManager : MonoBehaviour
{
    public static FieldWorkManager instance;

    [Header("# FieldWork UI")]
    [SerializeField] FieldWorkUI[] fieldWorkUIArray;
    [SerializeField] List<int> fieldWorkCooltime;

    [Header("# FieldWork Info")]
    [SerializeField] TMP_Text[] fieldWorkHelpText;
    [SerializeField] TMP_Text[] fieldWorkHelpSuccessText;
    public FieldWorkInfo fieldWorkInfo;
    public FieldWork[] fieldWorkArray;
    [HideInInspector] public FieldWork noneWork;
    private FieldWork[][] fieldWorkData => LoadFieldWorkData.instance.fieldWorkData;

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

    void InitFieldWorkData()
    {
        for(int i =0; i<fieldWorkInfo.level.Length; i++)
        {
            // 구매하지 않은 작업이면 Return
            if (fieldWorkInfo.level[i] == 0)
                return;
            
            // 쿨타임데이터 로드
            fieldWorkInfo.coolTimeList[i] -= DataManager.instance.GetIntervalDateTime();

            // 작업의 단계와 레벨을 구분하기위한 로직
            int step = fieldWorkInfo.level[i] / 10;
            int level = fieldWorkInfo.level[i] % 10;

            fieldWorkUIArray[i].gameObject.SetActive(true);
            fieldWorkArray[i].step = step;
            fieldWorkArray[i].level = level;
            fieldWorkArray[i].type = (FieldWorkType)i;
            fieldWorkArray[i].result = (ResultType)(i % 2);
            fieldWorkArray[i].growPoint = fieldWorkData[i][fieldWorkInfo.level[i] - 1].growPoint;
            fieldWorkArray[i].dewPoint = fieldWorkData[i][fieldWorkInfo.level[i] - 1].dewPoint;
            fieldWorkArray[i].price = fieldWorkData[i][fieldWorkInfo.level[i] - 1].price;
            fieldWorkArray[i].helpText = fieldWorkData[i][fieldWorkInfo.level[i] - 1].helpText;
            fieldWorkArray[i].helpSuccessText = fieldWorkData[i][fieldWorkInfo.level[i] - 1].helpSuccessText;
            fieldWorkArray[i].available = true;
            fieldWorkArray[i].coolTime = fieldWorkCooltime[i];


            fieldWorkHelpText[i].text = fieldWorkData[i][fieldWorkInfo.level[i] - 1].helpText;
            fieldWorkHelpSuccessText[i].text = fieldWorkData[i][fieldWorkInfo.level[i] - 1].helpSuccessText;
        }
    }
}
