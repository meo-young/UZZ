using UnityEngine;

public class PureController : MonoBehaviour
{
    public static PureController instance;

    public BoxCollider2D walkBound;

    [Header("# State Position Info")]
    public Transform basePos;
    public Transform[] fieldWorkPos;
    public Transform[] fieldWorkTextPos;


    [HideInInspector] public IControllerState preparationState;
    [HideInInspector] public bool isLevelUp;
    [HideInInspector] public float cycleCheckTimer; // 한 사이클이 돌았는지 체크하는 변수
    [HideInInspector] public float workTimer; // 작업시간 체크하는 변수
    [HideInInspector] public bool autoWorkCheckFlag;
    [HideInInspector] public bool helpEventCheckFlag; // 도움작업을 받았는지 체크하는 변수
    [HideInInspector] public bool flowerEventCheckFlag; // 꽃 이벤트를 확인했는지 체크하는 변수
    [HideInInspector] public bool spiritEventCheckFlag; // 정령의 힘 이벤트가 걸렸는지 확인하는 변수
    [HideInInspector] public bool spiritAdvertisementFlag; // 광고를 본 경우 true
    [HideInInspector] public FieldWork fieldWorkState; //푸르의 현재 작업 상태를 나타내기위한 변수. FieldWorkUI의 OnClick핸들러에서 접근함.

    [HideInInspector] public PureStat pureStat;
    [HideInInspector] public PureAnimationSet pureAnimationSet;
    [HideInInspector] public PresentManager presentManager;
    [HideInInspector] public FieldWorkManager fieldWorkManager;
    [HideInInspector] public FlowerManager flowerManager;
    [HideInInspector] public SpeechBubbleSet speechBubbleSet;
    [HideInInspector] public VFXManager vfxManager;
    [HideInInspector] public PureInteractionText interactionText;
    [HideInInspector] public AutoText autoText;

    public IControllerState CurrentState
    {
        get; private set;
    }

    public IControllerState _idleState, _baseState, _interactionState, _walkState, _presentReadyState, _presentGiveState, _workState, _workHelpState, _showerState;

    private void Awake()
    {
        if(instance == null)
            instance = this;

        pureStat = MainManager.instance.pureStat;
        pureAnimationSet = MainManager.instance.pureAnimationSet;
        fieldWorkManager = MainManager.instance.fieldWorkManager;
        speechBubbleSet = MainManager.instance.speechBubbleSet;
        vfxManager = MainManager.instance.vfxManager;
        presentManager = MainManager.instance.presentManager;
        flowerManager = MainManager.instance.flowerManager;
        interactionText = MainManager.instance.pureInteractionText;
        autoText = MainManager.instance.autoText;
    }

    private void Start()
    {
        #region Init State
        _idleState = gameObject.AddComponent<PureIdleState>();
        _baseState = gameObject.AddComponent<PureBaseState>();
        _interactionState = gameObject.AddComponent<PureInteractionState>();
        _walkState = gameObject.AddComponent<PureWalkState>();
        _presentReadyState = gameObject.AddComponent<PurePresentReadyState>();
        _presentGiveState = gameObject.AddComponent<PurePresentGiveState>();
        _workState = gameObject.AddComponent<PureWorkState>();
        _workHelpState = gameObject.AddComponent<PureWorkHelpState>();
        _showerState = gameObject.AddComponent<PureShowerState>();
        #endregion
        Initialize();

        ChangeState(_idleState);
    }

    public void ChangeState(IControllerState controllerState)
    {
        if(CurrentState != null)
        {
            CurrentState.OnStateExit();
        }

        CurrentState = controllerState;
        CurrentState.OnStateEnter(this);
    }

    private void Update()
    {
        UpdateState();   
    }

    private void UpdateState()
    {
        if (CurrentState != null)
            CurrentState.OnStateUpdate();
    }

    public bool CheckPureAvailable()
    {
        if (CurrentState == _idleState)
            return true;

        if (CurrentState == _walkState)
            return true;

        return false;
    }

    public void Initialize()
    {
        workTimer = 0;
        isLevelUp = false;
        autoWorkCheckFlag = false;
        helpEventCheckFlag = false;
        flowerEventCheckFlag = false;
        fieldWorkState = fieldWorkManager.noneWork;
        cycleCheckTimer = 0;
        preparationState = _idleState;
    }

    public Transform ReturnWorkPurePosition()
    {
        return fieldWorkPos[(int)fieldWorkState.type].transform;
    }

    public Transform GetWorkTextPosition()
    {
        return fieldWorkTextPos[(int)fieldWorkState.type].transform;
    }

}
