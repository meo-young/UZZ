using UnityEngine;
using static Constant;

public class AdvertisementUI : MonoBehaviour
{
    public static AdvertisementUI instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        transform.localScale = Vector3.zero;
    }

    // 광고 UI 표시
    public void Show()
    {
        transform.localScale = Vector3.one;
    }

    // 광고 UI 숨김
    public void Hide()
    {
        transform.localScale = Vector3.zero;
    }

    // 광고보기 버튼 이벤트 핸들러
    public void OnClickAdvertisement()
    {
        PureController.instance.spiritAdvertisementFlag = true;
        PureController.instance.ChangeState(PureController.instance._workState);
        GiveReward(true);
    }

    // 보상받기 버튼 이벤트 핸들러
    public void OnClickSkip()
    {
        PureController.instance.ChangeState(PureController.instance._workState);
        GiveReward(false);
    }

    public void GiveReward(bool isAdvertisement)
    {
        if(isAdvertisement)
        {
            PureStat.instance.GetLikeability(SPIRIT_EVENT_LIKEABILITY_REWARD * 2);
            FlowerManager.instance.GetFlowerExp(SPIRIT_EVENT_GROWTH_REWARD * 2);
            MainManager.instance.dewUI.Count(SPIRIT_EVENT_DEW_REWARD * 2);
            // 유리 구슬 보상 획득 해야함함
        }
        else
        {
            PureStat.instance.GetLikeability(SPIRIT_EVENT_LIKEABILITY_REWARD);
            FlowerManager.instance.GetFlowerExp(SPIRIT_EVENT_GROWTH_REWARD);
            MainManager.instance.dewUI.Count(SPIRIT_EVENT_DEW_REWARD);
        }
    }

}
