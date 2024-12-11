using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class FieldWorkManager : MonoBehaviour
{
    public static FieldWorkManager instance;

    [Serializable]
    public class _2dArray
    {
        public Sprite[] fieldWorkNumber = new Sprite[5];
    }

    [Header("# FieldWork UI")]
    [SerializeField] FieldWorkUI[] fieldWorkUIArray;
    [SerializeField] TMP_Text[] fieldWorkHelpText;
    [SerializeField] TMP_Text[] fieldWorkHelpSuccessText;
    [SerializeField] Image[] fieldWorkIcon;

    [Header("# FieldWork Info")]
    [SerializeField] _2dArray[] icons;
    [SerializeField] List<int> fieldWorkCooltime;
    public FieldWorkInfo fieldWorkInfo;
    public FieldWork[] fieldWorkArray;
    public FieldWork noneWork;
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
        int randomNum = UnityEngine.Random.Range(0, 100);
        if (randomNum < helpProbability)
            return true;

        return false;
    }

    // 자동작업 확률 체크
    public bool CheckAutoWorkProbability()
    {
        int randNum = UnityEngine.Random.Range(0, 100);

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
            int randNum = UnityEngine.Random.Range(0, 5);
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

            fieldWorkUIArray[i].gameObject.SetActive(true);                                                     // 구매한 작업의 UI 활성화
            fieldWorkArray[i].step = step;                                                                      // Step
            fieldWorkArray[i].level = level;                                                                    // Level
            fieldWorkArray[i].type = (FieldWorkType)i;                                                          // Watering, Scissor ... 구분
            fieldWorkArray[i].growPoint = fieldWorkData[i][fieldWorkInfo.level[i] - 1].growPoint;               // 획득 성장치
            fieldWorkArray[i].dewPoint = fieldWorkData[i][fieldWorkInfo.level[i] - 1].dewPoint;                 // 획득 이슬
            fieldWorkArray[i].price = fieldWorkData[i][fieldWorkInfo.level[i] - 1].price;                       // 작업 구매 가격
            fieldWorkArray[i].helpText = fieldWorkData[i][fieldWorkInfo.level[i] - 1].helpText;                 // 작업 도움 텍스트
            fieldWorkArray[i].helpSuccessText = fieldWorkData[i][fieldWorkInfo.level[i] - 1].helpSuccessText;   // 작업 도움 성공 텍스트
            fieldWorkArray[i].available = true;                                                                 // 현재 실행가능한 작업인지
            fieldWorkArray[i].coolTime = fieldWorkCooltime[i];                                                  // 쿨타임
            fieldWorkArray[i].icon = fieldWorkData[i][fieldWorkInfo.level[i] - 1].icon;                         // 작업 아이콘
            fieldWorkHelpText[i].text = fieldWorkData[i][fieldWorkInfo.level[i] - 1].helpText;                  // 작업도움텍스트 UI 변경
            fieldWorkHelpSuccessText[i].text = fieldWorkData[i][fieldWorkInfo.level[i] - 1].helpSuccessText;    // 작업도움성공텍스트 UI 변경
            fieldWorkIcon[i].sprite = icons[i].fieldWorkNumber[fieldWorkArray[i].icon];                         // 작업아이콘 할당
        }
    }
}
