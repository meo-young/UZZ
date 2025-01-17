using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constant;

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
        if (miniEventCounter > MINIEVENT_FINISH_TIME)
        {
            flowerManager.GetMiniEventReward();
            MainManager.instance.pure.SetActive(true);
        }
    }
}
