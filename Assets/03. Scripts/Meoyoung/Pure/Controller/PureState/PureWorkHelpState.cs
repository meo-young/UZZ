using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PureWorkHelpState : MonoBehaviour, IControllerState
{
    PureController pc;
    FieldWork fws;
    public void OnStateEnter(PureController controller)
    {
        if (pc == null)
            pc = controller;

        SoundManager.instance.PlaySFX(SFX.Ambience.TROUBLE);
        fws = pc.fieldWorkState;
        pc.pureAnimationSet.ControlFieldWorkHelpAnimation(fws.state);
    }

    public void OnStateUpdate()
    {

    }

    // 작업도움상태가 성공적으로 끝나면 호감도를 얻고, 작업도움성공말풍선이 활성화됨
    public void OnStateExit()
    {
        pc.pureAnimationSet.ControlFieldWorkHelpAnimation(fws.state);
        pc.pureStat.GetLikeability(pc.fieldWorkManager.likeability);
        pc.pureAnimationSet.ShowFieldWorkHelpSuccessText(fws.state);
    }
}
