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
    // �̱��� ��� static ���� ����, �̷��� ��� ���.

    // �ν����Ϳ��� ������ ��.
    [SerializeField] private Image blackBack; // ȭ�� �� ä�� �̹��� ������Ʈ. (������.)
    [SerializeField] private Image whiteBack;
    [SerializeField] private float time = 1.0f;

    // ���� Static �޼ҵ忡�� ����� ��.
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
        // ����.
        sequenceFade_B = DOTween.Sequence()
            .SetAutoKill(false) // DoTween Sequence�� �⺻������ ��ȸ����. �����Ϸ��� ������.
            .OnRewind(() => // ���� ��. OnStart�� unity Start �Լ��� �Ҹ� �� ȣ���. ������ ����.
            {
                BlackBack.enabled = true;
            })
            .Append(BlackBack.DOFade(1.0f, 0)) // ��ο���. ���� �� ����.
            .OnComplete(() => // ���� ��.
            {
                BlackBack.enabled = true;
            });

        sequenceFadeOut_B = DOTween.Sequence()
            .SetAutoKill(false) // DoTween Sequence�� �⺻������ ��ȸ����. �����Ϸ��� ������.
            .OnRewind(() => // ���� ��. OnStart�� unity Start �Լ��� �Ҹ� �� ȣ���. ������ ����.
            {
                BlackBack.enabled = true;
            })
            .Append(BlackBack.DOFade(0.0f, 0f)) // ��ο���. ���� �� ����.
            .Append(BlackBack.DOFade(1.0f, Time)) // ��ο���. ���� �� ����.

            .OnComplete(() => // ���� ��.
            {
                BlackBack.enabled = true;
            });

        sequenceFadeIn_B = DOTween.Sequence()
            .SetAutoKill(false) // DoTween Sequence�� �⺻������ ��ȸ����. �����Ϸ��� ������.
            .OnRewind(() => // ���� ��. OnStart�� unity Start �Լ��� �Ҹ� �� ȣ���. ������ ����.
            {
                BlackBack.enabled = true;
            })
            .Append(BlackBack.DOFade(1.0f, 0f)) // ��ο���. ���� �� ����.
            .Append(BlackBack.DOFade(0.0f, Time)) // �����. ���� �� ����.
            .OnComplete(() => // ���� ��.
            {
                BlackBack.enabled = false;
            });
        sequenceFade_W = DOTween.Sequence()
            .SetAutoKill(false) // DoTween Sequence�� �⺻������ ��ȸ����. �����Ϸ��� ������.
            .OnRewind(() => // ���� ��. OnStart�� unity Start �Լ��� �Ҹ� �� ȣ���. ������ ����.
            {
                WhiteBack.enabled = true;
            })
            .Append(WhiteBack.DOFade(1.0f, 0)) // ��ο���. ���� �� ����.
            .OnComplete(() => // ���� ��.
            {
                WhiteBack.enabled = true;
            });

        sequenceFadeOut_W = DOTween.Sequence()
            .SetAutoKill(false) // DoTween Sequence�� �⺻������ ��ȸ����. �����Ϸ��� ������.
            .OnRewind(() => // ���� ��. OnStart�� unity Start �Լ��� �Ҹ� �� ȣ���. ������ ����.
            {
                WhiteBack.enabled = true;
            })
            .Append(WhiteBack.DOFade(0.0f, 0f)) // ��ο���. ���� �� ����.
            .Append(WhiteBack.DOFade(1.0f, Time)) // ��ο���. ���� �� ����.

            .OnComplete(() => // ���� ��.
            {
                WhiteBack.enabled = true;
            });

        sequenceFadeIn_W = DOTween.Sequence()
            .SetAutoKill(false) // DoTween Sequence�� �⺻������ ��ȸ����. �����Ϸ��� ������.
            .OnRewind(() => // ���� ��. OnStart�� unity Start �Լ��� �Ҹ� �� ȣ���. ������ ����.
            {
                WhiteBack.enabled = true;
            })
            .Append(WhiteBack.DOFade(1.0f, 0f)) // ��ο���. ���� �� ����.
            .Append(WhiteBack.DOFade(0.0f, Time)) // �����. ���� �� ����.
            .OnComplete(() => // ���� ��.
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
        sequenceFade_B.Restart(); // Play()�� �ϸ�, �ѹ� �ۿ� ���� �� ��.
      
    }

    static private void FadeOut_B()
    {
        sequenceFadeOut_B.Restart(); // Play()�� �ϸ�, �ѹ� �ۿ� ���� �� ��.
       
    }
    static private void FadeIn_B()
    {
        sequenceFadeIn_B.Restart(); // Play()�� �ϸ�, �ѹ� �ۿ� ���� �� ��.

    }
    static private void Fade_W()
    {
        sequenceFade_W.Restart(); // Play()�� �ϸ�, �ѹ� �ۿ� ���� �� ��.

    }

    static private void FadeOut_W()
    {
        sequenceFadeOut_W.Restart(); // Play()�� �ϸ�, �ѹ� �ۿ� ���� �� ��.

    }
    static private void FadeIn_W()
    {
        sequenceFadeIn_W.Restart(); // Play()�� �ϸ�, �ѹ� �ۿ� ���� �� ��.

    }

}