using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;
    [SerializeField] Image image;
    void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Debug.LogWarning("���� �ΰ� �̻��� ���� �Ŵ����� �����մϴ�!");
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        
    }


    public Item[] itemList;
}
