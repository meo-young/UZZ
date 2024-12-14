using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FlowerProfileUI : MonoBehaviour
{
    [Header("# Flower Base Info")]
    [SerializeField] TMP_Text flowerName;
    [SerializeField] Text flowerType;

    [Header("# Flower Change Info")]
    [SerializeField] Image flowerImage;
    [SerializeField] Text flowerStep;
    [SerializeField] Text flowerLevel;

    [Header("# Slider")]
    [SerializeField] Slider flowerCurrentGrowthSlider;
    [SerializeField] Text requiredExpForNextLevel;

    [Header("# Total")]
    [SerializeField] Text totalTime;
    [SerializeField] Text totalDays;
    [SerializeField] Text totalDews;
    [SerializeField] Text totalGrowth;

    private int playTime => (int)Mathf.Floor(MainManager.instance.gameInfo.playTime / 3600);
    private int dew => (int)MainManager.instance.gameInfo.dew;
    private float growth => FlowerManager.instance.flowerInfo.exp + FlowerManager.instance.flowerData[FlowerManager.instance.flowerInfo.level].totalExp;
    private int dayCount => MainManager.instance.gameInfo.totalDayCounter;
    private void Awake()
    {
        if(this.gameObject.activeSelf)
            this.gameObject.SetActive(false);
    }

    public void InitFlowerBaseInfo(string _flowerName, string _flowerType)
    {
        flowerName.text = _flowerName;
        flowerType.text = _flowerType;
    }

    public void InitFlowerInfo(Sprite _flowerImage, int _flowerStep, int _flowerLevel)
    {
        flowerStep.text = _flowerStep.ToString();
        flowerLevel.text = _flowerLevel.ToString();
        flowerImage.sprite = _flowerImage;
    }

    public void InitFlowerSlider(float currentGrowth, float requiredExp)
    {
        flowerCurrentGrowthSlider.value = currentGrowth;
        requiredExpForNextLevel.text = "다음 레벨까지 앞으로 " + requiredExp.ToString("F2");
    }

    void InitTotalTime(int hour)
    {
        totalTime.text = hour.ToString() + "시간";
    }

    void InitTotalDays(int days)
    {
        totalDays.text = days.ToString() + "일";
    }

    void InitTotalDew(int dew)
    {
        totalDews.text = dew.ToString();    
    }

    public void InitTotalGrowth()
    {
        totalGrowth.text = growth.ToString("F2");
    }

    public void SetActiveFunction()
    {
        if(!this.gameObject.activeSelf)
        {
            InitTotalTime(playTime);
            InitTotalDew(dew);
            InitTotalGrowth();
            InitTotalDays(dayCount);
            this.gameObject.SetActive(true);
        }
        else
            this.gameObject.SetActive(false);
    }
}
