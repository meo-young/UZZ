using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTime : MonoBehaviour
{
    [SerializeField]
    private float timer;

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0)
            gameObject.SetActive(false);
        timer -= Time.deltaTime;
    }
}
