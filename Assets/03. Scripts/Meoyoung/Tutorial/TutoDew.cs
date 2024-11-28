using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TutoDew : MonoBehaviour
{
    [SerializeField] Text dewCount;
    public IEnumerator Count(float target, float current)
    {
        yield return new WaitForSeconds(1f);
        float duration = 0.5f; // ī���ÿ� �ɸ��� �ð� ����. 
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
