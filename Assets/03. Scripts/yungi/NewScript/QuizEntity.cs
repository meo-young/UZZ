using UnityEngine;

[System.Serializable]
public class QuizEntity
{
    public int index;

    public DType type;
    [TextArea]
    public string dialog;
    public bool answer;
    
}
