using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GardenUI : MonoBehaviour
{
    [SerializeField] private GameObject FurniturePrefab;    // 가구 프리팹
    [SerializeField] private Transform Place;           // 가구가 배치될 오브젝트

    private TMP_Text FurnitureCountText;                // 가구 수 텍스트
    private RectTransform RightButtonPanel;             // 오른쪽 버튼 패널
    private RectTransform LeftButtonPanel;             // 왼쪽 버튼 패널
    private Image HideButton;                           // 숨기기 버튼 이미지
    private Transform FurnitureContent;                 // 가구 컨텐츠
    private Transform ThemeContent;                   // 테마 컨텐츠
    private Transform Placement;                       // 가구 배치 오브젝트

    private Image currentFurnitureImage;                 // 현재 선택된 이미지
    private Image currentThemeImage;                    // 현재 선택된 테마 이미지

    private GardenPlacement currentPlacement;

    private void Start()
    {
        RightButtonPanel =      Variable.instance.Init<RectTransform>(transform, nameof(RightButtonPanel), RightButtonPanel);
        LeftButtonPanel =       Variable.instance.Init<RectTransform>(transform, nameof(LeftButtonPanel), LeftButtonPanel);
        HideButton =            Variable.instance.Init<Image>(transform, nameof(HideButton), HideButton);
        FurnitureCountText =    Variable.instance.Init<TMP_Text>(transform, nameof(FurnitureCountText), FurnitureCountText);
        FurnitureContent =      Variable.instance.Init<Transform>(transform, nameof(FurnitureContent), FurnitureContent);
        ThemeContent =          Variable.instance.Init<Transform>(transform, nameof(ThemeContent), ThemeContent);
        Placement =             Variable.instance.Init<Transform>(transform, nameof(Placement), Placement);
        
        currentPlacement =      Placement.GetComponent<GardenPlacement>();

        UpdateThemeContent();

        transform.localScale = Vector3.zero;
    }

    // 정원사의 집 UI 켜기
    public void OnGardenBtnHandler()
    {
        UpdateGardenData();
        UpdateFurnitureContent(0);

        transform.localScale = Vector3.one;
        Place.localScale = Vector3.one;
    }

    // 정원사의 집 UI 끄기
    public void OnGardenBackBtnHandler()
    {
        SoundManager.instance.PlaySFX(SFX.Diary.BUTTON);
        transform.localScale = Vector3.zero;
        Place.localScale = Vector3.zero;
    }

    // 뽑기, 메인 버튼 활성화
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
        // 이전에 선택한 이미지는 Default로
        if (currentThemeImage != null && _image != currentThemeImage)
            AddressableManager.instance.LoadSprite("theme_check", currentThemeImage);

        // 현재 선택한 이미지는 Focus로
        currentThemeImage = _image;
        AddressableManager.instance.LoadSprite("check", currentThemeImage);

        UpdateFurnitureContent(_theme);
    }


    private void UpdateThemeContent()
    {
        for (int i = 0; i < ThemeContent.childCount; ++i)
        {
            // 현재 i 값을 로컬 변수에 저장
            int themeIndex = i;

            Transform child = ThemeContent.GetChild(i);
            Image image = child.GetComponentInChildren<Image>();
            Button button = child.GetComponentInChildren<Button>();

            // themeIndex 사용
            button.onClick.AddListener(() => OnThemeBtnHandler(themeIndex, image));

            if (themeIndex == 0)
                OnThemeBtnHandler(0, image);
        }
    }



    // 가구 컨텐츠 업데이트
    private void UpdateFurnitureContent(int _theme)
    {
        // 기존 컨텐츠 제거
        foreach (Transform child in FurnitureContent)
        {
            Destroy(child.gameObject);
        }

        // 가구 목록 생성
        foreach (var furniture in DrawManager.instance.drawInfo[_theme].myFurnitures)
        {
            for (int i = 0; i < furniture.Value; ++i)
            {
                // 프리팹 생성
                GameObject obj = Instantiate(FurniturePrefab, FurnitureContent);

                Image[] images = obj.GetComponentsInChildren<Image>();

                // 뒷배경 이미지 가져옴
                Image background = images[0];

                // 해당 가구 이미지로 교체
                Image icon = images[1];

                // 버튼 가져옴
                Button btn = obj.GetComponentInChildren<Button>();

                // 가구 데이터 가져오기
                Furniture furnitureData = DrawManager.instance.GetFurnitureData(0, furniture.Key);

                // 버튼 클릭 이벤트 추가
                btn.onClick.AddListener(() => OnFurnitureBtnHandler(furnitureData, _theme));

                // 아이콘 로드
                if (furnitureData != null)
                    AddressableManager.instance.LoadSprite(furnitureData.icon, icon);
            }
        }
    }

    // 가구 버튼 클릭 이벤트
    private void OnFurnitureBtnHandler(Furniture _furniture, int _theme)
    {
        // 배치할 가구 이미지 로드
        currentPlacement.OnPlacementBtnHandler(_furniture, _theme);

        // 가구 배치 오브젝트 스케일 1로
        Placement.localScale = Vector3.one;

        // 좌, 우 패널 숨기기
        LeftButtonPanel.localScale = Vector3.zero;
        RightButtonPanel.localScale = Vector3.zero;
    }


    private void UpdateGardenData()
    {
        // 가구 수 업데이트
        FurnitureCountText.text = DrawManager.instance.GetFurnitueCount().ToString() + "개";
    }
}
