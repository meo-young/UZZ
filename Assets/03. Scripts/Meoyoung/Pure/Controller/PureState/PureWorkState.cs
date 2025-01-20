using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constant;

public class PureWorkState : MonoBehaviour, IControllerState
{
    PureController pc;
    FieldWork fws;

    public void OnStateEnter(PureController controller)
    {
        if (pc == null)
            pc = controller;

        fws = pc.fieldWorkState;
        pc.pureAnimationSet.ControlFieldWorkAnimation(fws.type);
    }

    public void OnStateUpdate()
    {
        pc.workTimer += Time.deltaTime;

        if (pc.workTimer > FIELDWORK_REQUIRED_TIME) // 푸르의 작업시간이 지나면 Idle 상태로 전이
            pc.ChangeState(pc._idleState);

        if (!pc.pureStat.pureInfo.autoText && !pc.pureStat.pureInfo.interactionText)
        {
            pc.autoText.ShowFieldWorkText(pc.fieldWorkTextPos[(int)fws.type], (int)fws.type);
            pc.pureStat.SetTrueAutoText();
            return;
        }

        if (pc.helpEventCheckFlag)
            return;

        if (pc.workTimer < FIELDWORK_HELP_PROBABILITY) // 작업도움확률체크 시간이 된 경우 확률체크
            return;

        if (pc.fieldWorkManager.CheckHelpProbability())
            pc.ChangeState(pc._workHelpState);

        pc.helpEventCheckFlag = true;
    }

    public void OnStateExit()
    {
        pc.pureAnimationSet.ControlFieldWorkAnimation(fws.type);
    }
}
