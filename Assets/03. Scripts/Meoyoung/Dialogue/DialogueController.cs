using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static DialogueManager;

public class DialogueController : MonoBehaviour
{
    [SerializeField] int currentIndex;

    [Header("# Dialogue UI Info")]
    [SerializeField] GameObject firstChoicePanel;
    [SerializeField] GameObject secondChoicePanel;
    [SerializeField] Text firstChoiceText;
    [SerializeField] Text secondChoiceText;
    [SerializeField] Text dialogueText;
    [SerializeField] Text nameText;

    private DialogueInfo[] dialogues => DialogueManager.instance.dialogueInfos;

    private void Awake()
    {
        OffChoicePanel();
    }


    private void Start()
    {
        currentIndex = 0;
    }

    public void OnFirstChoiceHandler()
    {
        currentIndex = dialogues[currentIndex].nextIndex1;
        ChangeDialogueText();
    }

    public void OnSecondChoiceHandler()
    {
        OffChoicePanel();

        if (dialogues[currentIndex].nextIndex2 == 0)
        {
            currentIndex = dialogues[currentIndex].nextIndex1;
            ChangeDialogueText();
        }
        else
        {
            currentIndex = dialogues[currentIndex].nextIndex2;
            ChangeDialogueText();
        }
    }

    public void OnNextBtnHandler()
    {
        if (dialogues[currentIndex].type == Type.Choice)
            return;

        currentIndex++;
        ChangeDialogueText();
    }
    void OnChoicePanel()
    {
        OffChoicePanel();

        if (!firstChoicePanel.activeSelf)
            firstChoicePanel.SetActive(true);

        if (!secondChoicePanel.activeSelf)
        {
            if (dialogues[currentIndex].dialogue2 != "")
                secondChoicePanel.SetActive(true);
        }
    }
    void OffChoicePanel()
    {
        if (firstChoicePanel.activeSelf)
            firstChoicePanel.SetActive(false);

        if (secondChoicePanel.activeSelf)
            secondChoicePanel.SetActive(false);
    }

    void ChangeDialogueText()
    {
        if (dialogues[currentIndex].type == Type.Choice)
        {
            firstChoiceText.text = dialogues[currentIndex].dialogue1;
            secondChoiceText.text = dialogues[currentIndex].dialogue2;

            OnChoicePanel();
        }
        else
        {
            OffChoicePanel();
            dialogueText.text = dialogues[currentIndex].dialogue1;
        }
    }

    void CheckLastDialogue()
    {
        if (dialogues[currentIndex].nextIndex1 == 0)
            return;
    }
}
