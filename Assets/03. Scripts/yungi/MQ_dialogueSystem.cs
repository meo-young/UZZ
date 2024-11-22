
using System;
using System.Collections;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using Unity.Mathematics;
using UnityEngine.XR;
using DG.Tweening;
using UnityEngine.UI;

public class MQ_dialogSystem : MonoBehaviour
{
    [SerializeField] private int questNum;                       //퀘스트 고유 번호, 프로토타입 한정 [SerializeField] 속성 부여

    [SerializeField] private Dialogs_MainQuest dialogs;

    [SerializeField] private GameObject ui;
    [SerializeField] private TextMeshProUGUI text_name;
    [SerializeField] private TextMeshProUGUI text_dialog;

    [Header("다음 대화 버튼")]
    [SerializeField] private GameObject nextButton;

    [SerializeField] private DialogData_MQ[] dialogs_System;                //현재 분기의 대사 목록 배열
    private int dialogTotalNum;                                          //전체 대화 개수
    private int currentDialogIndex = -1;                                 //현재 대사 순번
                                                                         
    private float typingSpeed = 0.03f;                                   //텍스트 타이핑 효과의 재생속도
    private bool isTypingEffect = false;                                 //텍스트 타이핑 효과를 재생중인지

    [SerializeField] private CameraController cameraController;

    [SerializeField] private Image illust_daisy;
    [SerializeField] private Image illust_bomE;


    void Start()
    {
        
    }

    void Update()
    { 

    }

    
    private void refresh() //엑셀 값을 받아오는 작업
    {
        int index = 0;

        Array.Resize(ref dialogs_System, 100);


        for (int i = 0; i < dialogs.Entities.Count; ++i)
        {
            if (dialogs.Entities[i].questNum == questNum)
            {
                dialogs_System[index].name     = dialogs.Entities[i].name;
                dialogs_System[index].dialogue = dialogs.Entities[i].dialog;

                index++;
                dialogTotalNum = index;
            }
        }

        text_name.text = dialogs_System[0].name;
        set_illust(text_name.text);

        Array.Resize(ref dialogs_System, dialogTotalNum);
    }

    public void UpdateDialog()
    {
        nextButton.SetActive(true);

        if (isTypingEffect)
        {
            isTypingEffect = false;
            //타이핑 효과를 중지하고, 현재 대사를 출력한다.
            StopCoroutine("OnTypingText");

            text_dialog.text = dialogs_System[currentDialogIndex].dialogue;
        }
        else if (!isTypingEffect)
        {
            //대사가 남아있을경우 다음대사 진행
            if (dialogs_System.Length > currentDialogIndex + 1)
            {
                SetNextDialog();
            }
            //대사가 더 이상 없을경우 모든 오브젝트를 비활성화하고 true 반환
            else
            { 
                currentDialogIndex = -1;
                end_DialogSystem();
                
            }
        }
    }

    public void SetNextDialog()
    {
        //다음 대사 진행
        currentDialogIndex++;
        text_name.text = dialogs_System[currentDialogIndex].name;
        set_illust(text_name.text);

        StartCoroutine("OnTypingText");
    }


    private IEnumerator OnTypingText()
    {
        int index = 0;
        isTypingEffect = true;

        //텍스트를 한글자씩 타이핑치듯 재생
        while (index <= dialogs_System[currentDialogIndex].dialogue.Length)
        {
            text_dialog.text = dialogs_System[currentDialogIndex].dialogue.Substring(0, index);

            index++;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTypingEffect = false;
    }

    public void start_DialogSystem(int _id)
    {
        ui.SetActive(true);
        questNum = _id;

        refresh();

        nextButton.SetActive(true);
        UpdateDialog();

        cameraController.goLibrary();
    }

    public void end_DialogSystem()
    {
        nextButton.SetActive(false);
        ui.SetActive(false);

        cameraController.goGarret_Outside();
    }

    private void set_illust(string name)
    {
        if (name.Equals("푸르")) { illust_daisy.color = Color.white; illust_bomE.color = Color.gray; }
        else { illust_bomE.color = Color.white; illust_daisy.color = Color.gray; }
    }
}

    [System.Serializable]
public struct DialogData_MQ
{
    public string name;             //캐릭터이름
    public string expression;       //감정 표현
    [TextArea(3, 10)]
    public string dialogue;         //대사
}


