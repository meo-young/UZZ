using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    [SerializeField] private GameObject uiMiniGame;

    [SerializeField] private GameObject QuizPanel;
    [SerializeField] private GameObject TigerPanel;
    [SerializeField] private GameObject MeteorPanel;
    [SerializeField] private GameObject OgeulaePanel;
    [SerializeField] private GameObject PruePanel;
    [SerializeField] private GameObject HenryPanel;
    [SerializeField] private GameObject MerlruPanel;


    
    [SerializeField] private CameraController cameraController; //얘 카메라 컨트롤러에서도 접근함. 코드 다시 짜야 돼 이 스크립트.

    int i = 0;
    void Start()
    {
      
        
    }


    void Update()
    {
        
    }

    public void RandomGame()
    {

        
    }
    public void OnUIMiniGame_Garret()
    {
        i = Random.Range(0, 3);

        uiMiniGame.SetActive(true);
        cameraController.miniGamePossible.SetActive(false);
        switch (i)
        {      
            case 0:
                QuizPanel.SetActive(true);              
                break;
            case 1:
                TigerPanel.SetActive(true);
                break;
            case 2:
                MeteorPanel.SetActive(true); 
                break;
            
            

        }
        testPanel.SetActive(false);
        GameManager.Instance.OnOffUIStart(false);
    }

    public void OnUIMiniGame_Garret_Test(int a)
    {
        i = a;

        uiMiniGame.SetActive(true);
        cameraController.miniGamePossible.SetActive(false);
        switch (i)
        {
            case 0:
                QuizPanel.SetActive(true);
                break;
            case 1:
                TigerPanel.SetActive(true);
                break;
            case 2:
                MeteorPanel.SetActive(true);
                break;



        }
        testPanel.SetActive(false);
        GameManager.Instance.OnOffUIStart(false);
    }
    public void OnUIMiniGame_Work()
    {
        i = 0;

        uiMiniGame.SetActive(true);
        cameraController.miniGamePossible_work.SetActive(false);
        switch (i)
        {
            
            case 0:
                PruePanel.SetActive(true);
                break;
            case 1:
                HenryPanel.SetActive(true);
                break;
            case 2:
                MerlruPanel.SetActive(true);
                break;
            default:
                break;

        }
        GameManager.Instance.OnOffUIStart(false);
    }
    
    public void OnUIMiniGame_Test()
    {
        i = 3;

        uiMiniGame.SetActive(true);
        cameraController.miniGamePossible.SetActive(false);
        switch (i)
        {
            case 0:
                QuizPanel.SetActive(true);
                break;
            case 1:
                TigerPanel.SetActive(true);
                break;
            case 2:
                MeteorPanel.SetActive(true);
                break;
            case 3:
                OgeulaePanel.SetActive(true);
                cameraController.miniGamePossible_ogurae.SetActive(false);
                break;


        }
        GameManager.Instance.OnOffUIStart(false);
    }


    public void OffUIMiniGame()
    {
        uiMiniGame.SetActive(false);
        
        QuizPanel.SetActive(false);
        
        TigerPanel.SetActive(false);
        
        MeteorPanel.SetActive(false);      
        
        OgeulaePanel.SetActive(false);
        
        PruePanel.SetActive(false);
        
        HenryPanel.SetActive(false);
        
        MerlruPanel.SetActive(false);
        
        

        
        

    }
    [SerializeField] GameObject testPanel;

    public void TestOn()
    {
        testPanel.SetActive(true);
    }
    public void TestOff()
    {
        testPanel.SetActive(false);
    }

}
