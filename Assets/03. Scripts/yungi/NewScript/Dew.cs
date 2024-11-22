using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dew : MonoBehaviour
{
    public float dew;
    [SerializeField] private GameObject VFX_Bubble_Full;
    private void Update()
    {
        dew += Time.deltaTime;
        
        if (dew > 5)
        {
            VFX_Bubble_Full.SetActive(true);
        }
        else
        {
            VFX_Bubble_Full.SetActive(false);
        }
    }
}
