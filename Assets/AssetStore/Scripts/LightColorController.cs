using UnityEngine;
using UnityEngine.UI;

public interface ColorSetterInterface
{
    void Refresh();

    void SetColor(float time);
}

[ExecuteInEditMode]
public class LightColorController : MonoBehaviour
{
    [SerializeField][Range(0, 1)] public float time;

    public Slider timeSlider;

    private bool timePlus = true;
    [SerializeField]
    private ColorSetterInterface[] setters;
    private float currentTime = 0;

    public float timeValue => currentTime;

    public void GetSetters()
    {
        setters = GetComponentsInChildren<ColorSetterInterface>();
        foreach (var setter in setters)
            setter.Refresh();
    }

    private void OnEnable()
    {
        GetSetters();
        UpdateSetters();
    }

    private void OnDisable()
    {
        UpdateSetters();
    }

    public void UpdateSetters()
    {
        currentTime = time;

        foreach (var setter in setters)
            setter.SetColor(time);
    }

    public void TimeControl()
    {
        time = timeSlider.value;
       
        
    }
}
