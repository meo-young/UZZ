using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class GardenUI : MonoBehaviour
{
    [SerializeField] private GameObject FurniturePrefab;
    [SerializeField] private Transform Place;
    [SerializeField] private Transform Placement;

    private TMP_Text FurnitureCountText;
    private RectTransform RightButtonPanel;
    private RectTransform LeftButtonPanel;
    private Image HideButton;
    private Transform FurnitureContent;
    private Transform ThemeContent;

    private Image currentFurnitureImage;
    private Image currentThemeImage;
    private GardenPlacement currentPlacement;
    private int currentThemeIndex = 0;

    private void Start()
    {
        RightButtonPanel = Variable.instance.Init<RectTransform>(transform, nameof(RightButtonPanel), RightButtonPanel);
        LeftButtonPanel = Variable.instance.Init<RectTransform>(transform, nameof(LeftButtonPanel), LeftButtonPanel);
        HideButton = Variable.instance.Init<Image>(transform, nameof(HideButton), HideButton);
        FurnitureCountText = Variable.instance.Init<TMP_Text>(transform, nameof(FurnitureCountText), FurnitureCountText);
        FurnitureContent = Variable.instance.Init<Transform>(transform, nameof(FurnitureContent), FurnitureContent);
        ThemeContent = Variable.instance.Init<Transform>(transform, nameof(ThemeContent), ThemeContent);

        currentPlacement = Placement.GetComponent<GardenPlacement>();
        UpdateThemeContent();
        transform.localScale = Vector3.zero;
    }

    public void OnGardenBtnHandler()
    {
        UpdateGardenData();
        UpdateFurnitureContent(0);
        transform.localScale = Vector3.one;
        Place.localScale = Vector3.one;
    }

    public void OnGardenBackBtnHandler()
    {
        SoundManager.instance.PlaySFX(SFX.Diary.BUTTON);
        transform.localScale = Vector3.zero;
        Place.localScale = Vector3.zero;
    }

    public void OnVisibleBtnHandler()
    {
        if (RightButtonPanel.localScale == Vector3.zero)
        {
            RightButtonPanel.localScale = Vector3.one;
            AddressableManager.instance.LoadSprite("eye_1", HideButton);
        }
        else
        {
            RightButtonPanel.localScale = Vector3.zero;
            AddressableManager.instance.LoadSprite("eye_2", HideButton);
        }
    }

    private void OnThemeBtnHandler(int _theme, Image _image)
    {
        if (currentThemeImage != null && _image != currentThemeImage)
            AddressableManager.instance.LoadSprite("theme_check", currentThemeImage);

        currentThemeIndex = _theme;
        currentThemeImage = _image;
        AddressableManager.instance.LoadSprite("check", currentThemeImage);
        UpdateFurnitureContent(_theme);
    }

    private void UpdateThemeContent()
    {
        for (int i = 0; i < ThemeContent.childCount; ++i)
        {
            int themeIndex = i;
            Transform child = ThemeContent.GetChild(i);
            Image image = child.GetComponentInChildren<Image>();
            Button button = child.GetComponentInChildren<Button>();
            button.onClick.AddListener(() => OnThemeBtnHandler(themeIndex, image));

            if (themeIndex == 0)
                OnThemeBtnHandler(0, image);
        }
    }

    public void UpdateFurnitureContent(int _theme)
    {
        foreach (Transform child in FurnitureContent)
        {
            Destroy(child.gameObject);
        }

        // 배치된 가구와 미배치된 가구를 분리하여 저장할 리스트
        var placedFurnitures = new List<(string Key, FurnitureInstance Instance)>();
        var unplacedFurnitures = new List<(string Key, FurnitureInstance Instance)>();

        // 가구들을 배치 여부에 따라 분류
        foreach (var furniture in DrawManager.instance.drawInfo[_theme].furnitureInstances)
        {
            foreach (var instance in furniture.Value)
            {
                if (instance.isPlaced)
                    placedFurnitures.Add((furniture.Key, instance));
                else
                    unplacedFurnitures.Add((furniture.Key, instance));
            }
        }

        // 배치된 가구들을 먼저 생성
        foreach (var item in placedFurnitures)
        {
            CreateFurnitureUI(item.Key, item.Instance, _theme);
        }

        // 그 다음 미배치된 가구들을 생성
        foreach (var item in unplacedFurnitures)
        {
            CreateFurnitureUI(item.Key, item.Instance, _theme);
        }
    }

    // UI 생성 로직을 분리하여 코드 중복 제거
    private void CreateFurnitureUI(string key, FurnitureInstance instance, int theme)
    {
        GameObject obj = Instantiate(FurniturePrefab, FurnitureContent);
        obj.name = instance.instanceId;

        Image[] images = obj.GetComponentsInChildren<Image>();
        Image background = images[0];
        Image icon = images[1];

        Button btn = obj.GetComponentInChildren<Button>();

        // 실제 가구 이름 추출 (인스턴스 ID 제거)
        string actualFurnitureName = key.Split('_')[0];
        Furniture furnitureData = DrawManager.instance.GetFurnitureData(theme, actualFurnitureName);

        // isPlaced 상태에 따라 배경 이미지 설정
        if (instance.isPlaced)
        {
            AddressableManager.instance.LoadSprite("check", background);
        }
        else
        {
            AddressableManager.instance.LoadSprite("non_check", background);
        }

        btn.onClick.AddListener(() => OnFurnitureBtnHandler(furnitureData, theme, instance.instanceId));

        if (furnitureData != null)
        {
            AddressableManager.instance.LoadSprite(furnitureData.icon, icon);
        }
    }

    // UI 관련 동작만 처리하는 새로운 메서드 추가
    public void UpdatePlacementUI()
    {
        Placement.localScale = Vector3.one;
        LeftButtonPanel.localScale = Vector3.zero;
        RightButtonPanel.localScale = Vector3.zero;
    }

    public void OnFurnitureBtnHandler(Furniture _furniture, int _theme, string instanceId)
    {
        currentPlacement.OnPlacementBtnHandler(_furniture, _theme, instanceId);
        UpdatePlacementUI();
    }

    private void UpdateGardenData()
    {
        FurnitureCountText.text = DrawManager.instance.GetFurnitueCount().ToString() + "개";
    }

    public int GetCurrentThemeIndex()
    {
        return currentThemeIndex;
    }
}