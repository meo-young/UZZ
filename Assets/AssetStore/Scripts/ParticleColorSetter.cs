using UnityEngine;

public class ParticleColorSetter : MonoBehaviour, ColorSetterInterface
{
    [GradientUsage(true)]
    [SerializeField] Gradient gradient = null;
    [SerializeField] ParticleSystem[] particleSystems = null;

    public void Refresh()
    {
        particleSystems = GetComponentsInChildren<ParticleSystem>();
    }

    public void SetColor(float time)
    {
        foreach (var particleSystem in particleSystems)
        {

            ParticleSystem.MainModule mainModule = particleSystem.main;
            mainModule.startColor = new ParticleSystem.MinMaxGradient(gradient.Evaluate(time));
        }

    }
}