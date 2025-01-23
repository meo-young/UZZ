using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class FurnitureSubResult : MonoBehaviour
{
    private Button skipBtn;
    private Image furnitureImage;
    private Image starImage;
    private TMP_Text furnitureNameText;
    private TMP_Text furnitureFlavorText;

    // 5회 뽑기 데이터를 저장할 배열
    private Furniture[] furnitures;
    private Furniture currentFurniture;
    private int currentFurnitureIndex;

    private FurnitureMainResult furnitureMainResult;

    private void Awake() {
        Image[] images = GetComponentsInChildren<Image>();
        TMP_Text[] texts = GetComponentsInChildren<TMP_Text>();

        skipBtn = GetComponentsInChildren<Button>()[1];
        furnitureImage = images[1];
        starImage = images[2];
        furnitureNameText = texts[0];
        furnitureFlavorText = texts[1];

        furnitureMainResult = FindFirstObjectByType<FurnitureMainResult>();
    }

    private void Start() {
        Init();
    }
    

    public void SetOneFurniture(Furniture _furniture)
    {
        // 스킵 버튼 비활성화
        skipBtn.gameObject.SetActive(false);

        // 가구 데이터 설정
        currentFurniture = _furniture;

        // 가구 정보 설정
        SetFurniture(currentFurniture);
    }

    public void SetFurnitures(Furniture[] _furnitures)
    {
        // 스킵 버튼 활성화
        skipBtn.gameObject.SetActive(true);

        // 5회 뽑기 데이터 설정
        furnitures = _furnitures;

        // 맨 첫 가구를 보여줌
        SetFurniture(furnitures[currentFurnitureIndex++]);
    }


    void SetFurniture(Furniture _furniture)
    {
        // 가구 아이콘 설정
        AddressableManager.instance.LoadSprite(_furniture.icon, furnitureImage);
        
        // 가구 이름 설정
        furnitureNameText.text = _furniture.name;

        // 가구 플레이버 텍스트 설정
        furnitureFlavorText.text = _furniture.flavorText;

        // 가구 등급에 따라 별 아이콘 설정
        AddressableManager.instance.LoadSprite("Star_" + _furniture.rank, starImage);
    }

    public void OnNextFurnitureBtnHandler()
    {
        // 1회 뽑기라면 바로 스킵
        if(furnitures == null)
        {
            Init();
            furnitureMainResult.ShowOneDrawResult(currentFurniture);
            return;
        }

        // 보여줄 가구 데이터가 있다면 다음 가구를 보여줌
        if(currentFurnitureIndex < furnitures.Length)
            SetFurniture(furnitures[currentFurnitureIndex++]);
        else
            Skip();
    }

    public void OnSkipBtnHandler()
    {
        Skip();
    }

    void Skip()
    {
        furnitureMainResult.ShowFiveDrawResult(furnitures);
        Init();
    }

    void Init()
    {
        furnitures = null;
        currentFurnitureIndex = 0;
        transform.localScale = Vector3.zero;
    }
}
