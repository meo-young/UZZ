using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SaveAndLoad : MonoBehaviour
{
    //[SerializeField]
    //Inventory inv;
    //public Item[] items;

    //private void Start()
    //{
    //    inv = GetComponent<Inventory>();
    //}

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.S))
    //        Save();
    //    else if (Input.GetKeyDown(KeyCode.L))
    //        Load();
    //}

    //void Save()
    //{
    //    List<ItemLoad> itemsToLoads = new List<ItemLoad>();
    //    for (int i = 0; i < inv.slots.Length; i++)
    //    {
    //        Slot z = inv.slots[i];
    //        if(z.item)
    //        {
    //            ItemLoad h = new ItemLoad(z.item.ID, z.itemCount, i);
    //            itemsToLoads.Add(h);
    //        }
    //    }
    //    string json = CustomJson.ToJson(itemsToLoads);
    //    File.WriteAllText(Application.persistentDataPath+"/save", json);
    //    Debug.Log("Saving...");

      
    //}

    //void Load()
    //{
    //    Debug.Log("Loading");
    //    List<ItemLoad> itemsToLoad = CustomJson.FromJson<ItemLoad>(File.ReadAllText(Application.persistentDataPath+ "/save"));

       
    //    for (int i = 0; i<inv.slots.Length;i++)
    //    {
    //        foreach(ItemLoad z in itemsToLoad)
    //        {
    //            if(i == z.slotIndex)
    //            {
    //                inv.slots[i].item=inv.items[z.id];
    //                inv.slots[i].itemCount = z.amount;
    //                inv.slots[i].GetComponent<Image>().sprite = inv.items[z.id].itemImage;
    //                inv.slots[i].SetColor(1);         
    //                inv.slots[i].SetText();

    //                break;
    //            }
    //        }
    //    }
    //}
}

[System.Serializable]
public class ItemLoad
{
    public int id, amount, slotIndex;
    public ItemLoad(int ID,int AMOUNT,int SLOTINDEX)
    {
        id = ID;
        amount = AMOUNT;
        slotIndex = SLOTINDEX;
    }
}