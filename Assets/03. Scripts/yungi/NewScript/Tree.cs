using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{

    [SerializeField] public PrayManager prayManager;
    private void OnMouseDown()
    {
        prayManager.OnUI();
    }
}
