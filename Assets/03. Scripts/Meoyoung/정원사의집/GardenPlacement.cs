using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;

public class GardenPlacement : MonoBehaviour
{
    [SerializeField] private GameObject furniturePrefab;
    [SerializeField] private Transform Place;

    private Transform LockBtn;
    private Transform UnlockBtn;

    private Transform RightButtonPanel;
    private Transform LeftButtonPanel;
    private GameObject currentFurniture;
    private Furniture currentFurnitureData;
    private int currentTheme;
    private string currentInstanceId;

    private void Awake()
    {
        LockBtn = Variable.instance.Init<Transform>(transform, nameof(LockBtn), LockBtn);
        UnlockBtn = Variable.instance.Init<Transform>(transform, nameof(UnlockBtn), UnlockBtn);

        LockBtn.localScale = Vector3.zero;
        UnlockBtn.localScale = Vector3.zero;
    }

    private async void Start()
    {
        RightButtonPanel = Variable.instance.Init<Transform>(transform.parent, nameof(RightButtonPanel), RightButtonPanel);
        LeftButtonPanel = Variable.instance.Init<Transform>(transform.parent, nameof(LeftButtonPanel), LeftButtonPanel);

        transform.localScale = Vector3.zero;
        await DataManager.instance.JsonLoadAsync();
        LoadSavedPlacements();
        Place.localScale = Vector3.zero;
    }

    private void LoadSavedPlacements()
    {
        var placements = GardenManager.instance.GetAllPlacements();
        foreach (var placement in placements)
        {
            Furniture furnitureData = DrawManager.instance.GetFurnitureData(placement.themeIndex, placement.furnitureName);
            if (furnitureData != null)
            {
                SpawnSavedFurniture(furnitureData, placement);
            }
        }
    }

    private void SpawnSavedFurniture(Furniture _furniture, FurniturePlacementData _placement)
    {
        GameObject furniture = Instantiate(furniturePrefab, Place);
        furniture.transform.position = _placement.position;

        SpriteRenderer spriteRenderer = furniture.GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = _furniture.layer;

        BoxCollider boxCollider = furniture.GetComponent<BoxCollider>();

        var dragItem = furniture.GetComponent<DragItem>();
        dragItem.placementId = _placement.id;
        dragItem.furnitureInstanceId = _placement.furnitureInstanceId;
        dragItem.isLocekd = _placement.isLocked;
        dragItem.furnitureData = _furniture;
        dragItem.themeIndex = _placement.themeIndex;

        if (_placement.isFlipped)
            furniture.transform.Rotate(0, 180, 0);

        // 스프라이트 로드 후 코너 초기화
        Addressables.LoadAssetAsync<Sprite>(_furniture.image).Completed += (operation) =>
        {
            spriteRenderer.sprite = operation.Result;
            if (spriteRenderer.sprite != null)
            {
                boxCollider.size = spriteRenderer.sprite.bounds.size;
                dragItem.Select();  // 코너 초기화
                dragItem.HideCorners();  // 초기에는 코너 숨기기
            }
        };
    }

    public void OnPlacementBtnHandler(Furniture _furniture, int _theme, string instanceId)
    {
        var instance = DrawManager.instance.GetFurnitureInstance(instanceId);
        if (instance != null && instance.isPlaced)
        {
            for(int i=0; i<Place.childCount; i++)
            {
                var child = Place.GetChild(i);
                var sameItem = child.GetComponent<DragItem>();
                if(sameItem.furnitureInstanceId == instanceId)
                {
                    SetCurrentFurniture(child.gameObject);
                    return;
                }
            }
            return;
        }

        currentTheme = _theme;
        currentFurnitureData = _furniture;
        currentInstanceId = instanceId;

        string newPlacementId = System.Guid.NewGuid().ToString();

        currentFurniture = Instantiate(furniturePrefab, Place);

        SpriteRenderer spriteRenderer = currentFurniture.GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = _furniture.layer;

        BoxCollider boxCollider = currentFurniture.GetComponent<BoxCollider>();
        boxCollider.size = spriteRenderer.bounds.size;

        var dragItem = currentFurniture.GetComponent<DragItem>();
        dragItem.placementId = newPlacementId;
        dragItem.furnitureInstanceId = currentInstanceId;
        dragItem.isLocekd = false;
        dragItem.furnitureData = _furniture;
        dragItem.themeIndex = _theme;
        UpdateLockBtn();


        Addressables.LoadAssetAsync<Sprite>(_furniture.image).Completed += (operation) =>
        {
            spriteRenderer.sprite = operation.Result;
            if (spriteRenderer.sprite != null)
            {
                boxCollider.size = spriteRenderer.sprite.bounds.size;
                dragItem.Select();
            }
        };
    }

