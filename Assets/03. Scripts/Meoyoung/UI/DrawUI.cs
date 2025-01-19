using System.Collections.Generic;
using UnityEngine;
using System.Linq;  // LINQ 사용을 위해 추가

public class DrawUI : MonoBehaviour
{
    [Header("# Nurung UI Info")]
    [SerializeField] NurungUI nurungUI;
    [SerializeField] GameObject itemPrefab;

    [Header("터치 방지 패널")]
    [SerializeField] GameObject noTouchPanel;
     

    private DrawManager drawManager;

    private void Awake()
    {
        drawManager = FindFirstObjectByType<DrawManager>();

        transform.localScale = Vector3.zero;
        
        if(noTouchPanel.activeSelf)
            noTouchPanel.SetActive(false);
    }

    public void OnDrawBtnHandler()                              // 뽑기 버튼 클릭
    {
        if(transform.localScale == Vector3.zero)
            transform.localScale = Vector3.one;

        noTouchPanel.SetActive(true);
    }
    public void OnPrevBtnHandler()                              // 이전 버튼 클릭
    {
        SoundManager.instance.PlaySFX(SFX.Diary.BUTTON);

        if (transform.localScale == Vector3.one)
            transform.localScale = Vector3.zero;

        noTouchPanel.SetActive(false);
    }

    public void OnContentBtnHandler(int _index)                 // 테마 클릭 후 뽑기 탭으로 넘어감
    {
        nurungUI.InitTextInfo(
            _title: drawManager.drawdatas[_index].title,
            _dew: MainManager.instance.gameInfo.dew.ToString()
            );
        nurungUI.InitFurnitueList(GetFurnitureList(_index));
        nurungUI.OnNurungBtnHandler(_index);


        if (transform.localScale == Vector3.one)
            transform.localScale = Vector3.zero;
    }

    Furniture[] GetFurnitureList(int _index)               // 선택한 테마의 가구 리스트를 반환
    {
        Furniture[] furniture = new Furniture[drawManager.drawdatas[_index].furnitures.Count];
        for(int i=0; i<furniture.Length; i++)
            furniture[i] = drawManager.drawdatas[_index].furnitures[i];
        
        // rank로 먼저 정렬하고, rank가 같으면 index로 정렬
        return furniture.OrderBy(f => f.rank)
                       .ThenBy(f => f.index)
                       .ToArray();
    }
}
