using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrameCheck : MonoBehaviour
{
    private float frameTime = 0f;
    private float checkTimer = 0;
    [SerializeField] private int size = 25;
    [SerializeField] private Color color = Color.red;


    private void Update()
    {
        checkTimer += Time.deltaTime;
        if (checkTimer > 1)
        {
            checkTimer = 0;
            frameTime += (Time.unscaledDeltaTime - frameTime);
        }
    }

    private void OnGUI()
    {
        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(30, 30, Screen.width, Screen.height);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = size;
        style.normal.textColor = color;

        float ms = frameTime * 1000f;
        float fps = 1.0f / frameTime;
        string text = string.Format("{0:0.} FPS ({1:0.0} ms)", fps, ms);

        GUI.Label(rect, text, style);
    }
}
