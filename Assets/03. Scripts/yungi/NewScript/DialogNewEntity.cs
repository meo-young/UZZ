using UnityEngine;

[System.Serializable]
public class DialogNewEntity
{
    public int branch;

    public int index;
    public string name;
    public DType type;
    [TextArea]
    public string dialog;
    public string dialog2;

    public int nextIndex;
    public int nextIndex2;
    public string bgm;
    public string sfx;
    public string transition;
    public int background;
    public float bgsize;

    public float transform_x;
    public float transform_y;
    public float size;

    public float des_x;
    public float des_y;
    public float des_speed; 
    public float zoom_size;
    public float zoom_speed;

    public int font_size;
    public float delay;
}
