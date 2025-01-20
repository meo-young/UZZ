using System.Collections;
using System.Collections.Generic;
using TMPro;
using static Constant;
using UnityEngine;

public class PureWalk : MonoBehaviour
{
    [SerializeField] PureController pc;
    [SerializeField] PureMove pm;

    private float walkTimer;
    private bool firstWalkFlag = false;

    private void OnEnable()
    {
        pc.preparationState = pc._idleState;
        walkTimer = 0;
        pm.isWalkState = true;
        pm.SetRandomPosition();
    }

    private void Update()
    {
        if(!firstWalkFlag)
        {
            pc.ChangeState(pc._baseState);
            firstWalkFlag = true;
        }

        walkTimer += Time.deltaTime;
        if (walkTimer > PURE_WALK_RANDOM_TIME)
        {
            pc.ChangeState(pc._baseState);
        }
    }

    private void OnDisable()
    {
        pm.isWalkState = false;
    }
}
