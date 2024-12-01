using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class PureInteractionText : MonoBehaviour
{
    public static PureInteractionText instance;

    [System.Serializable]
    public struct InteractionText
    {
        public List<string> availableText;
    }
    [Header("# Text")]
    [SerializeField] List<InteractionText> pureTextSets;
    [Space(10)]
    [Header("# Dialogue")]
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] Text dialogueText;

    [SerializeField] TextAsset dialogueTextDataTable;

    private TimeType timeType => MainManager.instance.gameInfo.timeType;

    private int pureLevel => PureStat.instance.pureInfo.level;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        if (dialoguePanel.activeSelf)
            dialoguePanel.SetActive(false);
    }

    private void Start()
    {
        UpdatePureInteractionDialogue();
    }

    public void ShowRandomInteractionText()
    {
        SoundManager.instance.PlaySFX(SFX.Ambience.TEXT);
        dialoguePanel.SetActive(true);

        Vector3 textPosition = transform.position + new Vector3(0, 7f, 0);
        dialoguePanel.transform.position = textPosition;
        dialogueText.text = pureTextSets[(int)timeType].availableText[Random.Range(0,pureTextSets[(int)timeType].availableText.Count)];

        Invoke(nameof(SetActiveFalseText), 3.0f);
    }

    void SetActiveFalseText()
    {
        if(dialoguePanel != null)
            dialoguePanel.SetActive(false);
    }

    void InitializePureTextSets(int size)
    {
        pureTextSets = new List<InteractionText>(size);

        for (int i = 0; i < size; i++)
        {
            InteractionText interactionText = new InteractionText
            {
                availableText = new List<string>() // availableText 리스트 초기화
            };
            pureTextSets.Add(interactionText);
        }
    }

    public void UpdatePureInteractionDialogue()
    {
        InitializePureTextSets(4);

        StringReader reader = new StringReader(dialogueTextDataTable.text);
        bool head = false;
        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            if(!head)
            {
                head = true;
                continue;
            }
            string[] values = line.Split('\t');
            if (pureLevel + 1 < int.Parse(values[1]))
            {
                continue;
            }
            else
            {
                if (int.Parse(values[0]) == 4)
                {
                    for(int i=0; i<pureTextSets.Count; i++)
                    {
                        pureTextSets[i].availableText.Add(values[2]);
                    }
                }
                else
                {
                    pureTextSets[int.Parse(values[0])].availableText.Add(values[2]);
                }
            }
        }
    }
}