    public void SetCurrentFurniture(GameObject furniture)
    {
        if (currentFurniture != null && currentFurniture != furniture)
        {
            var prevDragItem = currentFurniture.GetComponent<DragItem>();
            var instance = DrawManager.instance.GetFurnitureInstance(prevDragItem.furnitureInstanceId);

            if (instance != null)
            {
                if (!instance.isPlaced)
                {
                    // 배치되지 않은 가구는 삭제
                    DrawManager.instance.UpdateFurniturePlacementStatus(prevDragItem.furnitureInstanceId, false);
                    Destroy(currentFurniture);
                }
                else
                {
                    // 배치된 가구는 원래 위치로 복원
                    prevDragItem.ResetPosition();
                    prevDragItem.Deselect();
                }
            }
        }

        currentFurniture = furniture;
        if (currentFurniture != null)
        {
            var dragItem = currentFurniture.GetComponent<DragItem>();
            currentTheme = dragItem.themeIndex;
            currentFurnitureData = dragItem.furnitureData;
            currentInstanceId = dragItem.furnitureInstanceId;
            dragItem.StartDrag();  // 드래그 시작 시 원래 위치 저장
            dragItem.Select();

            UpdateLockBtn();
        }
    }

    private void UpdateFurniturePlacement(DragItem dragItem, bool isNewPlacement = false)
    {
        bool isFlipped = dragItem.transform.rotation.y == 180;

        if (isNewPlacement)
        {
            GardenManager.instance.UpdatePlacementWithoutCheck(
                currentTheme,
                currentFurnitureData.name,
                dragItem.furnitureInstanceId,
                dragItem.transform.position,
                dragItem.isLocekd,
                isFlipped
            );
        }
        else
        {
            GardenManager.instance.UpdateExistingPlacement(
                dragItem.placementId,
                dragItem.transform.position,
                dragItem.isLocekd,
                isFlipped
            );
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        var gardenUI = transform.parent.GetComponent<GardenUI>();
        if (gardenUI != null)
        {
            gardenUI.UpdateFurnitureContent(gardenUI.GetCurrentThemeIndex());
        }
    }

    public void OnPlacementCompleteBtnHandler()
    {
        InitUI();
        if (currentFurniture != null)
        {
            DragItem dragItem = currentFurniture.GetComponent<DragItem>();
            dragItem.Deselect();
            dragItem.EndDrag();  // 배치 완료 시 드래그 상태 종료

            var instance = DrawManager.instance.GetFurnitureInstance(dragItem.furnitureInstanceId);
            bool isNewPlacement = instance != null && !instance.isPlaced;
            UpdateFurniturePlacement(dragItem, isNewPlacement);
        }
    }

    private void InitUI()
    {
        transform.localScale = Vector3.zero;
        RightButtonPanel.localScale = Vector3.one;
        LeftButtonPanel.localScale = Vector3.one;
    }

    public void OnContainBtnHandler()
    {
        if (currentFurniture == null) return;

        string placementId = currentFurniture.GetComponent<DragItem>().placementId;
        Destroy(currentFurniture);
        GardenManager.instance.RemovePlacement(placementId);
        InitUI();
        UpdateUI();
    }

    public void OnAllContainBtnHandler()
    {
        for (int i = 0; i < Place.childCount; i++)
        {
            Destroy(Place.GetChild(i).gameObject);
        }
        InitUI();
        GardenManager.instance.RemoveAllPlacement();
        UpdateUI();
    }

    private void UpdateLockBtn()
    {
        if (currentFurniture != null)
        {
            if (currentFurniture.GetComponent<DragItem>().isLocekd)
            {
                LockBtn.localScale = Vector3.zero;
                UnlockBtn.localScale = Vector3.one;
            }
            else
            {
                LockBtn.localScale = Vector3.one;
                UnlockBtn.localScale = Vector3.zero;
            }
        }
    }

    public void OnLockBtnHandler()
    {
        if (currentFurniture == null) return;
        currentFurniture.GetComponent<DragItem>().isLocekd = true;
        UpdateCurrentFurniturePlacement();
        UpdateLockBtn();
    }

    public void OnUnlockBtnHandler()
    {
        if (currentFurniture == null) return;
        currentFurniture.GetComponent<DragItem>().isLocekd = false;
        UpdateCurrentFurniturePlacement();
        UpdateLockBtn();
    }

    public void OnFlipBtnHandler()
    {
        if (currentFurniture == null) return;
        currentFurniture.transform.Rotate(0, 180, 0);
        UpdateCurrentFurniturePlacement();
    }

    private void UpdateCurrentFurniturePlacement()
    {
        if (currentFurniture == null) return;
        UpdateFurniturePlacement(currentFurniture.GetComponent<DragItem>(), false);
    }
}