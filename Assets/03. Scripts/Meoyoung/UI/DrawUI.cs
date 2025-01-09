using System.Collections.Generic;
using UnityEngine;

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
        
        if(noTouchPanel.activeSelf)
            noTouchPanel.SetActive(false);
    }

    public void OnDrawBtnHandler()                              // 뽑기 버튼 클릭
    {
        if(!this.gameObject.activeSelf)
            this.gameObject.SetActive(true);

        noTouchPanel.SetActive(true);
    }
    public void OnPrevBtnHandler()                              // 이전 버튼 클릭
    {
        SoundManager.instance.PlaySFX(SFX.Diary.BUTTON);

        if (this.gameObject.activeSelf)
            this.gameObject.SetActive(false);

        noTouchPanel.SetActive(false);
    }

    public void OnContentBtnHandler(int _index)                 // 테마 클릭 후 뽑기 탭으로 넘어감
    {
        nurungUI.InitTextInfo(
            _title: drawManager.drawdatas[_index].title,
            _dew: MainManager.instance.gameInfo.dew.ToString()
            );
        nurungUI.InitFurnitueList(GetFurnitureList(_index));
        nurungUI.OnNurungBtnHandler();
        if (this.gameObject.activeSelf)
            this.gameObject.SetActive(false);
    }

    DrawManager.Furniture[] GetFurnitureList(int _index)               // 선택한 테마의 가구 리스트를 반환
    {
        DrawManager.Furniture[] furniture = new DrawManager.Furniture[drawManager.drawdatas[_index].length];
        for(int i=0; i<furniture.Length; i++)
            furniture[i] = drawManager.drawdatas[_index].furnitures[i];
        return furniture;
    }
}
