using System;
using System.Collections;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using Unity.Mathematics;
using UnityEngine.XR;
using DG.Tweening;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    [SerializeField]
    private int presentCharacterID;   //���� ��ȭ�ϴ� ĳ������ ���� ��ȣ
    [SerializeField] private int text_Type;
    [SerializeField] private int branch;
   
    [SerializeField] private UZZ_DataTable dialogs;

    [SerializeField] private GameObject talks;


    [Header("������ ��ȭ")]
    [SerializeField] private GameObject      ui_talk_gardener;
    [SerializeField] private TextMeshProUGUI text_GardenerName;
    [SerializeField] private TextMeshProUGUI text_GardenerTalk;

    [Header("���� ��ȭ")]
    [SerializeField] private GameObject      ui_talk_spirit;
    [SerializeField] private TextMeshProUGUI text_SpiritName;
    [SerializeField] private TextMeshProUGUI text_SpiritTalk;

    [Header("���� �� ��ȭ")]
    [SerializeField] private GameObject      ui_talk_work;
    [SerializeField] private TextMeshProUGUI text_name;
    [SerializeField] private TextMeshProUGUI text_workTalk;

    //[Header("����Ʈ ��ȭ")]
    //[SerializeField] private GameObject ui_quest;
    //[SerializeField] private GameObject ui_quest_talk;
    //[SerializeField] private TextMeshProUGUI text_QuestName;
    //[SerializeField] private TextMeshProUGUI text_QuestTalk;

    [Header("���� ��ȭ ��ư")]
    [SerializeField] private GameObject nextButton;


    [Header("Blind")]
    [SerializeField] private GameObject blind;

    [SerializeField]
    private bool isTalk = false;
    [SerializeField]
    private float offTimer = 3;

    public enum TalkType
    {
        Gardener,
        Spirit,
        Work,
        Quest
    }

    private TalkType talkType;

    [SerializeField] private DialogData[] dialogs_System;                //���� �б��� ��� ��� �迭
    private int dialogTotalNum;
    //��ü ��ȭ ����
    [SerializeField]
    private int currentDialogIndex = -1;                                 //���� ��� ����
                                                                         
    private float typingSpeed = 0.03f;                                   //�ؽ�Ʈ Ÿ���� ȿ���� ����ӵ�

    [SerializeField]
    private bool isTypingEffect = false;                                 //�ؽ�Ʈ Ÿ���� ȿ���� ���������
    [SerializeField]
    public bool isEnd = true;

    void Start()
    {
        text_Type = 0;
        branch = 0;
        talks.SetActive(false);
    }

    private void Update()
    {
        if(!isEnd)
        {
            offTimer -= Time.deltaTime;
            
            if(offTimer < 0)
            {

                offTimer = 3;
                end_DialogSystem();
               
                //�ٲ���
            }
        }
    }


    private void refresh() //���� ���� �޾ƿ��� �۾�
    {
        int index = 0;

        Array.Resize(ref dialogs_System, 100);

        //if (text_Type == 4) //�ϻ��ȭ�� ��
        //{
        //    branch = UnityEngine.Random.Range(0, 3);
        //}
        //else
        //{
        //    branch = 0;
        //}
       
        int count = 0;
        for (int i = 0; i < dialogs.DialogueTextTable.Count; ++i)
        {
            if (dialogs.DialogueTextTable[i].id == presentCharacterID && dialogs.DialogueTextTable[i].text_Type == text_Type )
            {
                count = dialogs.DialogueTextTable[i].branch;
          
            }
        }
        branch = UnityEngine.Random.Range(0, count+1);
        for (int i = 0; i < dialogs.DialogueTextTable.Count; ++i)
        {
            if (dialogs.DialogueTextTable[i].id == presentCharacterID && dialogs.DialogueTextTable[i].text_Type == text_Type && dialogs.DialogueTextTable[i].branch == branch)
            {
                dialogs_System[index].name     = dialogs.DialogueTextTable[i].character_Name;
                dialogs_System[index].dialogue = dialogs.DialogueTextTable[i].text_Kor;

                index++;
                dialogTotalNum = index;
            }
        }

        if (talkType == TalkType.Gardener) { text_GardenerName.text = dialogs_System[0].name; }
        else if (talkType == TalkType.Spirit) { text_SpiritName.text = dialogs_System[0].name; }
        else if (talkType == TalkType.Work) { text_name.text = dialogs_System[0].name; }
        //else if (talkType == TalkType.Quest) { text_QuestName.text = dialogs_System[0].name; }

        Array.Resize(ref dialogs_System, dialogTotalNum);
    }

    public void UpdateDialog()
    {
        offTimer = 3;
       
       
        nextButton.SetActive(true);
        isEnd = false;
        if (isTypingEffect)
        {
            isTypingEffect = false;
            //Ÿ���� ȿ���� �����ϰ�, ���� ��縦 ����Ѵ�.
            StopCoroutine("OnTypingText");
            

            if (talkType == TalkType.Work) 
            {
                text_workTalk.text = dialogs_System[currentDialogIndex].dialogue;
            }
            else if (talkType == TalkType.Spirit)
            {
                text_SpiritTalk.text = dialogs_System[currentDialogIndex].dialogue;
            }
            else if (talkType == TalkType.Gardener)
            {
                Debug.Log(currentDialogIndex);
                text_GardenerTalk.text = dialogs_System[currentDialogIndex].dialogue;
            }
            else if (talkType == TalkType.Quest)
            {
                //text_QuestTalk.text = dialogs_System[currentDialogIndex].dialogue;
            }
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

        StartCoroutine("OnTypingText");
    }


    private IEnumerator OnTypingText()
    {
        int index = 0;
        isTypingEffect = true;
        

        //�ؽ�Ʈ�� �ѱ��ھ� Ÿ����ġ�� ���
        while (index <= dialogs_System[currentDialogIndex].dialogue.Length)
        {
            if (talkType == TalkType.Gardener)
            {
                text_GardenerTalk.text = dialogs_System[currentDialogIndex].dialogue.Substring(0, index);
                
            }
            else if (talkType == TalkType.Spirit)
            {
                text_SpiritTalk.text = dialogs_System[currentDialogIndex].dialogue.Substring(0, index);
                
            }
            else if (talkType == TalkType.Work)
            {
                text_workTalk.text = dialogs_System[currentDialogIndex].dialogue.Substring(0, index);
            }
            else if (talkType == TalkType.Quest)
            {
                //text_QuestTalk.text = dialogs_System[currentDialogIndex].dialogue.Substring(0, index);
            }

            index++;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTypingEffect = false;
        
    }

    public void start_DialogSystem(int _id, int _text_Type, TalkType _talkType)
    {
        
        if(currentDialogIndex != -1)
        {
            return;
        }
        presentCharacterID = _id;
        text_Type = _text_Type;
        talkType = _talkType;

        

        talks.SetActive(true);


        if (talkType == TalkType.Gardener)
        {
            ui_talk_gardener.SetActive(true);
            ui_talk_gardener.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            ui_talk_gardener.transform.DOScale(1.0f, 0.5f).SetEase(Ease.OutBack);
        }
        else if (talkType == TalkType.Spirit)
        {
            ui_talk_spirit.SetActive(true);
            ui_talk_spirit.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            ui_talk_spirit.transform.DOScale(1.0f, 0.5f).SetEase(Ease.OutBack);
        }
        else if (talkType == TalkType.Work)
        {
            ui_talk_work.SetActive(true);
            ui_talk_work.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            ui_talk_work.transform.DOScale(1.0f, 0.5f).SetEase(Ease.OutBack);
        }
        else if (talkType == TalkType.Quest)
        {
            //ui_quest.SetActive(true);
            //ui_quest_talk.SetActive(true);
            //ui_quest_talk.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            //ui_quest_talk.transform.DOScale(1.0f, 0.5f).SetEase(Ease.OutBack);
        }
        refresh();

        nextButton.SetActive(true);
        UpdateDialog(); 
        
        blind.SetActive(true);
    }

    public void end_DialogSystem()
    {
        
        isEnd = true;
        ui_talk_gardener.SetActive(false);
        ui_talk_spirit.SetActive(false);
        ui_talk_work.SetActive(false);
        //ui_quest.SetActive(false);
        nextButton.SetActive(false);
       

        blind.SetActive(false);
        talks.SetActive(false);
    }

    
}

[System.Serializable]
public struct DialogData
{
    //public int speakerIndex;        //�̸��� ��縦 ����� ���� DialogSystem�� speakers �迭 ����
    public string name;             //ĳ�����̸�
    public string expression;       //���� ǥ��
    [TextArea(3, 10)]
    public string dialogue;         //���
}


