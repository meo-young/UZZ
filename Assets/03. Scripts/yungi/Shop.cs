using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Shop : MonoBehaviour
{


    public Text text;

    [SerializeField]
    int count = 1;

    
        
    public Item selectItem;
   

   

    private void LateUpdate()
    {
        text.text = count.ToString();
    }
    public void PlusBtn()
    {
        count++;
    }
    public void MinusBtn()
    {
        count--;
    }

    public void SelectItem(Item Sitem)
    {
        selectItem = Sitem;
        count = 1;
    }
}
