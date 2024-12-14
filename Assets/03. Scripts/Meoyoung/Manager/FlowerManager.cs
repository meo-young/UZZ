using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class FlowerInfo
{
    public int level;
    public float exp;
    public int totalGetDew;
    public bool isStepUp;
    public float dewCounter;
}

public class FlowerManager : MonoBehaviour
{
    public static FlowerManager instance;

    [System.Serializable]
    public struct FlowerData // 인덱스는 flowerLevel로 체크
    {
        public int level;
        public int step;
        public Sprite image;
        public int requiredExp;
        public float prodcuedGrowth;
        public float maxGrowth;
        public int producedDew;
        public int maxDew;
        public float totalExp;
    }

    [Header("# Flower Info")]
    [SerializeField] TextAsset flowerDataTable;
    public FlowerInfo flowerInfo;
    [SerializeField] float dewMinInterval;
    public Sprite[] flowerImage;
    [HideInInspector] public FlowerData[] flowerData;

    [Header("# Flower UI")]
    [SerializeField] FlowerUI flowerUI;
    [SerializeField] FlowerProfileUI flowerProfileUI;

    [Header("# FlowerManager Stat")]
    [Range(0, 100)][SerializeField] int flowerEventProbability;
    [Range(0, 100)][SerializeField] int bigProbability;
    [Space(10)]
    [SerializeField] int bigLikeability; // 빅이벤트 호감도 보상
    [SerializeField] int bigGrowth; // 빅이벤트 성장치 보상
    [SerializeField] int miniGrowth; // 미니이벤트 성장치 보상
    public float miniEventFinishTime; // 미니이벤트 이벤트 소요 시간
    [Space(10)]
    public float maxShakeTime;
    public float acquireInterval;
    public Transform acquireEffectSpawnPos;
    public Transform acquireEffectTargetPos;
    public Transform stepUpEffectPos;
    public Transform levelUpEffectPos;
    public ItemAcquireFx itemPrefab;
    private List<ItemAcquireFx> items;
    private float counter;
    private float intervalCounter;
    private bool finishShakeEvent;
    [Space(10)]

