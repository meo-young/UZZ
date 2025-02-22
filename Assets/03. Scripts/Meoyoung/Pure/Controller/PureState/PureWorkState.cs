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

        if (pc.workTimer < FIELDWORK_HELP_CHECK_TIME) // 작업도움확률체크 시간이 된 경우 확률체크
            return;

        // 작업도움이 걸린 경우 정령의 힘 이벤트 확률 체크
        // 만약 정령의 힘 이벤트가 걸리지 않았다면 작업도움 실행
        if (pc.fieldWorkManager.CheckHelpProbability())
        {
            if(pc.fieldWorkManager.CheckSpiritEventProbability())
            {
                // 정령의 힘 이벤트 실행
                Debug.Log("정령의 힘 이벤트 실행");
                pc.spiritEventCheckFlag = true;
            }

            pc.ChangeState(pc._workHelpState);
            
        }
           

        pc.helpEventCheckFlag = true;
    }

    public void OnStateExit()
    {
        pc.pureAnimationSet.ControlFieldWorkAnimation(fws.type);
    }
}
