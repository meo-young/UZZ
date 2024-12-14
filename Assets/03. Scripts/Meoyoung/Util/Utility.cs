using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Utility : MonoBehaviour
{
    public static Utility instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void SliderCounting(Slider _slider, float _startValue, float _targetValue, Action CallBack)
    {
        StartCoroutine(SliderCountingCoroutine(_slider,_startValue, _targetValue, CallBack));
    }

    IEnumerator SliderCountingCoroutine(Slider _slider, float _startValue, float _targetValue, Action CallBack)
    {
        float start = _startValue;
        float target = _targetValue;
        float elapsedValue = (_targetValue - _startValue) / 1f;


        while (start < target)
        {
            start += elapsedValue * Time.deltaTime;

            Debug.Log(start);

            if (start > target)
                break;

            _slider.value = start;
            yield return null;
        }

        if (target == 1)
            _slider.value = 0f;
        else
            _slider.value = target;

        CallBack();
    }
}
