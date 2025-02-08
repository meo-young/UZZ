using UnityEngine;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;

public class GardenManager : MonoBehaviour
{
    public static GardenManager instance;
    public SerializedDictionary<string, FurniturePlacementData> gardenInfo = new();
    private int currentPlacementId = 0;

    private async void Awake()
    {
        if (instance == null)
            instance = this;

        await DataManager.instance.JsonLoadAsync();
        currentPlacementId = gardenInfo.Count;
    }


    public string GetCurrentPlacementId()
    {
        return currentPlacementId.ToString();
    }

    // 가구 배치 정보 추가/업데이트
    public void UpdatePlacement(int themeIndex, string furnitureName, Vector3 position, bool isLocked, bool isFlipped)
    {
        string placementId = GetCurrentPlacementId();
        
        var placement = new FurniturePlacementData
        {
            id = placementId,
            themeIndex = themeIndex,
            furnitureName = furnitureName,
            position = position,
            isLocked = isLocked,
            isFlipped = isFlipped
        };
        
        gardenInfo[placementId] = placement;
        currentPlacementId++;
        return;
    }

    // 가구 배치 정보 업데이트 (기존 ID 사용)
    public void UpdateExistingPlacement(string placementId, Vector3 position, bool isLocked, bool isFlipped)
    {
        if (gardenInfo.TryGetValue(placementId, out var placement))
        {
            placement.position = position;
            placement.isLocked = isLocked;
            placement.isFlipped = isFlipped;
        }
    }

    // 가구 배치 정보 삭제
    public void RemovePlacement(string placementId)
    {
        Debug.Log("RemovePlacement : " + placementId);
        gardenInfo.Remove(placementId);
    }

    // 가구 배치 정보 모두 삭제
    public void RemoveAllPlacement()
    {
        gardenInfo.Clear();
        currentPlacementId = 0;
    }

    // 저장된 모든 배치 정보 가져오기
    public List<FurniturePlacementData> GetAllPlacements()
    {
        return new List<FurniturePlacementData>(gardenInfo.Values);
    }
}

[System.Serializable]
public class FurniturePlacementData
{
    public string id;             // 배치 고유 ID
    public int themeIndex;        // 가구 테마 인덱스
    public string furnitureName;  // 가구 이름
    public Vector3 position;      // 배치 위치
    public bool isLocked;         // 잠금 여부
    public bool isFlipped;        // 뒤집힘 여부
}