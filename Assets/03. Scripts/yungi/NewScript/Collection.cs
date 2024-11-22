using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collection : MonoBehaviour
{
    public GameObject[] fishes;
    public GameObject[] meteors;

    private void Start()
    {
       
    }
    public void GetFish(int index)
    {
        fishes[index].GetComponent<Image>().color = Color.white;
    }

    public void GetMeteor(int index)
    {
        meteors[index].GetComponent<Image>().color = Color.white;
    }
}
