using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{

    private bool sceneChanged;

    private void Awake()
    {
        sceneChanged = false;
    }
    private void Update()
    {
        if (sceneChanged)
            return;

        if(Input.touchCount >= 1)
        {
            ChangeSceneToMain();
        }
    }

    void ChangeSceneToMain()
    {
        sceneChanged = true;
        SceneManager.LoadScene("Main");
    }
}
