using System.Collections.Generic;
using System.IO;
using UnityEngine;
using AYellowpaper.SerializedCollections;
using static Constant;


public class DrawManager : MonoBehaviour
{
    public static DrawManager instance;

    [Header("# 데이터테이블")]
    [SerializeField] TextAsset drawDataTable;


    public DrawData[] drawdatas = new DrawData[DRAW_THEME_COUNT];
    public DrawInfo drawInfo = new();

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }

    private void Start()
    {
        drawdatas[0] = new DrawData();
        drawdatas[0].title = DRAW_THEME1_NAME;
        drawdatas[0].furnitures = LoadTextAssetData.instance.LoadData<Furniture>(drawDataTable);
    }

    public void AddFurniture(string _furniture)
    {
        if(drawInfo.myFurnitures.ContainsKey(_furniture))
            drawInfo.myFurnitures[_furniture]++;
        else
            drawInfo.myFurnitures[_furniture] = 1;
    }
}

#region 뽑기 관련 클래스
public class DrawData
{
    public string title;                    // 테마 이름
    public List<Furniture> furnitures;      // 테마에 속한 가구 정보

    public DrawData()
    {
        furnitures = new List<Furniture>();
    }
}

public class Furniture
{
    public int index;                   // 가구 번호
    public string name;                 // 가구 명
    public string flavorText;           // 가구의 FlavorText
    public int rank;                    // 가구 등급
    public int layer;                   // 가구 레이어   
    public string icon;                 // 가구 아이콘
    public string image;                // 가구 이미지
    public float probability;           // 가구가 뜰 확률
    public int power;                   // 가구 팔았을 때 받는 가루
    public int requiredPower;           // 가구 조합시 필요한 가루
}

[System.Serializable]
public class DrawInfo
{
    public SerializedDictionary<string, int> myFurnitures;

    public DrawInfo()
    {
        myFurnitures = new SerializedDictionary<string, int>();
    }
}
#endregion