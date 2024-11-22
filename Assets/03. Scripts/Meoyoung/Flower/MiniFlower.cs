using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniFlower : MonoBehaviour
{
    [SerializeField] FlowerManager flowerManager;

    private float miniEventCounter;

    private void OnEnable()
    {
        miniEventCounter = 0f;
    }

    private void Update()
    {
        miniEventCounter += Time.deltaTime;
        if (miniEventCounter > flowerManager.miniEventFinishTime)
            flowerManager.GetMiniEventReward();
    }
}
