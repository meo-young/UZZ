using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using AYellowpaper.SerializedCollections;
using static Constant;

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
public class FurnitureInstance
{
    public string instanceId;  // 가구 인스턴스의 고유 ID
    public bool isPlaced;     // 배치 여부

    public FurnitureInstance(string id)
    {
        instanceId = id;
        isPlaced = false;
    }
}

[System.Serializable]
public class DrawInfo
{
    public SerializedDictionary<string, List<FurnitureInstance>> furnitureInstances;

    public DrawInfo()
    {
        furnitureInstances = new SerializedDictionary<string, List<FurnitureInstance>>();
    }
}
#endregion

public class DrawManager : MonoBehaviour
{
    public static DrawManager instance;

    [Header("# 데이터테이블")]
    [SerializeField] TextAsset drawDataTable;

    public DrawData[] drawdatas = new DrawData[DRAW_THEME_COUNT];
    public DrawInfo[] drawInfo = new DrawInfo[DRAW_THEME_COUNT];

    public bool isDataLoaded { get; private set; } = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        drawdatas[0] = new DrawData();
        drawdatas[0].title = DRAW_THEME1_NAME;
        drawdatas[0].furnitures = LoadTextAssetData.instance.LoadData<Furniture>(drawDataTable);
        isDataLoaded = true;
    }

    public void AddFurniture(int _theme, string _furniture)
    {
        if (drawInfo[_theme].furnitureInstances == null)
            drawInfo[_theme].furnitureInstances = new SerializedDictionary<string, List<FurnitureInstance>>();

        // 각 가구 인스턴스마다 고유한 키를 생성
        string instanceId = $"{_theme}_{_furniture}_{System.Guid.NewGuid().ToString()}";
        string uniqueKey = $"{_furniture}_{instanceId}";  // 각 가구 인스턴스마다 고유한 키

        // 새로운 리스트 생성
        drawInfo[_theme].furnitureInstances[uniqueKey] = new List<FurnitureInstance>();
        drawInfo[_theme].furnitureInstances[uniqueKey].Add(new FurnitureInstance(instanceId));
    }

    // 배치 상태 업데이트
    public void UpdateFurniturePlacementStatus(string instanceId, bool isPlaced)
    {
        FurnitureInstance instance = GetFurnitureInstance(instanceId);
        if (instance != null)
        {
            instance.isPlaced = isPlaced;
            return;
        }
    }

    // 배치하지 않은 가구 반환
    public List<FurnitureInstance> GetUnplacedInstances(int theme, string furnitureName)
    {
        if (!drawInfo[theme].furnitureInstances.ContainsKey(furnitureName))
            return new List<FurnitureInstance>();

        return drawInfo[theme].furnitureInstances[furnitureName]
            .Where(x => !x.isPlaced)
            .ToList();
    }


    // 사용자가 보유한 총 가구 개수 반환 (중복 포함)
    public int GetFurnitueCount()
    {
        int count = 0;
        for (int i = 0; i < DRAW_THEME_COUNT; ++i)
        {
            foreach (var furnitureList in drawInfo[i].furnitureInstances.Values)
                count += furnitureList.Count;
        }
        return count;
    }

    // 가구의 고유 ID 값을 제외한 이름 반환
    public Furniture GetFurnitureData(int theme, string furnitureName)
    {
        // furnitureName에서 실제 가구 이름만 추출 (예: "chair_1234567" -> "chair")
        string actualName = furnitureName.Split('_')[0];
        return drawdatas[theme].furnitures.Find(x => x.name == actualName);
    }

    // 고유 ID를 통해 가구 인스턴스 반환
    public FurnitureInstance GetFurnitureInstance(string instanceId)
    {
        for (int theme = 0; theme < DRAW_THEME_COUNT; ++theme)
        {
            foreach (var furnitureList in drawInfo[theme].furnitureInstances.Values)
            {
                var instance = furnitureList.Find(x => x.instanceId == instanceId);
                if (instance != null)
                {
                    return instance;
                }
            }
        }
        return null;
    }
}