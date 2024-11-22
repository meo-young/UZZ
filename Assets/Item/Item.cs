using Newtonsoft.Json;
using UnityEngine;


[CreateAssetMenu]
public class Item : ScriptableObject
{
    public int id;
    public string itemName;
    public int itemPrice_Petal;
    public int itemPrice_Dew;


    [JsonIgnore]
    public Sprite itemImage;

    public void LoadSprite()
    {
       

        for (int i = 0; i < ItemManager.instance.itemList.Length; i++)
        {
            if (this.id == ItemManager.instance.itemList[i].id)
            {
                this.itemImage= ItemManager.instance.itemList[i].itemImage;
                break;
            }

        }
    }
}
