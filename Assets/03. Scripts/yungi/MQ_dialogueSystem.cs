
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
    [SerializeField] private int questNum;                       //����Ʈ ���� ��ȣ, ������Ÿ�� ���� [SerializeField] �Ӽ� �ο�

    [SerializeField] private Dialogs_MainQuest dialogs;

    [SerializeField] private GameObject ui;
    [SerializeField] private TextMeshProUGUI text_name;
    [SerializeField] private TextMeshProUGUI text_dialog;

    [Header("���� ��ȭ ��ư")]
    [SerializeField] private GameObject nextButton;

    [SerializeField] private DialogData_MQ[] dialogs_System;                //���� �б��� ��� ��� �迭
    private int dialogTotalNum;                                          //��ü ��ȭ ����
    private int currentDialogIndex = -1;                                 //���� ��� ����
                                                                         
    private float typingSpeed = 0.03f;                                   //�ؽ�Ʈ Ÿ���� ȿ���� ����ӵ�
    private bool isTypingEffect = false;                                 //�ؽ�Ʈ Ÿ���� ȿ���� ���������

    [SerializeField] private CameraController cameraController;

    [SerializeField] private Image illust_daisy;
    [SerializeField] private Image illust_bomE;


    void Start()
    {
        
    }

    void Update()
    { 

    }

    
    private void refresh() //���� ���� �޾ƿ��� �۾�
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
            //Ÿ���� ȿ���� �����ϰ�, ���� ��縦 ����Ѵ�.
            StopCoroutine("OnTypingText");

            text_dialog.text = dialogs_System[currentDialogIndex].dialogue;
        }
        else if (!isTypingEffect)
        {
            //��簡 ����������� ������� ����
            if (dialogs_System.Length > currentDialogIndex + 1)
            {
                SetNextDialog();
            }
            //��簡 �� �̻� ������� ��� ������Ʈ�� ��Ȱ��ȭ�ϰ� true ��ȯ
            else
            { 
                currentDialogIndex = -1;
                end_DialogSystem();
                
            }
        }
    }

    public void SetNextDialog()
    {
        //���� ��� ����
        currentDialogIndex++;
        text_name.text = dialogs_System[currentDialogIndex].name;
        set_illust(text_name.text);

        StartCoroutine("OnTypingText");
    }


    private IEnumerator OnTypingText()
    {
        int index = 0;
        isTypingEffect = true;

        //�ؽ�Ʈ�� �ѱ��ھ� Ÿ����ġ�� ���
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
        if (name.Equals("Ǫ��")) { illust_daisy.color = Color.white; illust_bomE.color = Color.gray; }
        else { illust_bomE.color = Color.white; illust_daisy.color = Color.gray; }
    }
}

    [System.Serializable]
public struct DialogData_MQ
{
    public string name;             //ĳ�����̸�
    public string expression;       //���� ǥ��
    [TextArea(3, 10)]
    public string dialogue;         //���
}


