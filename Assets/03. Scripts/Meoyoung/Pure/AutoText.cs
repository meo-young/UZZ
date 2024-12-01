using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;


public class AutoText : MonoBehaviour
{
    public List<List<string>> baseAutoText = new();
    public List<List<string>> interactionAutoText = new();
    public List<List<string>> fieldWorkAutoText = new();

    [SerializeField] TextAsset autoTextDataTable;
    [SerializeField] public float autoTextCounter;

    [Header("# Dialogue")]
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] Text dialogueText;
    [SerializeField] Vector3 dialoguePos;
    private void Awake()
    {
        for(int i=0; i<2; i++)
        {
            baseAutoText.Add(new List<string>());
            for (int j = 0; j < 2; j++)
                baseAutoText[i].Add(string.Empty);
        }
        for (int i = 0; i < 5; i++)
        {
            interactionAutoText.Add(new List<string>());
            for (int j = 0; j < 2; j++)
                interactionAutoText[i].Add(string.Empty);
        }
        for (int i = 0; i < 5; i++)
        {
            fieldWorkAutoText.Add(new List<string>());
            for (int j = 0; j < 2; j++)
                fieldWorkAutoText[i].Add(string.Empty);
        }
    }
    private void Start()
    {
        UpdateAutoDialogue();
    }
    void UpdateAutoDialogue()
    {
        StringReader reader = new StringReader(autoTextDataTable.text);
        bool head = false;
        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            if (!head)
            {
                head = true;
                continue;
            }
            string[] values = line.Split('\t');
            
            string type = values[0];
            int index = int.Parse(values[1]);
            int animationIndex = int.Parse(values[2]);
            string text = values[3];

            switch(type)
            {
                case "0":
                    baseAutoText[index][animationIndex] = text;
                    break;
                case "1":
                    interactionAutoText[index][animationIndex] = text;
                    break;
                case "2":
                    fieldWorkAutoText[index][animationIndex] = text;
                    break;
            }
        }
    }

    public void ShowIdleRandomText(Transform _targetTrans, int type)
    {
        SoundManager.instance.PlaySFX(SFX.Ambience.TEXT);
        dialoguePanel.SetActive(true);

        Vector3 textPosition = _targetTrans.position + dialoguePos;
        dialoguePanel.transform.position = textPosition;

        int randNum = Random.Range(0, baseAutoText[type].Count);
        dialogueText.text = baseAutoText[type][randNum];

        Invoke(nameof(SetActiveFalseText), 3.0f);
    }
    
    public void ShowInteractionText(Transform _targetTrans, int type)
    {
        dialoguePanel.SetActive(true);

        Vector3 textPosition = _targetTrans.position + dialoguePos;
        dialoguePanel.transform.position = textPosition;

        int randNum = Random.Range(0, interactionAutoText[type].Count);
        dialogueText.text = interactionAutoText[type][randNum];

        Invoke(nameof(SetActiveFalseText), 3.0f);
    }

    public void ShowFieldWorkText(Transform _targetTrans, int type)
    {
        dialoguePanel.SetActive(true);

        Vector3 textPosition = _targetTrans.position + dialoguePos;
        dialoguePanel.transform.position = textPosition;

        int randNum = Random.Range(0, fieldWorkAutoText[type].Count);
        dialogueText.text = fieldWorkAutoText[type][randNum];

        Invoke(nameof(SetActiveFalseText), 3.0f);
    }


    void SetActiveFalseText()
    {
        if(dialoguePanel != null)
            dialoguePanel.SetActive(false);
    }
}
