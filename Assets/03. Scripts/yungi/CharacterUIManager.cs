using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUIManager : MonoBehaviour
{
    [SerializeField] private GameObject ui_characterInfo;
    //[SerializeField] private TextMeshProUGUI text_name;
    //[SerializeField] private TextMeshProUGUI text_age;

    //[SerializeField] private TextMeshProUGUI text_exp_level;
    //[SerializeField] private TextMeshProUGUI text_exp_value;

    //[SerializeField] private TextMeshProUGUI text_intel_level;
    //[SerializeField] private TextMeshProUGUI text_intel_value;

    //[SerializeField] private TextMeshProUGUI text_sens_level;
    //[SerializeField] private TextMeshProUGUI text_sens_value;

    //[SerializeField] private TextMeshProUGUI text_reas_level;
    //[SerializeField] private TextMeshProUGUI text_reas_value;

    //[SerializeField] private Slider slider_exp;
    //[SerializeField] private Slider slider_intel;
    //[SerializeField] private Slider slider_sens;
    //[SerializeField] private Slider slider_reas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void set_CharacterInfo(string _name, int _age,
                                  int _exp_level, int _exp_value, int _exp_value_max,
                                  int _intel_level, int _intel_value, int _intel_value_max,
                                  int _sens_level, int _sens_value,   int _sens_value_max,
                                  int _reas_level, int _reas_value,   int _reas_value_max)
    {
        /*
        text_name.text = "이름 : " + _name;
        text_age.text = "나이 : " + _age.ToString();
        text_exp_level.text = "Lv : " + _exp_level.ToString();
        text_exp_value.text = _exp_value.ToString() + " / " + _exp_value_max.ToString();
        
        text_intel_level.text = "Lv : " + _intel_level.ToString();
        text_intel_value.text = _intel_value.ToString() + " / " + _intel_value_max.ToString();
        
        text_sens_level.text = "Lv : " + _sens_level.ToString();
        text_sens_value.text = _sens_value.ToString() + " / " + _sens_value_max.ToString();

        text_reas_level.text = "Lv : " + _reas_level.ToString();
        text_reas_value.text = _reas_value.ToString() + " / " + _reas_value_max.ToString();

        slider_exp.maxValue = _exp_value_max;
        slider_exp.value = _exp_value;

        slider_intel.maxValue = _intel_value_max;
        slider_intel.value = _intel_value;

        slider_sens.maxValue = _sens_value_max;
        slider_sens.value = _sens_value;

        slider_reas.maxValue = _reas_value_max;
        slider_reas.value = _reas_value;
        */
    }

    public void on_CharacterInfo()
    {
        ui_characterInfo.SetActive(true);
    }

    public void off_CharacterInfo()
    {
        ui_characterInfo.SetActive(false);
    }
}
