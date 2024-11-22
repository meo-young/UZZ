using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteColorSetter : MonoBehaviour, ColorSetterInterface
{
    [GradientUsage(true)]
    [SerializeField] Gradient gradient = null;
    [SerializeField] SpriteRenderer[] spritesRenderers = null;

    public void Refresh()
    {
        spritesRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    public void SetColor(float time)
    {
        foreach (var spriteRenderer in spritesRenderers)
        {



            spriteRenderer.color = gradient.Evaluate(time);

        }

    }
}