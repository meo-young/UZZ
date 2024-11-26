using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    public enum Type
    {
        Dialog = 0,
        Choice = 1
    }

    public enum TransitionType
    {
        FadeIn_B = 0,
        FadeOut_B = 1,
    }

    public struct DialogueInfo
    {
        public int branch;
        public Type type;
        public string name;
        public string dialogue1;
        public string dialogue2;
        public int nextIndex1;
        public int nextIndex2;
        public int backgroundIndex;
        public int transition;
    }

    [Header("# Dialogue")]
    [SerializeField] TextAsset dialogueDataTable;
    public DialogueInfo[] dialogueInfos;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        dialogueInfos = new DialogueInfo[100];
        UpdateDialogueData();
    }



    public void UpdateDialogueData()
    {
        StringReader reader = new StringReader(dialogueDataTable.text);
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
            if (!string.IsNullOrEmpty(values[2]))
                dialogueInfos[int.Parse(values[1])].type = (Type)Enum.Parse(typeof(Type), values[2]);
            dialogueInfos[int.Parse(values[1])].name = values[3];
            dialogueInfos[int.Parse(values[1])].dialogue1 = values[4];
            dialogueInfos[int.Parse(values[1])].dialogue2 = values[5];
            dialogueInfos[int.Parse(values[1])].nextIndex1 = int.Parse(values[6]);
            if (!string.IsNullOrEmpty(values[7]))
                dialogueInfos[int.Parse(values[1])].nextIndex2 = int.Parse(values[7]);
            dialogueInfos[int.Parse(values[1])].backgroundIndex = int.Parse(values[8]);
            if (!string.IsNullOrEmpty(values[9]))
                dialogueInfos[int.Parse(values[1])].transition = (int)(TransitionType)Enum.Parse(typeof(TransitionType), values[9]);
            else
                dialogueInfos[int.Parse(values[1])].transition = -1;
            Debug.Log(dialogueInfos[int.Parse(values[1])].transition);
        }
    }
}
