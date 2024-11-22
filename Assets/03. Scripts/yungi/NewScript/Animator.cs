using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator : MonoBehaviour
{
    //������ �ִϸ��̼��� ���� ��
    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset[] AnimClip;
    
    //�ִϸ��̼� ���� Enum
    public enum AnimState
    {
        Idle, Work
    }

    //���� �ִϸ��̼� ó���� ���������� ���� ����
    public AnimState _AnimState;

    //���� � �ִϸ��̼��� ����ǰ� �ִ����� ���� ����
    public string CurrentAnimation;
    //����ӵ�
    public float timeSclae = 1f;
    private void Awake()
    {

    }

    private void Update()
    {
        //�ִϸ��̼� ����
        SetCurrentAnimation(_AnimState);
    }
    private void _AsncAnimation(AnimationReferenceAsset animClip, bool loop, float timeScale)
    {
        skeletonAnimation.timeScale = timeScale;
        skeletonAnimation.loop = loop;
        //������ �ִϸ��̼��� ����Ϸ��� �Ѵٸ� �Ʒ� �ڵ� ���� ����x
        if (animClip.name.Equals(CurrentAnimation))
            return;
            
        //�ش� �ִϸ��̼����� �����Ѵ�.
        skeletonAnimation.state.SetAnimation(0, animClip, loop).TimeScale = timeScale;
       
       

        //���� ����ǰ� �ִ� �ִϸ��̼� ���� ����
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

        //ª�� �ۼ��Ѵٸ� �䷸��..
        //_AsncAnimation(AnimClip[(int)AnimState.Work], true, 1f) ;
    }
}
