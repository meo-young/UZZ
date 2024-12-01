using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurePresentGiveState : MonoBehaviour, IControllerState
{
    PureController pc;

    public void OnStateEnter(PureController controller)
    {
        if (pc == null)
            pc = controller;

        // 이미 수집하지 않은 선물중 랜덤한 선물 획득하는 로직 구현
        SoundManager.instance.PlaySFX(SFX.PureSound.GIVEGIFT);
        pc.pureAnimationSet.purePresentGive.SetActive(true);
    }

    public void OnStateUpdate()
    {

    }

    public void OnStateExit()
    {
        pc.pureAnimationSet.purePresentGive.SetActive(false);
    }
}
