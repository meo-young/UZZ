using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;

public class GardenPlacement : MonoBehaviour
{
    [SerializeField] private GameObject furniturePrefab;

    private Transform Place;
    private GameObject currentFurniture;


    private void Start() {
        Place = Variable.instance.Init<Transform>(transform.parent, nameof(Place), Place);
    }

    // 완료 버튼 클릭 이벤트
    public void OnPlacementCompleteBtnHandler()
    {
        transform.localScale = Vector3.zero;
    }
    public void OnPlacementBtnHandler(string imagePath)
    {
        // 배치할 가구 생성
        currentFurniture = Instantiate(furniturePrefab, Place);
        RectTransform rectTransform = currentFurniture.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = Vector2.zero;

        // 배치할 가구 이미지 가져오기
        Image furnitureImage = currentFurniture.GetComponent<Image>();

        // 배치할 가구 이미지 할당 및 크기 설정
        AddressableManager.instance.LoadSprite(imagePath, furnitureImage);
        
        // 이미지 로드가 완료된 후에 크기 설정하도록 콜백에서 처리
        Addressables.LoadAssetAsync<Sprite>(imagePath).Completed += (operation) =>
        {
            furnitureImage.sprite = operation.Result;
            furnitureImage.SetNativeSize();
            rectTransform.sizeDelta = operation.Result.rect.size;
        };
    }
}
