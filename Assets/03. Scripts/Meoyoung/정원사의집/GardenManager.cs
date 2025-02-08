using UnityEngine;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;

[System.Serializable]
public class FurniturePlacementData
{
    public string furnitureInstanceId; // 가구 인스턴스 ID
    public int themeIndex;            // 가구 테마 인덱스
    public string furnitureName;       // 가구 이름
    public Vector3 position;           // 배치 위치
    public bool isLocked;             // 잠금 여부
    public bool isFlipped;            // 뒤집힘 여부
}

public class GardenManager : MonoBehaviour
{
    public static GardenManager instance;
    public SerializedDictionary<string, FurniturePlacementData> gardenInfo = new();

    private async void Awake()
    {
        if (instance == null)
            instance = this;

        await DataManager.instance.JsonLoadAsync();
    }

    public void UpdatePlacement(int themeIndex, string furnitureName, string instanceId, Vector3 position, bool isLocked, bool isFlipped)
    {
        var temp = DrawManager.instance.GetFurnitureInstance(instanceId);
        if (temp != null && temp.isPlaced)
        {
            Debug.Log("이미 배치된 가구입니다.");
            return;
        }

        var placement = new FurniturePlacementData
        {
            themeIndex = themeIndex,
            furnitureName = furnitureName,
            furnitureInstanceId = instanceId,
            position = position,
            isLocked = isLocked,
            isFlipped = isFlipped
        };

        gardenInfo[instanceId] = placement;
        DrawManager.instance.UpdateFurniturePlacementStatus(instanceId, true);
    }

    public void UpdateExistingPlacement(string instanceId, Vector3 position, bool isLocked, bool isFlipped)
    {
        if (gardenInfo.TryGetValue(instanceId, out var placement))
        {
            placement.position = position;
            placement.isLocked = isLocked;
            placement.isFlipped = isFlipped;
            DrawManager.instance.UpdateFurniturePlacementStatus(placement.furnitureInstanceId, true);
        }
    }

    // 새로운 메서드 추가
    public void UpdatePlacementWithoutCheck(int themeIndex, string furnitureName, string instanceId, Vector3 position, bool isLocked, bool isFlipped)
    {
        var placement = new FurniturePlacementData
        {
            themeIndex = themeIndex,
            furnitureName = furnitureName,
            furnitureInstanceId = instanceId,
            position = position,
            isLocked = isLocked,
            isFlipped = isFlipped
        };

        gardenInfo[instanceId] = placement;
        // 여기서만 isPlaced를 업데이트
        DrawManager.instance.UpdateFurniturePlacementStatus(instanceId, true);
    }



    public void RemovePlacement(string placementId)
    {
        if (gardenInfo.TryGetValue(placementId, out var placement))
        {
            DrawManager.instance.UpdateFurniturePlacementStatus(placement.furnitureInstanceId, false);
            gardenInfo.Remove(placementId);
        }
    }

    public void RemoveAllPlacement()
    {
        foreach (var placement in gardenInfo.Values)
        {
            DrawManager.instance.UpdateFurniturePlacementStatus(placement.furnitureInstanceId, false);
        }
        gardenInfo.Clear();
    }

    public List<FurniturePlacementData> GetAllPlacements()
    {
        return new List<FurniturePlacementData>(gardenInfo.Values);
    }
}