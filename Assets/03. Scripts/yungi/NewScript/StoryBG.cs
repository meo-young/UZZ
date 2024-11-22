using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryBG : MonoBehaviour
{




    public SpriteRenderer[] spriteRenderers;
    public float fadeDuration = 2f; // 서서히 사라지는 시간
    public float zeroAlpha = 0.0f;

    public float oneAlpha = 1.0f;

    void Start()
    {
        spriteRenderers = gameObject.GetComponentsInChildren<SpriteRenderer>();
    }

    public void OnEnable()
    {
       
        // 각 SpriteRenderer의 색상 알파값을 변경
        foreach (SpriteRenderer sr in spriteRenderers)
        {
            Color color = sr.color;
            color.a = oneAlpha;
            sr.color = color;
            sr.sortingOrder = -1;
        }
    }
    public void Off()
    {


        StartCoroutine(FadeOut());

    }

    public void Stop()
    {
        StopCoroutine(FadeOut());
    }


    // Update is called once per frame
    private IEnumerator FadeOut()
    {
        float startAlpha = spriteRenderers[0].color.a; // assuming all have the same alpha initially
        float elapsedTime = 0f;

        foreach (SpriteRenderer sr in spriteRenderers)
        {

            sr.sortingOrder = 0;
        }

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, 0f, elapsedTime / fadeDuration);

            foreach (SpriteRenderer sr in spriteRenderers)
            {
                Color color = sr.color;
                color.a = newAlpha;
                sr.color = color;
            }

            yield return null;
        }

        // Ensure alpha is set to 0
        foreach (SpriteRenderer sr in spriteRenderers)
        {
            Color color = sr.color;
            color.a = 0f;
            sr.color = color;
        }

        // Disable the game object after fading out
        gameObject.SetActive(false);
    }
}
