using UnityEngine;

[System.Serializable]
public class PresentInfo
{
    public bool presentFlag;
    public float presentTimer;
}


public class PresentManager : MonoBehaviour
{
    public static PresentManager instance;
    [Tooltip("선물 UI")]
    [SerializeField] GameObject presentUI;
    [Tooltip("선물확률"), Range(0, 100)]
    [SerializeField] int presentProbabilty = 5;
    [Tooltip("선물확률 재활성화 간격(Second 단위)")]
    [SerializeField] float presentInterval = 120f;
    [Tooltip("선물 주기 후 애니메이션 출력까지 대기시간")]
    public float presentAfterWaitSeconds = 5;

    public PresentInfo presentInfo;
    private PureController pc;
    private bool firstCheck;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        if (presentUI == null)
            presentUI = GameObject.FindWithTag("PresentPanel");

        if (presentUI.activeSelf)
            presentUI.SetActive(false);

        firstCheck = false;
    }

    private void Start()
    {
        pc = MainManager.instance.pureController;

        if(presentInfo.presentTimer < presentInterval)
            pc.ChangeState(pc._walkState);

    }
    private void Update()
    {
        // 선물을 받은지 2분이 경과하거나, 처음 받는 상태인 경우 선물확률 체크
        // 선물확률에 걸리면 푸르는 선물 준비상태로 전이

        if (!presentInfo.presentFlag && presentInfo.presentTimer == 0)
            CheckProbabilty(presentProbabilty);
        else
            CheckPresentReactiveTime(presentInterval);
    }

    #region PresentFunction
    void CheckProbabilty(int probability)
    {
        if (MainManager.instance.gameInfo.cycleFlag)
            return;

        if (!pc.CheckPureAvailable())
            return;

        int randomNumber = Random.Range(0, 100);
        if (randomNumber < probability)
            pc.ChangeState(pc._presentReadyState);
        else
            pc.ChangeState(pc._walkState);


        MainManager.instance.gameInfo.cycleFlag = true;
        presentInfo.presentFlag = true;
    }

    void CheckPresentReactiveTime(float interval)
    {
        presentInfo.presentTimer += Time.deltaTime;

        if (presentInfo.presentTimer > interval)
        {
            presentInfo.presentTimer = 0f;
            presentInfo.presentFlag = false;
        }
    }

    public void ShowPresentUI()
    {
        SoundManager.instance.PlaySFX(SFX.Ambience.TEXT);
        presentUI.SetActive(true);
    }
    #endregion
}
