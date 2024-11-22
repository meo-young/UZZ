using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondSprite : MonoBehaviour
{
    [GradientUsage(true)]
    [SerializeField] Gradient gradient = null;
    [SerializeField] SpriteRenderer[] spritesRenderers = null;

    public float totalTime = 10f;
    public float currentTime = 0f;
    void Start()
    {
        spritesRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > totalTime)
            currentTime =  0f;

        float normalizedTime = currentTime / totalTime;

        foreach (var spriteRenderer in spritesRenderers)
        {

            spriteRenderer.color = gradient.Evaluate(normalizedTime);

        }
    }
}
