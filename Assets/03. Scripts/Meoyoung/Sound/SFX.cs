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
        IDLE = 0,           // 기본
        SLEEP = 1,          // 졸기
        WALK = 2,           // 걷기
        TELEPORT = 3,       // 텔레포트
        DOWN = 4,           // 기운없기
        SING = 5,           // 노래부르기
        WATCH = 6,          // 꽃 쳐다보기
        WINDOW = 7,         // 창가 밖 바라보기
        STARR = 8,          // 앞에 빤히 쳐다보기
        HI = 9,             // 인사
        MUNG = 10,          // 멍때리기
        SLIME = 11,         // 슬라임 톡톡 치기
        DANCE = 12,         // 춤추기
        FLY = 13,           // 둥실둥실 떠다니기
        BHI = 14,           // 밝은 인사
        GROOMING = 15,      // 그루밍
        BUBI = 16,          // 부비부비
        LIGHT = 17,         // 손에서 빛 ??...
        HAND = 18,          // 사용자가 푸르를 잡고 들었을 때
        SCREEN = 19,        // ???
        WATERING = 20,      // 작업 : 물뿌리개
        SPADE = 21,         // 작업 : 삽
        FERTILIZER = 22,    // 작업 : 비료
        SCISSOR = 23,       // 작업 : 가위
        NUTRITIONAL = 24,   // 작업 : 영양제
        READYGIFT = 25,     // 선물 준비
        GIVEGIFT = 26,      // 선물 주기
        BIGEVENT = 27,      // 꽃 빅 이벤트
        MINIEVENT = 28,     // 꽃 미니 이벤트
        TOUCH = 29,         // 푸르 터치
    }       
    public enum Ambience
    {
        TOUCH = 0,          // GUI 터치
        TEXT = 1,           // UI, 텍스트 출력시
        TROUBLE = 2,        // 작업도움
        SOLVE = 3,          // 작업도움 해결
        LIKE = 4,           // 호감도
        LEVELUP = 5         // 푸르 레벨업
    }

    public enum Flower
    {  
        TOUCH = 0,          // 꽃 터치
        GLOW = 1,           // 꽃 수확
        STEPUP = 2,         // 스텝업
        LEVELUP = 3,        // 레벨업
        GROW = 4,           // 성장치 획득
        DUST = 5            // 먼지 이벤트 출력
    }

    public enum DEW
    {
        DROP = 0            // 이슬 획득
    }
}
