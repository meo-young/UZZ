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
            Debug.LogWarning("씬에 두개 이상의 게임 매니저가 존재합니다!");
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        
    }


    public Item[] itemList;
}
