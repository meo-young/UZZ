using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    public void OnStateUpdate()
    {
        baseAnimationTimer += Time.deltaTime;
        if (baseAnimationTimer > pc.pureStat.baseAnimationPlayTime)
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
