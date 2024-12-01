using UnityEngine;

public class BGM : MonoBehaviour
{
    public static BGM instance;
    [Header("# BGM Clips")]
    public AudioClip[] titleClips;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public enum Title
    { 
        DEFAULT = 0
    }
}
