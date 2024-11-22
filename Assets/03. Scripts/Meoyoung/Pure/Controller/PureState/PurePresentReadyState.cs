using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PurePresentReadyState : MonoBehaviour, IControllerState
{
    PureController pc;
    private bool readyFlag = false;

    public void OnStateEnter(PureController controller)
    {
        if(pc == null)
            pc = controller;

        readyFlag = true;
        pc.pureAnimationSet.purePresentReady.SetActive(true);
    }

    public void OnStateUpdate()
    {

    }

    public void OnStateExit()
    {
        readyFlag = false;
        pc.pureAnimationSet.purePresentReady.SetActive(false);
    }

    /*void OnMouseDown()
    {
        if (!readyFlag)
        {
            Debug.Log("ready 상태 아님");
            return;
        }

        pc.ChangeState(pc._presentGiveState);
    }*/
}
