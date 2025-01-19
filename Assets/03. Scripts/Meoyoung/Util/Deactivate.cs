using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactivate : MonoBehaviour
{
    [SerializeField] float setActiveFalseTime;
    private float timeCounter;

    private void Start()
    {
        timeCounter = 0;
    }

    private void Update()
    {
        timeCounter += Time.deltaTime;
        if (timeCounter > setActiveFalseTime)
        {
            timeCounter = 0;
            this.gameObject.SetActive(false);
        }
    }
}
