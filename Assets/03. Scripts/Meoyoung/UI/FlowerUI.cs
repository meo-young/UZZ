using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlowerUI : MonoBehaviour
{
    [SerializeField] Slider flowerGrowthSlider;
    [SerializeField] Text flowerLevel;
    [SerializeField] Image flowerImage;


    public void InitFlowerUI(float currentGrowth, int _flowerLevel, Sprite _flowerImage)
    {
        flowerGrowthSlider.value = currentGrowth;
        flowerLevel.text = _flowerLevel.ToString();
        flowerImage.sprite = _flowerImage;
    }
}
