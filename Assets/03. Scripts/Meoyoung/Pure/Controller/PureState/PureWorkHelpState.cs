using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constant;

public class PureWorkHelpState : MonoBehaviour, IControllerState
{
    PureController pc;
    FieldWork fws;
    GameObject spiritEventVFX;

    public void OnStateEnter(PureController controller)
    {
        if (pc == null)
            pc = controller;

        SoundManager.instance.PlaySFX(SFX.Ambience.TROUBLE);
        fws = pc.fieldWorkState;
        pc.pureAnimationSet.ControlFieldWorkHelpAnimation(fws.type);

        if(pc.spiritEventCheckFlag)
        {
            spiritEventVFX = Instantiate(VFXManager.instance.spiritEventVFX, pc.ReturnWorkPurePosition().position, Quaternion.identity);
        }
    }

    public void OnStateUpdate()
    {

    }

    // 작업도움상태가 성공적으로 끝나면 호감도를 얻고, 작업도움성공말풍선이 활성화됨
    public void OnStateExit()
    {
        if(pc.spiritEventCheckFlag)
        {
            Destroy(spiritEventVFX);
            Instantiate(VFXManager.instance.spiritVFX, pc.ReturnWorkPurePosition().position, Quaternion.identity);
        }

        pc.pureAnimationSet.ControlFieldWorkHelpAnimation(fws.type);
        pc.pureStat.GetLikeability(FIELDWORK_LIKEABILITY);
        pc.pureAnimationSet.ShowFieldWorkHelpSuccessText(fws.type);
    }
}
