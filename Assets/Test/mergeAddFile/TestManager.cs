using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
   
    [SerializeField] private GameObject ui_box_panel;

    public Item[] item;

    public enum itemName
    {
        Ring,
        Key
    }
    private void Start()
    {
        ui_box_panel.SetActive(false);  
    }

  

    private void Update()
    {
        
    }

    public void onBoxUI()
    {
        ui_box_panel.SetActive(true);
    }

    public void offBoxUI()
    {
        ui_box_panel.SetActive(false);
    }
}
