using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TutoDew : MonoBehaviour
{
    [SerializeField] Text dewCount;
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
