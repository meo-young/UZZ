using UnityEngine;
using UnityEngine.UI;
public class GardenUI : MonoBehaviour
{
    // 오른쪽 버튼 패널
    private RectTransform RightButtonPanel;

    // 숨기기 버튼 이미지
    private Image HideButton;


    private void Start() {
        RightButtonPanel = Variable.instance.Init<RectTransform>(transform, nameof(RightButtonPanel), RightButtonPanel);
        HideButton = Variable.instance.Init<Image>(transform, nameof(HideButton), HideButton);

        transform.localScale = Vector3.zero;
    }

    // 정원사의 집 UI 켜기
    public void OnGardenBtnHandler()
    {
        transform.localScale = Vector3.one;
    }

    // 정원사의 집 UI 끄기
    public void OnGardenBackBtnHandler()
    {
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

}
