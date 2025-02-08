using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;

public class GardenPlacement : MonoBehaviour
{
    [SerializeField] private GameObject furniturePrefab;
    [SerializeField] private Transform Place;

    private Transform RightButtonPanel;
    private Transform LeftButtonPanel;
    private GameObject currentFurniture;
    private Furniture currentFurnitureData;
    private int currentTheme;

    private async void Start() {
        RightButtonPanel =  Variable.instance.Init<Transform>(transform.parent, nameof(RightButtonPanel), RightButtonPanel);
        LeftButtonPanel =   Variable.instance.Init<Transform>(transform.parent, nameof(LeftButtonPanel), LeftButtonPanel);

        transform.localScale = Vector3.zero;

        await DataManager.instance.JsonLoadAsync();
        LoadSavedPlacements();

        Place.localScale = Vector3.zero;
    }

    private void LoadSavedPlacements()
    {
        var placements = GardenManager.instance.GetAllPlacements();

        foreach(var placement in placements)
        {
            if(DrawManager.instance.drawInfo[placement.themeIndex].myFurnitures.ContainsKey(placement.furnitureName))
            {

                Furniture furnitureData = DrawManager.instance.GetFurnitureData(placement.themeIndex, placement.furnitureName);
                if(furnitureData != null)
                {
                    SpawnSavedFurniture(furnitureData, placement);
                }
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

        Addressables.LoadAssetAsync<Sprite>(_furniture.image).Completed += (operation) =>
        {
            spriteRenderer.sprite = operation.Result;

            if(spriteRenderer.sprite != null)
            {
                boxCollider.size = spriteRenderer.sprite.bounds.size;
            }
        };

        var dragItem = furniture.GetComponent<DragItem>();
        dragItem.placementId = _placement.id;
        dragItem.isLocekd = _placement.isLocked;

        if(_placement.isFlipped)
            furniture.transform.Rotate(0, 180, 0);
    }

    private void InitUI()
    {
        transform.localScale = Vector3.zero;
        RightButtonPanel.localScale = Vector3.one;
        LeftButtonPanel.localScale = Vector3.one;
    }

    // 완료 버튼 클릭 이벤트
    public void OnPlacementCompleteBtnHandler()
    {
        InitUI();
        UpdateCurrentFurniturePlacement();
    }
    public void OnPlacementBtnHandler(Furniture _furniture, int _theme)
    {
        // 현재 테마 설정
        currentTheme = _theme;
        
        // 현재 가구 정보 설정
        currentFurnitureData = _furniture;

        // 배치할 가구 생성
        currentFurniture = Instantiate(furniturePrefab, Place);

        // SpriteRenderer 컴포넌트 가져오기
        SpriteRenderer spriteRenderer = currentFurniture.GetComponent<SpriteRenderer>();

        // Order in Layer 설정
        spriteRenderer.sortingOrder = _furniture.layer;

        // BoxCollider 컴포넌트 가져온 후 크기 설정
        BoxCollider boxCollider = currentFurniture.GetComponent<BoxCollider>();
        boxCollider.size = spriteRenderer.bounds.size;

        // 이미지 할당 후, 이미지 크기에 맞게 BoxCollider 크기 설정
        Addressables.LoadAssetAsync<Sprite>(_furniture.image).Completed += (operation) =>
        {
            spriteRenderer.sprite = operation.Result;

            if(spriteRenderer.sprite != null)
            {
                boxCollider.size = spriteRenderer.sprite.bounds.size;
            }
        };

        // 가구 배치 정보 업데이트
        var dragItem = currentFurniture.GetComponent<DragItem>();
        dragItem.placementId = GardenManager.instance.GetCurrentPlacementId();
    }

    // 담기 버튼 이벤트
    public void OnContainBtnHandler()
    {
        // 현재 가구의 배치 ID 가져오기
        string placementId = currentFurniture.GetComponent<DragItem>().placementId;
        
        // 배치할 가구 삭제
        Destroy(currentFurniture);
        
        // GardenManager에서 해당 배치 정보 삭제
        GardenManager.instance.RemovePlacement(placementId);
        
        InitUI();
    }

    // 모두 담기 버튼 이벤트
    public void OnAllContainBtnHandler()
    {
        // 배치한 모든 가구 삭제
        for(int i = 0; i < Place.childCount; i++)
        {
            Destroy(Place.GetChild(i).gameObject);
        }

        InitUI();

        GardenManager.instance.RemoveAllPlacement();
    }

    // 잠김 버튼 이벤트
    public void OnLockBtnHandler()
    {
        currentFurniture.GetComponent<DragItem>().isLocekd = true;
        UpdateCurrentFurniturePlacement();
    }

    // 잠금 해제 버튼 이벤트
    public void OnUnlockBtnHandler()
    {
        currentFurniture.GetComponent<DragItem>().isLocekd = false;
        UpdateCurrentFurniturePlacement();
    }

    public void OnFlipBtnHandler()
    {
        currentFurniture.transform.Rotate(0, 180, 0);
        UpdateCurrentFurniturePlacement();
    }


    private void UpdateCurrentFurniturePlacement()
    {
        DragItem dragItem = currentFurniture.GetComponent<DragItem>();
        bool isFlipped = currentFurniture.transform.rotation.y == 180;

        GardenManager.instance.UpdatePlacement(
            currentTheme,                           // 테마 번호
            currentFurnitureData.name,                  // 가구 이름
            currentFurniture.transform.position,    // 가구 위치
            dragItem.isLocekd,                      // 잠김 여부
            isFlipped);                             // 뒤집힘 여부
    }
}
