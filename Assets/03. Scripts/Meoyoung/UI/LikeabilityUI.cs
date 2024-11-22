using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LikeabilityUI : MonoBehaviour
{
    [SerializeField] Slider likeabilitySilder;
    [SerializeField] Text likeabilityLevel;


    public void ReflectionValue(float sliderValue, int level)
    {
        likeabilitySilder.value = sliderValue;
        likeabilityLevel.text = "LV " + (level+1);
    }
}
