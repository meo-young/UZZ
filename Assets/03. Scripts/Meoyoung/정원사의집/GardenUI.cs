using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GardenUI : MonoBehaviour
{
    
    private TMP_Text FurnitureCountText;        // 가구 수 텍스트
    private RectTransform RightButtonPanel;     // 오른쪽 버튼 패널
    private Image HideButton;                   // 숨기기 버튼 이미지


    private void Start() {
        RightButtonPanel = Variable.instance.Init<RectTransform>(transform, nameof(RightButtonPanel), RightButtonPanel);
        HideButton = Variable.instance.Init<Image>(transform, nameof(HideButton), HideButton);
        FurnitureCountText = Variable.instance.Init<TMP_Text>(transform, nameof(FurnitureCountText), FurnitureCountText);

        transform.localScale = Vector3.zero;
    }

    // 정원사의 집 UI 켜기
    public void OnGardenBtnHandler()
    {
        UpdateGardenData();

        transform.localScale = Vector3.one;
    }

    // 정원사의 집 UI 끄기
    public void OnGardenBackBtnHandler()
    {
        SoundManager.instance.PlaySFX(SFX.Diary.BUTTON);
        transform.localScale = Vector3.zero;
    }

    // 뽑기, 메인 버튼 활성화
    public void OnVisibleBtnHandler()
    {
        if(RightButtonPanel.localScale == Vector3.zero)
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


    private void UpdateGardenData()
    {
        // 가구 수 업데이트
        FurnitureCountText.text = DrawManager.instance.GetFurnitueCount().ToString() + "개";
    }
}
