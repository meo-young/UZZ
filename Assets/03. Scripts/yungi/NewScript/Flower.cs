using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Flower : MonoBehaviour
{
    public int flowerLv=1;
    public float flower;
    public float maxFlower;

    [SerializeField] private GameObject VFX_Flower_Full;

    [SerializeField] UZZ_DataTable UZZ_DataTable;
    [System.Serializable]
    public struct Flower_Data
    {
        public int flower_Level;
        public int flower_image;
        public int flower_exp;
        public int flower_dew;
    }
    public Flower_Data[] flower_Data;

    private void Awake()
    {
        for (int i = 0; i < UZZ_DataTable.FlowerTable.Count; ++i)
        {
            flower_Data[i].flower_Level = UZZ_DataTable.FlowerTable[i].flower_Level;
            flower_Data[i].flower_image = UZZ_DataTable.FlowerTable[i].flower_image;
            flower_Data[i].flower_exp = UZZ_DataTable.FlowerTable[i].flower_exp;
            flower_Data[i].flower_dew = UZZ_DataTable.FlowerTable[i].flower_dew;
        }

    }
    // Update is called once per frame
    void Update()
    {
        flower += Time.deltaTime;
        if(flower>5)
        {
            VFX_Flower_Full.SetActive(true);
        }
        else
        {
            VFX_Flower_Full.SetActive(false);
        }
    }
}
