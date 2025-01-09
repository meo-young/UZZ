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

    [Header("#Main BGM")]
    [SerializeField] GameObject morning;
    [SerializeField] GameObject lunch;
    [SerializeField] GameObject evening;
    [SerializeField] GameObject night;

    public float timeValue => currentTime;

    private void Update()
    {
        if (time >= 0.1 && time < 0.35)
            ControlBGMObject(0);
        else if(time >= 0.35 && time < 0.6)
            ControlBGMObject(1);
        else if(time >= 0.6 && time <0.85)
            ControlBGMObject(2);
        else
            ControlBGMObject(3);
    }

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

    void ControlBGMObject(int index)
    {
        switch (index)
        {
            case 0:
                morning.SetActive(true);
                lunch.SetActive(false);
                evening.SetActive(false);
                night.SetActive(false);
                break;
            case 1:
                morning.SetActive(false);
                lunch.SetActive(true);
                evening.SetActive(false);
                night.SetActive(false);
                break;
            case 2:
                morning.SetActive(false);
                lunch.SetActive(false);
                evening.SetActive(true);
                night.SetActive(false);
                break;
            case 3:
                morning.SetActive(false);
                lunch.SetActive(false);
                evening.SetActive(false);
                night.SetActive(true);
                break;
        }
    }
}
