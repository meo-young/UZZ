using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DewUI : MonoBehaviour
{
    [Header("# Dew")]
    [SerializeField] Image dewImage;
    [SerializeField] Text dewCount;
    [SerializeField] float dewTextAnimationTime;

    private void Start()
    {
        dewCount.text = MainManager.instance.gameInfo.dew.ToString();
    }

    public void Count(float _point)
    {
        StartCoroutine(CountCoroutine(_point));
        MainManager.instance.gameInfo.dew += _point;
    }


    private IEnumerator CountCoroutine(float _point)
    {
        float current = MainManager.instance.gameInfo.dew;
        float target = MainManager.instance.gameInfo.dew + _point;

        float duration = 0.5f; // 카운팅에 걸리는 시간 설정. 

        float offset;
        if (target > current)
        {
            yield return new WaitForSeconds(1f);

            offset = (target - current) / duration;
            while (current < target)
            {
                current += offset * Time.deltaTime;
                if (current > target)
                    break;

                dewCount.text = ((int)current).ToString();
                yield return null;
            }
        }
        else
        {
            offset = (current - target) / duration;
            while (current > target)
            {
                current -= offset * Time.deltaTime;
                if (current < target)
                    break;

                dewCount.text = ((int)current).ToString();
                yield return null;
            }
        }
        current = target;
        dewCount.text = ((int)current).ToString();
    }
}
