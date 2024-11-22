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
        time = 0;
        GetSetters();
        UpdateSetters();
    }

    private void OnDisable()
    {
        time = 0;
        UpdateSetters();
    }

    private void Update()
    {
        
        if (currentTime != time)
            UpdateSetters();
        if (time > 1)
        {
            time = 0;

        }
        //하단 추가코드
        if (timePlus)
        {
            time += Time.deltaTime * 0.0041f / 15f;
        }
        else if (!timePlus)
        {
            time -= Time.deltaTime * 0.0041f / 15f;
        }

        if (time < 0)
        {
            timePlus = true;
        }





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
