using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureManager : MonoBehaviour
{
    public Transform[] furnitures;
    public Transform[] thema;
    public Transform furniture;

    private void Awake()
    {
        for (int i = 0; i < furniture.childCount; i++) 
        {
            thema[i] = furniture.GetChild(i);
        }

        ChangeThema(0);
    }

    public void ChangeThema(int index)
    {
       
        for (int i = 0; i < thema[index].childCount; i++)
        {
            furnitures[i] = thema[index].GetChild(i);
        }

    }

    //������β���
    public void OffFurnitures()
    {
        for (int i = 0; i < furnitures.Length; i++)
        {
            furnitures[i].gameObject.SetActive(false);
        }
    }
    //����Ű��
    public void OnFurniture(int index)
    {
        furnitures[index].gameObject.SetActive(true);
    }
    public void OffFurnitrue(int index)
    {
        furnitures[index].gameObject.SetActive(false);
    }

    public void FBtn(int index)
    {
        //����������
        if (furnitures[index].gameObject.activeSelf)
        {
            OffFurnitrue(index);
        }
        else
        {
            OnFurniture(index);
        }
    }


}
