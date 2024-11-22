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

    public IEnumerator Count(float target, float current)
    {
        yield return new WaitForSeconds(1f);
        float duration = 0.5f; // 카운팅에 걸리는 시간 설정. 
        float offset = (target - current) / duration;

        while (current < target)
        {
            current += offset * Time.deltaTime;
            dewCount.text = ((int)current).ToString();
            yield return null;
        }

        current = target;
        dewCount.text = ((int)current).ToString();
    }
}