    [Header("# FlowerEvent Object")]
    [SerializeField] GameObject bigDust; // 빅이벤트 출력할 먼지
    [SerializeField] GameObject miniDust; // 미니이벤트 출력할 먼지
    [Space(10)]
    [SerializeField] List<GameObject> defaultFlower;
    [SerializeField] List<GameObject> bigFlower;
    [SerializeField] List<GameObject> miniFlower;
    [HideInInspector] public bool isFlowerEvent;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        UpdateFlowerDataTable();
    }

    private void Start()
    {
        InitAcquireVariable();
        isFlowerEvent = false;
        items = new List<ItemAcquireFx>();
        SetActiveFalseAllFlower();
        InitFlowerUI();
        defaultFlower[flowerData[flowerInfo.level].step - 1].SetActive(true);
        if (flowerInfo.isStepUp) // 단계업중인지 확인
            ShowStepUpEffect();
    }

    private void Update()
    {
        CheckDewCount();
    }


    #region Probability
    public bool CheckFlowerEventProbability()
    {
        int randNum = Random.Range(0, 100);
        if (randNum < flowerEventProbability)
        {
            isFlowerEvent = true;
            CheckBigEventProbability();
            return true;
        }

        return false;
    }

    void CheckBigEventProbability()
    {
        int randNum = Random.Range(0, 100);
        if (randNum < bigProbability)
            ShowBigFlowerEvent();
        else
            ShowMiniFlowerEvent();
    }
    #endregion

    #region ShowEvent
    void ShowBigFlowerEvent()
    {
        SoundManager.instance.PlaySFX(SFX.Flower.DUST);
        SetActiveFalseDefaultFlower();
        bigFlower[flowerData[flowerInfo.level].step - 1].SetActive(true);
        bigDust.SetActive(true);
        MainManager.instance.pure.SetActive(false);
    }

    void ShowMiniFlowerEvent()
    {
        SoundManager.instance.PlaySFX(SFX.Flower.DUST);
        SetActiveFalseDefaultFlower();
        miniFlower[flowerData[flowerInfo.level].step - 1].SetActive(true);
        miniDust.SetActive(true);
        MainManager.instance.pure.SetActive(false);
    }
    #endregion

    #region SetActive Function
    void SetActiveFalseAllFlower()
    {
        for (int i = 0; i < defaultFlower.Count; i++)
        {
            if (defaultFlower[i].activeSelf)
                defaultFlower[i].SetActive(false);

            if (bigFlower[i].activeSelf)
                bigFlower[i].SetActive(false);

            if (miniFlower[i].activeSelf)
                miniFlower[i].SetActive(false);
        }
    }
    void SetActiveFalseDefaultFlower()
    {
        for (int i = 0; i < defaultFlower.Count; i++)
            defaultFlower[i].SetActive(false);
    }

    void BackToDefaultFlower()
    {
        int flowerStep = flowerData[flowerInfo.level].step - 1;

        defaultFlower[flowerStep].SetActive(true);
        if (miniFlower[flowerStep].activeSelf)
            miniFlower[flowerStep].SetActive(false);
        if (bigFlower[flowerStep].activeSelf)
            bigFlower[flowerStep].SetActive(false);
        if (miniDust.activeSelf)
            miniDust.SetActive(false);
        if (bigDust.activeSelf)
            bigDust.SetActive(false);
    }

    #endregion

    #region Reward
    public void GetBigEventReward()
    {
        isFlowerEvent = false;
        MainManager.instance.pureStat.GetLikeability(bigLikeability);
        BackToDefaultFlower();
        GetFlowerExp(bigGrowth);
    }

    public void GetMiniEventReward()
    {
        isFlowerEvent = false;
        BackToDefaultFlower();
        GetFlowerExp(miniGrowth);
    }
    #endregion

    #region Flower Exp

    public void GetFlowerExp(float exp, int _soundFlag = 0)
    {
        if (flowerInfo.isStepUp)
            return;

        if(_soundFlag == 0)
            SoundManager.instance.PlaySFX(SFX.Flower.GROW);

        UpdateFlowerSlider(exp);
        flowerInfo.exp += exp;
        if (flowerData[flowerInfo.level].requiredExp <= flowerInfo.exp)
        {
            if (flowerData[flowerInfo.level].step != flowerData[flowerInfo.level + 1].step)
            {
                if (flowerData[flowerInfo.level + 1].step == 0)
                    return;

                ShowStepUpEffect();
            }
            else
            {
                Instantiate(MainManager.instance.vfxManager.flowerLevelUpVFX, levelUpEffectPos.position, Quaternion.identity);
                SoundManager.instance.PlaySFX(SFX.Flower.LEVELUP);
                flowerInfo.level++;
                flowerInfo.exp = 0;
            }
        }

        InitFlowerUI();
    }

    void UpdateFlowerSlider(float _acquiredExp)
    {
        // 현재, 목표 경험치 비율 계산
        float currentExp = (float)flowerInfo.exp / flowerData[flowerInfo.level].requiredExp;
        float targetExp = (float)(flowerInfo.exp + _acquiredExp) / flowerData[flowerInfo.level].requiredExp;
        if (targetExp > 1)
            targetExp = 1;

        // 현재 레벨
        int currentFlowerLevel = flowerInfo.level + 1;

        // 다음 레벨의 꽃 이미지
        Sprite nextFlowerImage;
        if (flowerImage.Length <= flowerData[flowerInfo.level + 1].step)
            nextFlowerImage = flowerImage[flowerData[flowerInfo.level].step];
        else
            nextFlowerImage = flowerImage[flowerData[flowerInfo.level + 1].step];

        // 꽃 UI 최신화
        flowerUI.UpdateFlowerUi(currentExp, targetExp, currentFlowerLevel, nextFlowerImage);
    }

    void InitFlowerUI()
    {
        Sprite currentFlowerImage = flowerImage[flowerData[flowerInfo.level].step];
        float currentGrowthRatio = (float)flowerInfo.exp / flowerData[flowerInfo.level].requiredExp;
        float requiredExp = flowerData[flowerInfo.level].requiredExp - flowerInfo.exp;
        int currentFlowerLevel = flowerInfo.level + 1;
        int currentFlowerStep = flowerData[flowerInfo.level].step;

        flowerProfileUI.InitFlowerInfo(currentFlowerImage, currentFlowerStep, currentFlowerLevel);
        flowerProfileUI.InitFlowerSlider(currentGrowthRatio, requiredExp);
        flowerProfileUI.InitTotalGrowth();
    }

    void ShowStepUpEffect()
    {
        SoundManager.instance.PlaySFX(SFX.Flower.GLOW);
        flowerInfo.isStepUp = true;
        Instantiate(VFXManager.instance.flowerStepUpWaitVFX, stepUpEffectPos.position, Quaternion.identity);
    }

    public void FlowerStepUp()
    {
        Instantiate(MainManager.instance.vfxManager.flowerStepUpVFX, stepUpEffectPos.position, Quaternion.identity);
        SoundManager.instance.PlaySFX(SFX.Flower.STEPUP);
        SetActiveFalseDefaultFlower();
        flowerInfo.level++;
        flowerInfo.exp = 0;
        defaultFlower[flowerData[flowerInfo.level].step - 1].SetActive(true);
        flowerInfo.isStepUp = false;
        InitFlowerUI();
    }
    #endregion

    #region Acquire Event
    void ShowAcquireEffect()
    {
        int randCount = Random.Range(7, 15);
        for (int i = 0; i < randCount; ++i)
        {
            var itemFx = GameObject.Instantiate<ItemAcquireFx>(itemPrefab, this.transform);
            itemFx.Explosion(acquireEffectSpawnPos.position, 10.0f);
            items.Add(itemFx);
        }
        Instantiate(VFXManager.instance.daisyTouchVFX, acquireEffectSpawnPos);
    }
    public void MoveToTargetPos()
    {
        if (items == null)
            return;

        SoundManager.instance.PlaySFX(SFX.DEW.DROP);
        for (int i = 0; i < items.Count; i++)
            items[i].Move(acquireEffectTargetPos.position);

        GetDew();
        InitAcquireVariable();
    }

    public bool AcquireDewEffect()
    {
        if (flowerInfo.totalGetDew == 0)
            return false;

        if (finishShakeEvent) // 최대 흔들기 시간이 지난 경우 터치를 인식하지 않음
            return false;

        counter += Time.deltaTime;

        if (counter - intervalCounter > 0)
        {
            intervalCounter += acquireInterval;
            ShowAcquireEffect();
        }

        if (counter > maxShakeTime)
            finishShakeEvent = true;

        return true;
    }

    void InitAcquireVariable()
    {
        finishShakeEvent = false;
        intervalCounter = acquireInterval;
        counter = 0;
    }

    void GetDew()
    {
        float dew = flowerInfo.totalGetDew * (intervalCounter - acquireInterval) / maxShakeTime;
        MainManager.instance.dewUI.Count(dew);
        flowerInfo.dewCounter = 0;
    }

    public void CheckDewCount()
    {
        flowerInfo.dewCounter += Time.deltaTime;

        flowerInfo.totalGetDew = (int)(Mathf.Floor(flowerInfo.dewCounter / dewMinInterval) * flowerData[flowerInfo.level].producedDew);
        if (flowerInfo.totalGetDew >= flowerData[flowerInfo.level].maxDew)
            flowerInfo.totalGetDew = flowerData[flowerInfo.level].maxDew;
    }
    #endregion

    #region Flower DataTable
    public void UpdateFlowerDataTable()
    {
        flowerData = new FlowerData[100];
        StringReader reader = new StringReader(flowerDataTable.text);
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

            flowerData[int.Parse(values[0]) - 1].step = int.Parse(values[1]);
            flowerData[int.Parse(values[0]) - 1].requiredExp = int.Parse(values[3]);
            flowerData[int.Parse(values[0]) - 1].totalExp = float.Parse(values[4]);
            flowerData[int.Parse(values[0]) - 1].prodcuedGrowth = float.Parse(values[5]);
            flowerData[int.Parse(values[0]) - 1].maxGrowth = float.Parse(values[6]);
            flowerData[int.Parse(values[0]) - 1].producedDew = int.Parse(values[7]);
            flowerData[int.Parse(values[0]) - 1].maxDew = int.Parse(values[8]);
        }
    }
    #endregion
}
