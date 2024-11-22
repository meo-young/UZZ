using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemManager : MonoBehaviour
{
    [SerializeField] private GameObject ui_system_panel;

    private void Start()
    {
        ui_system_panel.SetActive(false);
    }

    public void InstaButton()
    {
        Application.OpenURL("https://www.instagram.com/studio.lamancha/");
    }

    public void onSystemUI()
    {
        ui_system_panel.SetActive(true);
    }

    public void offSystemUI()
    {
        ui_system_panel.SetActive(false);
    }

}
