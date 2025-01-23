using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Constant;


public class NurungUI : MonoBehaviour
{
    [Header("# Draw UI Info")]
    [SerializeField] GameObject drawPanel;

    [Header("# Nurung UI Info")]
    [SerializeField] TMP_Text TitleText;
    [SerializeField] TMP_Text dewText;
    [SerializeField] TMP_Text oneDrawText;
    [SerializeField] TMP_Text fiveDrawText;
    [SerializeField] TMP_Text oneDrawDewCostText;
    [SerializeField] TMP_Text oneDrawGlassCostText;
    [SerializeField] TMP_Text fiveDrawDewCostText;
    [SerializeField] TMP_Text fiveDrawGlassCostText;
    [Space(10)]
    [SerializeField] Transform contentPanel;
    [SerializeField] ScrollRect scrollRect;
    [Space(10)]
    [SerializeField] GameObject oneDrawUI;
    [SerializeField] GameObject fiveDrawUI;
    [SerializeField] GameObject makeFurnitureUI;

    [Header("# Furniture Result UI")]
    [SerializeField] GameObject resultUI;
    [SerializeField] GameObject furnitureItemPrefab;

    private Furniture[] furnitures;
    private Furniture[] selectedFurnitures;
    private FurnitureSubResult furnitureSubResult;
    private int drawThemeIndex;



    private void Awake() {
        oneDrawDewCostText.text = DRAW_ONE_DEW_COST.ToString();
        oneDrawGlassCostText.text = DRAW_ONE_GLASS_COST.ToString();
        fiveDrawDewCostText.text = DRAW_FIVE_DEW_COST.ToString();
        fiveDrawGlassCostText.text = DRAW_FIVE_GLASS_COST.ToString();

        furnitureSubResult = FindFirstObjectByType<FurnitureSubResult>();

        // 뽑은 가구 배열 초기화
        selectedFurnitures = new Furniture[DRAW_MAX_COUNT];
    }


    public void OnDisable()
    {
        for(int i=0; i<contentPanel.childCount; i++)
        {
            Destroy(contentPanel.GetChild(i).gameObject);
        }

        scrollRect.normalizedPosition = new Vector2(0, 1); // 위쪽으로 스크롤 초기화
    }

    public void InitTextInfo(string _title, string _dew)            // 타이틀, 가루 텍스트 초기화
    {
        TitleText.text = _title;
        dewText.text = "가루 " + _dew;
    }

    public void OnNurungBtnHandler(int index)                                // Nurung UI 켜는 버튼
    {
        if(!this.gameObject.activeSelf) 
            this.gameObject.SetActive(true);

        drawThemeIndex = index;
    }

    public void InitFurnitueList(Furniture[] _furnitures)
    {
        furnitures = _furnitures;
        
        // 기존 컨텐츠 삭제
        for (int i = 0; i < contentPanel.childCount; i++)
        {
            Destroy(contentPanel.GetChild(i).gameObject);
        }
        
        // 새로운 가구 아이템들 생성
        foreach (var furniture in furnitures)
        {
            GameObject item = Instantiate(furnitureItemPrefab, contentPanel);
            
            // 가구 정보 설정
            var image = item.GetComponentInChildren<Image>();
            
            AddressableManager.instance.LoadSprite(furniture.icon, image);
        }
    }


    public void OnPrevBtnHandler()                                  // 이전 버튼 함수
    {
        if (!drawPanel.activeSelf)
            drawPanel.SetActive(true);

        if(this.gameObject.activeSelf)
            this.gameObject.SetActive(false);
    }

    public void OnSubOneDrawBtnHandler()                               // 1회 뽑기창 활성화
    {
        if(!oneDrawUI.activeSelf)
            oneDrawUI.SetActive(true);

        oneDrawText.text = DrawManager.instance.drawdatas[drawThemeIndex].title + "\n1회 뽑기를 진행할까 ?";
        SoundManager.instance.PlaySFX(SFX.Diary.BUTTON);
    }

    public void OnSubOneDrawCloseBtnHandler()                          // 1회 뽑기창 닫기
    {
        if(oneDrawUI.activeSelf)
            oneDrawUI.SetActive(false);
    }

    public void OnMainOneDrawBtnHandler()                               // 1회 뽑기 시작
    {
        // 돈 감소하는 로직 구현 필요
        selectedFurnitures[0] = OneDraw();
        furnitureSubResult.SetFurniture(selectedFurnitures[0]);
        StartCoroutine(ShowMakeFurnitureUI());

        if (oneDrawUI.activeSelf)
            oneDrawUI.SetActive(false);
    }

    Furniture OneDraw()
    {
        float totalProbability = 0f;
        for(int i=0; i<furnitures.Length; i++)
        {
            totalProbability += furnitures[i].probability;
        }

        float randomNum = Random.Range(0, totalProbability);

        float cumulativeWeight = 0f;

        for(int i=0; i<furnitures.Length; i++)
        {
            cumulativeWeight += furnitures[i].probability;
            if (randomNum < cumulativeWeight)
            {
                DrawManager.instance.AddFurniture(furnitures[i].name);
                return furnitures[i];
            }
        }

        return new Furniture();
    }

    // 5회 뽑기
    public void OnSubFiveDrawBtnHandler()                              
    {
        if(!fiveDrawUI.activeSelf)
            fiveDrawUI.SetActive(true);

        fiveDrawText.text = DrawManager.instance.drawdatas[drawThemeIndex].title + "\n5회 뽑기를 진행할까 ?";
        SoundManager.instance.PlaySFX(SFX.Diary.BUTTON);
    }

    // 5회 뽑기 닫기
    public void OnSubFiveDrawCloseBtnHandler()                         
    {
        if(fiveDrawUI.activeSelf)
            fiveDrawUI.SetActive(false);
    }

    // 5회 뽑기 시작
    public void OnMainFiveDrawBtnHandler()                              
    {
        // 돈 감소하는 로직 구현 필요
        for(int i=0; i<DRAW_MAX_COUNT; i++)
        {
            selectedFurnitures[i] = OneDraw();
        }
        furnitureSubResult.SetFurnitures(selectedFurnitures);

        // 누렁이 망치 두드리는 화면 출력
        StartCoroutine(ShowMakeFurnitureUI());

        if(fiveDrawUI.activeSelf)
            fiveDrawUI.SetActive(false);
    }

    // 누렁이 망치질 시작   
    IEnumerator ShowMakeFurnitureUI()                                   
    {
        if(!makeFurnitureUI.activeSelf)
            makeFurnitureUI.SetActive(true);

        yield return new WaitForSeconds(DRAW_HAMMER_TIME);
        ShowSubResultUI();
    }

    // 뽑기 결과창
    void ShowSubResultUI()
    {
        // 결과창 활성화
        resultUI.transform.localScale = Vector3.one;

        if (makeFurnitureUI.activeSelf)
            makeFurnitureUI.SetActive(false);
    }
}
