using TMPro;
using UnityEngine;

public class PureWalkState : MonoBehaviour, IControllerState
{
    PureController pc;


    public void OnStateEnter(PureController controller)
    {
        if (pc == null)
            pc = controller;

        pc.pureAnimationSet.pureWalk.SetActive(true);
    }

    public void OnStateUpdate()
    {
        if (!pc.pureStat.pureInfo.autoText && !pc.pureStat.pureInfo.interactionText)
        {
            pc.autoText.ShowIdleRandomText(pc.basePos, 0);
            pc.pureStat.SetTrueAutoText();
        }
    }

    public void OnStateExit()
    {
        pc.pureAnimationSet.pureWalk.SetActive(false);
    }
}
