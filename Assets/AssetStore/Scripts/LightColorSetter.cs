using UnityEngine;


public class LightColorSetter : MonoBehaviour, ColorSetterInterface
{
    [SerializeField] Gradient gradient = null;

    public UnityEngine.Rendering.Universal.Light2D[] lights;

    public void Refresh()
    {
        
    }

    public void SetColor(float time)
    {
        foreach (var light in lights)
            light.color = gradient.Evaluate(time);
    }
}
