using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constant;

public class PureInteractionState : MonoBehaviour, IControllerState
{
    PureController pc;
    float baseAnimationTimer;
    int randNum;

    int level => pc.pureStat.pureInfo.level;
    public void OnStateEnter(PureController controller)
    {
        if (pc == null)
            pc = controller;

        randNum = Random.Range(0, pc.pureStat.likeabilityInfo[level].interactionAnimationIndex + 1);
        pc.pureAnimationSet.ShowRandomInteractionAnimation(randNum);
        baseAnimationTimer = 0;
        if (!MainManager.instance.gameInfo.likeabilityFlag)
        {
            pc.pureStat.GetLikeability(INTERACTION_LIKEABILITY);
            MainManager.instance.gameInfo.likeabilityFlag = true;
            Instantiate(pc.vfxManager.likeabilityVFX, this.transform.position, Quaternion.identity);
        }

        pc.pureStat.SetTrueInteractionText();
        pc.interactionText.ShowRandomInteractionText();
    }

    public void OnStateUpdate()
    {
        baseAnimationTimer += Time.deltaTime;
        if (baseAnimationTimer > pc.pureStat.baseAnimationPlayTime)
            pc.ChangeState(pc.preparationState);
    }

    public void OnStateExit()
    {
        pc.pureAnimationSet.ShowRandomInteractionAnimation(randNum);
    }
}
