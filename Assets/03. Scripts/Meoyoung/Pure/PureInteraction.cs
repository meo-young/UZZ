using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PureInteraction : MonoBehaviour
{
    [SerializeField] FlowerManager flowerManager;
    [SerializeField] PureController pc;

    void OnMouseDown()
    {
        if (MainManager.instance.gameInfo.showerFlag)
            return;

        if (flowerManager.isFlowerEvent)
            return;

        pc.preparationState = pc.CurrentState;
        pc.ChangeState(pc._interactionState);
    }
}
