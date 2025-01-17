using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlowerUI : MonoBehaviour
{
    [SerializeField] Slider flowerGrowthSlider;
    [SerializeField] Text flowerLevel;
    [SerializeField] Image flowerImage;
    private int level => FlowerManager.instance.flowerInfo.level;
    private float exp => FlowerManager.instance.flowerInfo.exp;
    private void Start()
    {
        Debug.Log("FlowerUI");
        flowerLevel.text = (level+1).ToString();
        flowerImage.sprite = FlowerManager.instance.flowerImage[FlowerManager.instance.flowerData[level].step];
        flowerGrowthSlider.value = exp / FlowerManager.instance.flowerData[level].requiredExp;
    }

    public void UpdateFlowerUi(float _currentExp, float _targetExp, int _flowerLevel, Sprite _nextFlowerImage)
    {
        Utility.instance.SliderCounting(flowerGrowthSlider, _currentExp, _targetExp, () => CheckLevelUp(_targetExp, _flowerLevel, _nextFlowerImage));
        flowerLevel.text = _flowerLevel.ToString();
    }


    void CheckLevelUp(float _targetExp, int _flowerLevel, Sprite nextFlowerImage)
    {
        if(_targetExp == 1)
        {
            flowerLevel.text = (_flowerLevel+1).ToString();
            flowerImage.sprite = nextFlowerImage;
        }
    }
}
