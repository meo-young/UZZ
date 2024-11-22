using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckResoulution : MonoBehaviour
{
    private int screenWidth;
    private int screenHeight;
    public Text text;
    private void Start()
    {
        int screenWidth = Screen.width;
        int screenHeight = Screen.height;

    }

    private void Update()
    {
        Debug.Log("Screen Resolution: " + screenWidth + "x" + screenHeight);
        text.text = "Screen Resolution: " + screenWidth + "x" + screenHeight;
    }
}
