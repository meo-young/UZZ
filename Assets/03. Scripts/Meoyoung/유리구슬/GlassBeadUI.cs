using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class GlassBeadUI : MonoBehaviour
{
    [Header("# 유리구슬 데이터테이블")]
    [SerializeField] TextAsset glassBeadDataTable;
    public List<GlassBeadData> glassBeadData;

    [Header("# 유리구슬 데이터 프리팹")]
    [SerializeField] GameObject glassBeadItemPrefab;
    [SerializeField] Transform glassBeadItemParent;

    void Start()
    {
        glassBeadData = LoadTextAssetData.instance.LoadData<GlassBeadData>(glassBeadDataTable);

        ShowGlassBeadItem();
    }


    void ShowGlassBeadItem()
    {
        foreach (var item in glassBeadData)
        {
            GameObject obj = Instantiate(glassBeadItemPrefab, glassBeadItemParent);
            
            Image[] images = obj.GetComponentsInChildren<Image>();
            AddressableManager.instance.LoadSprite(item.image, images[1]);

            TMP_Text[] texts = obj.GetComponentsInChildren<TMP_Text>();
            texts[0].text = item.name;
            texts[1].text = item.script;
            texts[2].text = item.price;
        }
    }

    public class GlassBeadData
    {
        public int      index;
        public string   name;
        public int      ea;
        public string   script;
        public string   image;
        public string      price;
        public string   id;
    }
}
