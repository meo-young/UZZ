using UnityEngine;
using UnityEngine.UI;
using static Constant;

public class FurnitureMainResult : MonoBehaviour
{
    [SerializeField] GameObject oneDrawResultUI;
    [SerializeField] GameObject fiveDrawResultUI;

    private Image oneDrawResultImage;
    private Image[] fiveDrawResultImages;

    private void Awake()
    {
        // 1회 뽑기 결과 이미지 초기화
        Image[] oneImages = oneDrawResultUI.GetComponentsInChildren<Image>();
        oneDrawResultImage = oneImages[3];

        // 5회 뽑기 결과 이미지 초기화
        Image[] fiveImages = fiveDrawResultUI.GetComponentsInChildren<Image>();
        fiveDrawResultImages = new Image[DRAW_MAX_COUNT];
        for(int i = 0; i < DRAW_MAX_COUNT; i++)
        {
            fiveDrawResultImages[i] = fiveImages[i + 3];
        }
    }

    public void ShowOneDrawResult(Furniture _furniture)
    {
        oneDrawResultUI.SetActive(true);
        AddressableManager.instance.LoadSprite(_furniture.icon, oneDrawResultImage);
    }

    public void ShowFiveDrawResult(Furniture[] _furnitures)
    {
        fiveDrawResultUI.SetActive(true);
        for(int i = 0; i < DRAW_MAX_COUNT; i++)
            AddressableManager.instance.LoadSprite(_furnitures[i].icon, fiveDrawResultImages[i]);
    }

    public void OnConfirmBtnHandler()
    {
        SoundManager.instance.PlaySFX(SFX.Diary.BUTTON);
        oneDrawResultUI.SetActive(false);
        fiveDrawResultUI.SetActive(false);
    }
}
