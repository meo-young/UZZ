using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Slot : MonoBehaviour
{
    public Item item; // 획득한 아이템
    public int itemCount; // 획득한 아이템의 개수
    public Image itemImage;  // 아이템의 이미지

    [Header("색 초기화 할 것들")]
    [SerializeField] Image itemImage_bg;
    [SerializeField] Image quantity_bg;
 

    [SerializeField]
    private TMP_Text text_Count;
    [SerializeField]
    private TMP_Text text_Name;
    [SerializeField]
    private Inventory inventory;
    
    public void SetColor(float _alpha)
    {
        Color color = new Color(1f, 1f, 1f, _alpha);
        
       
        text_Count.color = color;
        text_Name.color = color;
       
        itemImage.color = color;
        quantity_bg.color = color;
    }
    public void SetText()
    {
        text_Count.text = itemCount.ToString();
    }
    
    public void AddItem(Item _item, int _count = 1)
    {
        Debug.Log(_item);
        item = _item;
        itemCount = _count;
        item.LoadSprite();
        itemImage.sprite = _item.itemImage;
        text_Name.text = _item.itemName;

        if (ItemManager.instance == null)
        {
            Debug.LogError("ItemManager instance is null.");
            return;
        }
        
        //go_CountImage.SetActive(true);
        SetText();
        // if(item.itemType != Item.ItemType.Equipment)
        // {
        //     go_CountImage.SetActive(true);
        //     text_Count.text = itemCount.ToString();
        // }
        // else
        // {
        //     text_Count.text = "0";
        //     go_CountImage.SetActive(false);
        // }

        SetColor(1);
        gameObject.SetActive(true);

        
    }
    
    
    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        SetText();

        for (int i = 0; i < Inventory.Instance.items.Count; i++)
        {
            if(Inventory.Instance.items[i].item.id == item.id)
            {
                Inventory.Instance.items[i].count += _count;
                if(Inventory.Instance.items[i].count<=0)
                {
                    Inventory.Instance.items.RemoveAt(i);
                }
            }
            
        }
        Inventory.Instance.JsonItemSave();
        if (itemCount <= 0)
            ClearSlot();
    }
    
    private void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);

        text_Name.text = "";
        text_Count.text = "";
        
        //go_CountImage.SetActive(false);
    }

    //private void Sorting(Transform ds)
    //{
    //    string abc = $"{ds.parent}";
    //    string temp = (Regex.Replace(abc, @"\D", ""));
    //    int itemp = int.Parse(temp);
    //    if(itemCoun)
    //}

    
    
    
}