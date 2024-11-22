using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator : MonoBehaviour
{
    //스파인 애니메이션을 위한 것
    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset[] AnimClip;
    
    //애니메이션 대한 Enum
    public enum AnimState
    {
        Idle, Work
    }

    //현재 애니메이션 처리가 무엇인지에 대한 변수
    public AnimState _AnimState;

    //현재 어떤 애니메이션이 재생되고 있는지에 대한 변수
    public string CurrentAnimation;
    //재생속도
    public float timeSclae = 1f;
    private void Awake()
    {

    }

    private void Update()
    {
        //애니메이션 적용
        SetCurrentAnimation(_AnimState);
    }
    private void _AsncAnimation(AnimationReferenceAsset animClip, bool loop, float timeScale)
    {
        skeletonAnimation.timeScale = timeScale;
        skeletonAnimation.loop = loop;
        //동일한 애니메이션을 재생하려고 한다면 아래 코드 구문 실행x
        if (animClip.name.Equals(CurrentAnimation))
            return;
            
        //해당 애니메이션으로 변경한다.
        skeletonAnimation.state.SetAnimation(0, animClip, loop).TimeScale = timeScale;
       
       

        //현재 재생되고 있는 애니메이션 값을 변경
        CurrentAnimation = animClip.name;

    }

    private void SetCurrentAnimation(AnimState _state)
    {
         switch(_state)
        {
            case AnimState.Idle:
                _AsncAnimation(AnimClip[(int)AnimState.Idle], true, timeSclae);
                break;
            case AnimState.Work:
                _AsncAnimation(AnimClip[(int)AnimState.Work], true, timeSclae);
                break;
        }

        //짧게 작성한다면 요렇게..
        //_AsncAnimation(AnimClip[(int)AnimState.Work], true, 1f) ;
    }
}
