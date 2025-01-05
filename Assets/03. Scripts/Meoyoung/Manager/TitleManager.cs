using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [Header("# Tutorial Checker")]
    [SerializeField] TutorialChecker tutoChecker;
    [SerializeField] TutorialDataManager tutoDataManager;

    [Header("# Scene info")]
    [SerializeField] string sceneName;
    [SerializeField] string tutorialName;
    private bool sceneChanged;

    private void Awake()
    {
        Application.targetFrameRate = 60;
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

        SceneManager.LoadScene(sceneName);


        /*if (tutoChecker.flag)
        {
            Destroy(tutoChecker.gameObject);
            Destroy(tutoDataManager.gameObject);
            SceneManager.LoadScene(sceneName);
        }
        else 
            SceneManager.LoadScene(tutorialName);*/
    }
}
