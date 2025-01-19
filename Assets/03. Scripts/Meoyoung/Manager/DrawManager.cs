using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static Constant;

public class DrawInfo
{
    public Dictionary<Furniture, int> myFurnitures;
}

public class DrawManager : MonoBehaviour
{
    public static DrawManager instance;

    [Header("# Draw Info")]
    [SerializeField] TextAsset drawDataTable;
    public DrawData[] drawdatas = new DrawData[DRAW_THEME_COUNT];

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
#endregion