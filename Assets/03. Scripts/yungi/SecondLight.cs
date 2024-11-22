using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SecondLight : MonoBehaviour
{
    [GradientUsage(true)]
    [SerializeField] Gradient gradient = null;
    [SerializeField] Light2D[] light2Ds = null;

    public float totalTime = 10f;
    public float currentTime = 0f;
    void Start()
    {
        light2Ds = GetComponentsInChildren<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > totalTime)
            currentTime =  0f;

        float normalizedTime = currentTime / totalTime;

        foreach (var light2D in light2Ds)
        {

            light2D.color = gradient.Evaluate(normalizedTime);

        }
    }
}
