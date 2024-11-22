using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingManager : MonoBehaviour
{
    [SerializeField] private GameObject UI_Setting;

    public void OnUISetting()
    {
        UI_Setting.SetActive(true);
    }

    public void OffUISetting()
    {
        UI_Setting.SetActive(false);
    }
}
