using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class LikeabilityUI : MonoBehaviour
{
    [SerializeField] Slider likeabilitySilder;
    [SerializeField] Text likeabilityLevel;

    private void Awake()
    {
        if (this.gameObject.activeSelf)
            this.gameObject.SetActive(false);
    }
    public void ReflectionValue(float _sliderValue, float _targetValue, int _level, Transform _targetPos)
    {
        this.gameObject.SetActive(true);
        Utility.instance.SliderCounting(likeabilitySilder, _sliderValue, _targetValue, () => CheckLevelUp(_targetValue, _level, _targetPos));
        likeabilityLevel.text = "LV " + (_level+1);

        Invoke(nameof(SetActiveFalseUI), 3.0f);
    }

    void CheckLevelUp(float _targetValue, int _level, Transform _targetPos)
    {
        if (_targetValue == 1)
        {
            likeabilityLevel.text = "LV " + (_level + 2);
            Instantiate(VFXManager.instance.levelUpVFX, _targetPos.position, Quaternion.identity);
            SoundManager.instance.PlaySFX(SFX.Ambience.LEVELUP);
        }
    }



    void SetActiveFalseUI()
    {
        if (this.gameObject.activeSelf)
            this.gameObject.SetActive(false);

        MainManager.instance.pureController.isLevelUp = false;
    }
}
