using UnityEngine;

public class BGM : MonoBehaviour
{
    public static BGM instance;
    [Header("# BGM Clips")]
    public AudioClip[] titleClips;


    public enum Title
    { 
        DEFAULT = 0
    }
}
