using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PureHelpWork : MonoBehaviour
{
    [SerializeField] PureController pc;
    void OnMouseDown()
    {
        pc.ChangeState(pc._workState);
    }
}
