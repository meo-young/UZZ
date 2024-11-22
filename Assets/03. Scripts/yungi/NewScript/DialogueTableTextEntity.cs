using UnityEngine;

[System.Serializable]
public class DialogueTextTableEntity
{
    public int index;
    public int id;
    public string character_Name;
    public int text_Type;
    public int branch;
  
    public int subBranch;
    [TextArea]
    public string text_Kor;
 
    

}

