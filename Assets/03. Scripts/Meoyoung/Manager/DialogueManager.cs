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
        Choice = 1,
        Skip = 2
    }

    public enum TransitionType
    {
        FadeIn_B = 0,
        FadeOut_B = 1,
        FadeIn = 2,
        FadeOut = 3,
        Dissolve = 4,
        Popup = 5,
        Pop_FadeOut = 6
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
        public int ambience;
        public int se;
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

            // Dialog, Choice, Skip
            if (!string.IsNullOrEmpty(values[2]))
                dialogueInfos[int.Parse(values[1])].type = (Type)Enum.Parse(typeof(Type), values[2]);

            // 이름
            dialogueInfos[int.Parse(values[1])].name = values[3];

            // 대화 스크립트
            dialogueInfos[int.Parse(values[1])].dialogue1 = values[4];
            dialogueInfos[int.Parse(values[1])].dialogue2 = values[5];

            // NextIndex1,2
            dialogueInfos[int.Parse(values[1])].nextIndex1 = int.Parse(values[6]);
            if (!string.IsNullOrEmpty(values[7]))
                dialogueInfos[int.Parse(values[1])].nextIndex2 = int.Parse(values[7]);

            // Background
            dialogueInfos[int.Parse(values[1])].backgroundIndex = int.Parse(values[8]);

            // Transition
            if (!string.IsNullOrEmpty(values[9]))
                dialogueInfos[int.Parse(values[1])].transition = (int)(TransitionType)Enum.Parse(typeof(TransitionType), values[9]);
            else
                dialogueInfos[int.Parse(values[1])].transition = -1;

            // Ambience
            if (!string.IsNullOrEmpty(values[10]))
                dialogueInfos[int.Parse(values[1])].ambience = int.Parse(values[10]);
            else
                dialogueInfos[int.Parse(values[1])].ambience = -1;

            // SE
            if (!string.IsNullOrEmpty(values[11]))
                dialogueInfos[int.Parse(values[1])].se = int.Parse(values[11]);
            else
                dialogueInfos[int.Parse(values[1])].se = -1;
        }
    }
}
