using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NurungUI : MonoBehaviour
{
    [Header("# Draw UI Info")]
    [SerializeField] GameObject drawPanel;

    [Header("# Nurung UI Info")]
    [SerializeField] TMP_Text titleText;
    [SerializeField] TMP_Text dewText;
    [SerializeField] Transform contentPanel;
    [SerializeField] ScrollRect scrollRect;
    [Space(10)]
    [SerializeField] GameObject oneDrawUI;
    [SerializeField] GameObject fiveDrawUI;
    [SerializeField] GameObject makeFurnitureUI;

    [Header("# Furniture Result UI")]
    [SerializeField] GameObject resultUI;
    [SerializeField] Text furnitureNameText;
    [SerializeField] Text furnitureFlavorText;
    [SerializeField] Image furnitureImage;

    private DrawManager.Furniture[] furnitures;
    private DrawManager.Furniture selectedFurniture;
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
        titleText.text = _title;
        dewText.text = "가루 " + _dew;
    }

    public void OnNurungBtnHandler()                                // Nurung UI 켜는 버튼
    {
        if(!this.gameObject.activeSelf) 
            this.gameObject.SetActive(true);
    }

    public void InitFurnitueList(DrawManager.Furniture[] _furnitures)      // 아이템 리스트 초기화
    {
        furnitures = _furnitures;
        Debug.Log(furnitures[0].name);
        Debug.Log(furnitures.Length);
/*        for (int i=0; i<_furnitures.Length; i++)
        {
            _furnitures[i].transform.SetParent(contentPanel, false);
            _furnitures[i].transform.localScale = Vector3.one;
        }*/
    }


    public void OnPrevBtnHandler()                                  // 이전 버튼 함수
    {
        if (!drawPanel.activeSelf)
            drawPanel.SetActive(true);

        if(this.gameObject.activeSelf)
            this.gameObject.SetActive(false);
    }

    public void OnSubOneDrawBtnHandler()                               // 1회 뽑기
    {
        if(!oneDrawUI.activeSelf)
            oneDrawUI.SetActive(true);
    }

    public void OnSubOneDrawCloseBtnHandler()                          // 1회 뽑기 닫기
    {
        if(oneDrawUI.activeSelf)
            oneDrawUI.SetActive(false);
    }

    public void OnMainOneDrawBtnHandler()                               // 1회 뽑기 시작
    {
        // 돈 감소하는 로직 구현 필요
        selectedFurniture = OneDraw();
        StartCoroutine(ShowMakeFurnitureUI());

        if (oneDrawUI.activeSelf)
            oneDrawUI.SetActive(false);
    }

    DrawManager.Furniture OneDraw()
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
                return furnitures[i];
        }

        return new DrawManager.Furniture();
    }

    public void OnSubFiveDrawBtnHandler()                              // 5회 뽑기
    {
        if(!fiveDrawUI.activeSelf)
            fiveDrawUI.SetActive(true);
    }

    public void OnSubFiveDrawCloseBtnHandler()                         // 5회 뽑기 닫기
    {
        if(fiveDrawUI.activeSelf)
            fiveDrawUI.SetActive(false);
    }

    public void OnMainFiveDrawBtnHandler()                              // 5회 뽑기 시작
    {
        // 돈 감소하는 로직 구현 필요
        StartCoroutine(ShowMakeFurnitureUI());

        if(fiveDrawUI.activeSelf)
            fiveDrawUI.SetActive(false);
    }

    IEnumerator ShowMakeFurnitureUI()
    {
        if(!makeFurnitureUI.activeSelf)
            makeFurnitureUI.SetActive(true);

        yield return new WaitForSeconds(5.0f);
        ShowSubResultUI();
    }

    void ShowSubResultUI()
    {
        furnitureNameText.text = selectedFurniture.name;
        furnitureFlavorText.text = selectedFurniture.flavorText;

        if (!resultUI.activeSelf)
            resultUI.SetActive(true);

        if (makeFurnitureUI.activeSelf)
            makeFurnitureUI.SetActive(false);
    }
}
