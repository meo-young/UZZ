using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerController : MonoBehaviour
{
    private FlowerManager flowerManager;

    private void Start()
    {
        flowerManager = MainManager.instance.flowerManager;
    }

    private void Update()
    {
        
    }
}
