using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;


[System.Serializable]
public class MyItem
{
    public Item item;
    public int count;

    public MyItem(Item item, int count)
    {
        this.item = item;
        this.count = count;
    }
}
public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    public List<MyItem> items;
    [SerializeField]private ItemManager itemManager;
    [SerializeField]
    private Transform slotParent;
    [SerializeField]
    private Slot[] slots;

#if UNITY_EDITOR
    private void OnValidate()
    {
        slots = slotParent.GetComponentsInChildren<Slot>();
    }
#endif

    public void JsonItemSave()
    {

        FileStream stream = new FileStream(Application.persistentDataPath + "/item.json", FileMode.Create);

        string jsonData = JsonConvert.SerializeObject(items, Formatting.Indented);
        Debug.Log(jsonData);

        byte[] data = Encoding.UTF8.GetBytes(jsonData);
        stream.Write(data, 0, data.Length);
        stream.Close();

    }
    public void JsonItemLoad()
    {
        string filePath = Application.persistentDataPath + "/item.json";
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            items = JsonConvert.DeserializeObject<List<MyItem>>(jsonData);

            

            Debug.Log("Loaded items: " + jsonData);

          

        }
      
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // �ڽ��� �ı�
            return;
        }

        // �ν��Ͻ��� ������ �Ҵ�
        Instance = this;

        // �� ��ȯ �ÿ��� �ν��Ͻ� ����
        DontDestroyOnLoad(gameObject);


        //AddItem(FindItem(1), 2);
        JsonItemLoad();
        
    }
    private void Start()
    {
        FreshSlot();
    }
    Item FindItem(int id)
    {
        for (int i = 0; i < itemManager.itemList.Length; i++)
        {
            if (itemManager.itemList[i].id == id)
            {
                return itemManager.itemList[i];
               
            }
        }
        return null;
    }

    public void FreshSlot()
    {
        int i = 0;
        for (; i < items.Count && i < slots.Length; i++)
        {
            slots[i].AddItem(items[i].item, items[i].count);
            
             
        }
        for (; i < slots.Length; i++)
        {
            slots[i].item = null;
        }

        JsonItemSave();
    }

    public void AddItem(Item _item,int count)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].item.id == _item.id) // �������� id�� ������ Ȯ��
            {
                items[i].count += count; // ������ �������� ������ count�� ����
                FreshSlot();
                return; // �۾��� �Ϸ�Ǿ����Ƿ� �޼��带 ����
            }
        }

        if (items.Count <  slots.Length)
        {
            
            items.Add(new MyItem(_item, count));
            FreshSlot();
        }
        else
        {
            print("������ ���� �� �ֽ��ϴ�.");
        }
    }

    public void TestBtn()
    {
        int a = Random.RandomRange(0, 28);
        
        AddItem(ItemManager.instance.itemList[a], 1);
    }

}
