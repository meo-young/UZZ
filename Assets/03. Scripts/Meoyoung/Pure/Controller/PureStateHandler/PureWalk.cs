using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PureWalk : MonoBehaviour
{
    [SerializeField] PureController pc;
    [SerializeField] PureMove pm;

    private float walkTimer;

    private void OnEnable()
    {
        pc.preparationState = pc._idleState;
        walkTimer = 0;
        pm.isWalkState = true;
        pm.SetRandomPosition();
    }

    private void Update()
    {
        walkTimer += Time.deltaTime;
        if (walkTimer > pc.pureStat.walkRandomTime)
        {
            pc.ChangeState(pc._baseState);
        }
    }

    private void OnDisable()
    {
        pm.isWalkState = false;
    }
}
