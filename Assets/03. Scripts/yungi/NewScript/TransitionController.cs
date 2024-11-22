using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GlobalValue
{
    public enum Transition
    {
        Fade_B,FadeIn_B,FadeOut_B, Fade_W, FadeIn_W, FadeOut_W,
    }
}
public class TransitionController : MonoBehaviour
{
    // 싱글톤 대신 static 쓰기 위해, 이러한 방법 사용.

    // 인스펙터에서 수정할 값.
    [SerializeField] private Image blackBack; // 화면 꽉 채운 이미지 컴포넌트. (검은색.)
    [SerializeField] private Image whiteBack;
    [SerializeField] private float time = 1.0f;

    // 실제 Static 메소드에서 사용할 값.
    static private Image BlackBack;
    static private Image WhiteBack;
    static private float Time;

    static Sequence sequenceFade_B;
    static Sequence sequenceFadeIn_B;
    static Sequence sequenceFadeOut_B;
    static Sequence sequenceFade_W;
    static Sequence sequenceFadeIn_W;
    static Sequence sequenceFadeOut_W;


    private void Awake()
    {
        WhiteBack = whiteBack;
        BlackBack = blackBack;
       
        
        Time = time;
        WhiteBack.enabled = true;
        BlackBack.enabled = true;
    }

    private void Start()
    {
        // 구성.
        sequenceFade_B = DOTween.Sequence()
            .SetAutoKill(false) // DoTween Sequence는 기본적으로 일회용임. 재사용하려면 써주자.
            .OnRewind(() => // 실행 전. OnStart는 unity Start 함수가 불릴 때 호출됨. 낚이지 말자.
            {
                BlackBack.enabled = true;
            })
            .Append(BlackBack.DOFade(1.0f, 0)) // 어두워짐. 알파 값 조정.
            .OnComplete(() => // 실행 후.
            {
                BlackBack.enabled = true;
            });

        sequenceFadeOut_B = DOTween.Sequence()
            .SetAutoKill(false) // DoTween Sequence는 기본적으로 일회용임. 재사용하려면 써주자.
            .OnRewind(() => // 실행 전. OnStart는 unity Start 함수가 불릴 때 호출됨. 낚이지 말자.
            {
                BlackBack.enabled = true;
            })
            .Append(BlackBack.DOFade(0.0f, 0f)) // 어두워짐. 알파 값 조정.
            .Append(BlackBack.DOFade(1.0f, Time)) // 어두워짐. 알파 값 조정.

            .OnComplete(() => // 실행 후.
            {
                BlackBack.enabled = true;
            });

        sequenceFadeIn_B = DOTween.Sequence()
            .SetAutoKill(false) // DoTween Sequence는 기본적으로 일회용임. 재사용하려면 써주자.
            .OnRewind(() => // 실행 전. OnStart는 unity Start 함수가 불릴 때 호출됨. 낚이지 말자.
            {
                BlackBack.enabled = true;
            })
            .Append(BlackBack.DOFade(1.0f, 0f)) // 어두워짐. 알파 값 조정.
            .Append(BlackBack.DOFade(0.0f, Time)) // 밝아짐. 알파 값 조정.
            .OnComplete(() => // 실행 후.
            {
                BlackBack.enabled = false;
            });
        sequenceFade_W = DOTween.Sequence()
            .SetAutoKill(false) // DoTween Sequence는 기본적으로 일회용임. 재사용하려면 써주자.
            .OnRewind(() => // 실행 전. OnStart는 unity Start 함수가 불릴 때 호출됨. 낚이지 말자.
            {
                WhiteBack.enabled = true;
            })
            .Append(WhiteBack.DOFade(1.0f, 0)) // 어두워짐. 알파 값 조정.
            .OnComplete(() => // 실행 후.
            {
                WhiteBack.enabled = true;
            });

        sequenceFadeOut_W = DOTween.Sequence()
            .SetAutoKill(false) // DoTween Sequence는 기본적으로 일회용임. 재사용하려면 써주자.
            .OnRewind(() => // 실행 전. OnStart는 unity Start 함수가 불릴 때 호출됨. 낚이지 말자.
            {
                WhiteBack.enabled = true;
            })
            .Append(WhiteBack.DOFade(0.0f, 0f)) // 어두워짐. 알파 값 조정.
            .Append(WhiteBack.DOFade(1.0f, Time)) // 어두워짐. 알파 값 조정.

            .OnComplete(() => // 실행 후.
            {
                WhiteBack.enabled = true;
            });

        sequenceFadeIn_W = DOTween.Sequence()
            .SetAutoKill(false) // DoTween Sequence는 기본적으로 일회용임. 재사용하려면 써주자.
            .OnRewind(() => // 실행 전. OnStart는 unity Start 함수가 불릴 때 호출됨. 낚이지 말자.
            {
                WhiteBack.enabled = true;
            })
            .Append(WhiteBack.DOFade(1.0f, 0f)) // 어두워짐. 알파 값 조정.
            .Append(WhiteBack.DOFade(0.0f, Time)) // 밝아짐. 알파 값 조정.
            .OnComplete(() => // 실행 후.
            {
                WhiteBack.enabled = false;
            });

    }

    static public void Play(GlobalValue.Transition transition)
    {
        switch (transition)
        {
            case GlobalValue.Transition.Fade_B: Fade_B();
                Debug.Log("Fade"); break;
            case GlobalValue.Transition.FadeIn_B: FadeIn_B();
                Debug.Log("FadeIn"); break;
            case GlobalValue.Transition.FadeOut_B: FadeOut_B();
                Debug.Log("FadeOut"); break;
            case GlobalValue.Transition.Fade_W: Fade_W();
                Debug.Log("Fade"); break;
            case GlobalValue.Transition.FadeIn_W: FadeIn_W();
                Debug.Log("FadeIn"); break;
            case GlobalValue.Transition.FadeOut_W: FadeOut_W();
                Debug.Log("FadeOut"); break;
        }
    }

    static private void Fade_B()
    {
        sequenceFade_B.Restart(); // Play()로 하면, 한번 밖에 실행 안 됨.
      
    }

    static private void FadeOut_B()
    {
        sequenceFadeOut_B.Restart(); // Play()로 하면, 한번 밖에 실행 안 됨.
       
    }
    static private void FadeIn_B()
    {
        sequenceFadeIn_B.Restart(); // Play()로 하면, 한번 밖에 실행 안 됨.

    }
    static private void Fade_W()
    {
        sequenceFade_W.Restart(); // Play()로 하면, 한번 밖에 실행 안 됨.

    }

    static private void FadeOut_W()
    {
        sequenceFadeOut_W.Restart(); // Play()로 하면, 한번 밖에 실행 안 됨.

    }
    static private void FadeIn_W()
    {
        sequenceFadeIn_W.Restart(); // Play()로 하면, 한번 밖에 실행 안 됨.

    }

}