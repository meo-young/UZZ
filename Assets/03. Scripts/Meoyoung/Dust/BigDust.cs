using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigDust : MonoBehaviour
{
    [SerializeField] FlowerManager flowerManager;
    private int touchCounter;

    private void Awake()
    {
        touchCounter = 0;
    }
    private void OnMouseDown()
    {
        touchCounter++;
        if (touchCounter == 5)
            flowerManager.GetBigEventReward();
    }
}
