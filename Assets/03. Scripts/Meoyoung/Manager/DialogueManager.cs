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

    public struct DialogueInfo
    {
        public int branch;
        public Type type;
        public string name;
        public string dialogue1;
        public string dialogue2;
        public int nextIndex1;
        public int nextIndex2;
    }

    [Header("# Dialogue")]
    [SerializeField] TextAsset dialogueDataTable;
    public DialogueInfo[] dialogueInfos;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        dialogueInfos = new DialogueInfo[100];
    }
    private void Start()
    {
        UpdateDialogueData();
    }


    public void UpdateDialogueData()
    {
        StringReader reader = new StringReader(dialogueDataTable.text);
        bool head = false;
        bool head2 = false;
        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            if (!head)
            {
                head = true;
                continue;
            }

            if(!head2)
            {
                head2 = true;
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
        }

        //Check();
    }


    // For Debug
    void Check()
    {
        for(int i=0; i<dialogueInfos.Length; i++)
        {
            //Debug.Log(dialogueInfos[i].branch);
            //Debug.Log(dialogueInfos[i].name);
            Debug.Log(dialogueInfos[i].nextIndex2);

        }
    }
}
