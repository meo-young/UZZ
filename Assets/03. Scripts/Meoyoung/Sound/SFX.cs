using UnityEngine;

public class SFX : MonoBehaviour
{
    public static SFX instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    [Header("# SFX Clips")]
    public AudioClip[] pureSoundClips;
    public AudioClip[] ambienceSoundClips;
    public AudioClip[] flowerSoundClips;
    public AudioClip[] dewSoundClips;

    [System.Serializable]
    public enum PureSound       // 푸르와 관련된 효과음 모음
    {
        // Pure
        IDLE = 0,
        SLEEP = 1,
        WALK = 2,
        TELEPORT = 3,
        DOWN = 4,
        SING = 5,
        WATCH = 6,
        WINDOW = 7,
        STARR = 8,
        HI = 9,
        MUNG = 10,
        SLIME = 11,
        DANCE = 12,
        FLY = 13,
        BHI = 14,
        GROOMING = 15,
        BUBI = 16,
        LIGHT = 17,
        HAND = 18,
        SCREEN = 19,
        WATERING = 20,
        SPADE = 21,
        FERTILIZER = 22,
        SCISSOR = 23,
        NUTRITIONAL = 24,
        READYGIFT = 25,
        GIVEGIFT = 26,
        BIGEVENT = 27,
        MINIEVENT = 28,
        TOUCH = 29,
    }       
    public enum Ambience
    {
        TOUCH = 0,
        TEXT = 1,
        TROUBLE = 2,
        SOLVE = 3,
        LIKE = 4,
        LEVELUP = 5
    }

    public enum Flower
    {
        TOUCH = 0,
        GLOW = 1,
        STEPUP = 2,
        LEVELUP = 3,
        GROW = 4,
        DUST = 5
    }

    public enum DEW
    {
        DROP = 0
    }
}
