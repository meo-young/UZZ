using UnityEngine;

public class PureShowerState : MonoBehaviour, IControllerState
{
    PureController pc;

    public void OnStateEnter(PureController controller)
    {
        if (pc == null)
            pc = controller;

        pc.pureAnimationSet.pureShower.SetActive(true);
    }

    public void OnStateUpdate()
    {
        if(!MainManager.instance.gameInfo.showerFlag)
        {
            pc.ChangeState(pc._idleState);
        }
    }

    public void OnStateExit()
    {
        Debug.Log("샤워 종료");
        pc.pureAnimationSet.pureShower.SetActive(false);
    }
}
