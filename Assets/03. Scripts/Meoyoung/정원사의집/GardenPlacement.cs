using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;

public class GardenPlacement : MonoBehaviour
{
    [SerializeField] private GameObject furniturePrefab;
    [SerializeField] private Transform Place;
    [SerializeField] private RectTransform dragBoundary; // Inspector에서 할당할 드래그 범위 RectTransform
    [SerializeField] private Transform RightButtonPanel;
    [SerializeField] private Transform LeftButtonPanel;

    private Transform LockBtn;
    private Transform UnlockBtn;
    private GameObject currentFurniture;
    private Furniture currentFurnitureData;
    private int currentTheme;
    private string currentInstanceId;
    private GardenUI gardenUI;

    private DragItem currentDragItem => currentFurniture.GetComponent<DragItem>();




    private void Awake()
    {
        gardenUI = FindFirstObjectByType<GardenUI>();
        LockBtn = Variable.instance.Init<Transform>(transform, nameof(LockBtn), LockBtn);
        UnlockBtn = Variable.instance.Init<Transform>(transform, nameof(UnlockBtn), UnlockBtn);

        LockBtn.localScale = Vector3.zero;
        UnlockBtn.localScale = Vector3.zero;
    }

    private async void Start()
    {
        transform.localScale = Vector3.zero;
        LoadSavedPlacements();
        Place.localScale = Vector3.zero;
    }

    // 배치했던 가구 복원
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

    // 배치했던 가구 복원
    private void SpawnSavedFurniture(Furniture _furniture, FurniturePlacementData _placement)
    {
        GameObject furniture = Instantiate(furniturePrefab, Place);
        furniture.transform.position = _placement.position;

        SpriteRenderer spriteRenderer = furniture.GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = _furniture.layer;

        BoxCollider boxCollider = furniture.GetComponent<BoxCollider>();

        var dragItem = furniture.GetComponent<DragItem>();
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


    public RectTransform GetDragBoundary()
    {
        return dragBoundary;
    }

    // 가구 배치 버튼 클릭 시 호출
    public void OnPlacementBtnHandler(Furniture _furniture, int _theme, string instanceId)
    {
        var instance = DrawManager.instance.GetFurnitureInstance(instanceId);
        if (instance != null && instance.isPlaced)
        {
            for (int i = 0; i < Place.childCount; i++)
            {
                var child = Place.GetChild(i);
                var sameItem = child.GetComponent<DragItem>();
                if (sameItem.furnitureInstanceId == instanceId)
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
        currentFurniture = Instantiate(furniturePrefab, Place);

        SpriteRenderer spriteRenderer = currentFurniture.GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = _furniture.layer;

        BoxCollider boxCollider = currentFurniture.GetComponent<BoxCollider>();
        boxCollider.size = spriteRenderer.bounds.size;

        var dragItem = currentDragItem;
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

    // 현재 선택된 가구 설정
    public void SetCurrentFurniture(GameObject furniture)
    {
        if (currentFurniture != null && currentFurniture != furniture)
        {
            var prevDragItem = currentDragItem;
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
            var dragItem = currentDragItem;
            currentTheme = dragItem.themeIndex;
            currentFurnitureData = dragItem.furnitureData;
            currentInstanceId = dragItem.furnitureInstanceId;
            dragItem.StartDrag();  // 드래그 시작 시 원래 위치 저장
            dragItem.Select();

            UpdateLockBtn();
        }

    }
    
    // GardenInfo 정보 업데이트
    private void UpdateFurniturePlacement(DragItem dragItem)
    {
        bool isFlipped = dragItem.transform.rotation.y == 180;

        GardenManager.instance.UpdatePlacement(
            currentTheme,
            dragItem.furnitureData.name,  // 가구의 실제 이름 사용
            dragItem.furnitureInstanceId,
            dragItem.transform.position,
            dragItem.isLocekd,
            isFlipped
        );

        UpdateUI();
    }

    // 가구 목록 다시 그리기
    private void UpdateUI()
    {
        gardenUI.UpdateFurnitureContent(gardenUI.GetCurrentThemeIndex());
    }

    public void OnPlacementCompleteBtnHandler()
    {
        DeactivatePlacementUI();
        if (currentFurniture != null)
        {
            DragItem dragItem = currentDragItem;
            dragItem.Deselect();
            dragItem.EndDrag();

            UpdateFurniturePlacement(dragItem);
        }
    }

    public void OnContainBtnHandler()
    {
        if (currentFurniture == null) return;

        string instanceId = currentDragItem.furnitureInstanceId;

        // 먼저 배치 정보를 제거
        GardenManager.instance.RemovePlacement(instanceId);

        // UI 업데이트
        UpdateUI();

        // 가구 오브젝트 제거
        Destroy(currentFurniture);
        DeactivatePlacementUI();
    }

    public void OnAllContainBtnHandler()
    {
        for (int i = 0; i < Place.childCount; i++)
        {
            Destroy(Place.GetChild(i).gameObject);
        }
        DeactivatePlacementUI();
        GardenManager.instance.RemoveAllPlacement();
        UpdateUI();
    }

    public void OnLockBtnHandler()
    {
        currentDragItem.isLocekd = !currentDragItem.isLocekd;
        UpdateLockBtn();
        UpdateFurniturePlacement(currentDragItem);
    }

    public void OnFlipBtnHandler()
    {
        if (currentFurniture == null) return;
        currentFurniture.transform.Rotate(0, 180, 0);
        UpdateFurniturePlacement(currentDragItem);
    }

    private void UpdateLockBtn()
    {
        if (currentFurniture == null) return;

        if (currentDragItem.isLocekd)
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

    
    private void DeactivatePlacementUI()
    {
        transform.localScale = Vector3.zero;
        RightButtonPanel.localScale = Vector3.one;
        LeftButtonPanel.localScale = Vector3.one;
    }
}