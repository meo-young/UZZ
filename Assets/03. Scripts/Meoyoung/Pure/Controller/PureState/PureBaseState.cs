using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constant;

public class PureBaseState : MonoBehaviour, IControllerState
{
    PureController pc;
    float baseAnimationTimer;
    int randNum;

    int level => pc.pureStat.pureInfo.level;
    public void OnStateEnter(PureController controller)
    {
        if (pc == null)
            pc = controller;

        randNum = Random.Range(0, pc.pureStat.likeabilityInfo[level].defaultAnimationIndex + 1);
        pc.pureAnimationSet.ShowRandomBaseAnimation(randNum);
        baseAnimationTimer = 0;
        Debug.Log("베이스 애니메이션 시작");
    }

    public void OnStateUpdate()
    {
        baseAnimationTimer += Time.deltaTime;
        if (baseAnimationTimer > PURE_ANIMATION_TIME)
            pc.ChangeState(pc._idleState);

        if(!pc.pureStat.pureInfo.autoText && !pc.pureStat.pureInfo.interactionText)
        {
            pc.autoText.ShowInteractionText(pc.basePos, randNum);
            pc.pureStat.SetTrueAutoText();
        }
    }

    public void OnStateExit()
    {
        pc.pureAnimationSet.ShowRandomBaseAnimation(randNum);
    }
}
