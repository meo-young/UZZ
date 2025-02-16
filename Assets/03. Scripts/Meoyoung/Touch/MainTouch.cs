using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constant;

public class MainTouch : MonoBehaviour
{
    private FlowerManager flowerManager;
    private Animator flowerAnim;
    private PresentManager presentManager;
    private int dustTouchCounter;
    private PureController pc;
    private GameObject firstTouchedObject;
    private GameObject moveTouchedObject;

    private bool flowerTouched;
    private bool bCanFlowerAcquired;
    private bool bBigDustTouched;

    // 정원사의 집
    private GardenUI gardenUI;
    private GardenPlacement gardenPlacement;
    private bool isDragging = false;
    private GameObject dragTarget = null;
    private Camera mainCamera;
    private Vector3 dragOffset;
    private RectTransform dragBoundary;
    private Vector3 minBound;
    private Vector3 maxBound;

    private bool isShowerSwiping = false;
    private float showerSwipeTimer = 0f;

    private void Awake()
    {
        gardenUI = FindFirstObjectByType<GardenUI>();
        gardenPlacement = FindFirstObjectByType<GardenPlacement>();
        mainCamera = Camera.main;
        dragBoundary = gardenPlacement.GetDragBoundary();
    }


    private void Start()
    {
        flowerManager = MainManager.instance.flowerManager;
        pc = MainManager.instance.pureController;
        presentManager = MainManager.instance.presentManager;
        flowerTouched = false;
        bCanFlowerAcquired = false;
        bBigDustTouched = false;
        dustTouchCounter = 0;

        Vector3[] corners = new Vector3[4];
        dragBoundary.GetWorldCorners(corners);

        minBound = mainCamera.WorldToViewportPoint(corners[0]);
        maxBound = mainCamera.WorldToViewportPoint(corners[2]);
    }
    private void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            Vector3 effectPos = new(touchPosition.x, touchPosition.y, touchPosition.z + 10);
            #region Touch Begin
            if (touch.phase == TouchPhase.Began)
            {
                Physics.Raycast(touchPosition, transform.forward, out RaycastHit hit, 100f);
                if (hit.collider != null)
                {
                    firstTouchedObject = hit.transform.gameObject;
                    Debug.Log(firstTouchedObject.gameObject.tag);

                    switch (firstTouchedObject.tag)
                    {
                        case "PureShower":
                            isShowerSwiping = true;
                            showerSwipeTimer = 0f;
                            break;
                        case "DragItem":
                            var dragItem = firstTouchedObject.GetComponent<DragItem>();

                            gardenPlacement.SetCurrentFurniture(firstTouchedObject);
                            gardenUI.UpdatePlacementUI();

                            isDragging = true;
                            dragTarget = firstTouchedObject;
                            dragOffset = dragTarget.transform.position - touchPosition;
                            dragItem.ShowCorners();
                            break;
                        case "Pure_PresentGive":
                            SoundManager.instance.PlaySFX(SFX.PureSound.TOUCH);
                            presentManager.SetPresentImage();
                            break;
                        case "Pure_PresentReady":
                            SoundManager.instance.PlaySFX(SFX.PureSound.TOUCH);
                            pc.ChangeState(pc._presentGiveState);
                            break;
                        case "Pure_HelpWork":
                            SoundManager.instance.PlaySFX(SFX.Ambience.SOLVE);
                            pc.ChangeState(pc._workState);
                            break;
                        case "Pure_Idle":
                            if (flowerManager.isFlowerEvent)
                                break;
                            pc.preparationState = pc.CurrentState;
                            pc.ChangeState(pc._interactionState);
                            break;
                        case "Pure_Walk":
                            if (flowerManager.isFlowerEvent)
                                break;
                            pc.preparationState = pc.CurrentState;
                            pc.ChangeState(pc._interactionState);
                            break;
                        case "Flower":
                            SoundManager.instance.PlaySFX(SFX.Flower.TOUCH);
                            flowerTouched = true;
                            flowerAnim = firstTouchedObject.GetComponent<Animator>();
                            break;
                        case "Big_Dust":
                            bBigDustTouched = true;
                            dustTouchCounter++;
                            if (dustTouchCounter == 5)
                            {
                                flowerManager.GetBigEventReward();
                                dustTouchCounter = 0;
                                MainManager.instance.pure.SetActive(true);
                            }
                            break;
                        case "FlowerStepUp":
                            Destroy(firstTouchedObject);
                            flowerManager.FlowerStepUp();
                            break;
                        case "Garden":
                            gardenUI.OnGardenBtnHandler();
                            break;
                    }
                }

                if (bBigDustTouched)
                {
                    Instantiate(VFXManager.instance.flowerEventTouchVFX, effectPos, Quaternion.identity);
                    bBigDustTouched = false;
                }
                else
                    Instantiate(VFXManager.instance.defaultTouchVFX, effectPos, Quaternion.identity);

            }
            #endregion
            #region Touch Move
            if (touch.phase == TouchPhase.Moved)
            {
                if (isDragging && dragTarget != null)
                {
                    if (dragTarget.GetComponent<DragItem>().isLocekd)
                        return;

                    // 터치 위치를 RectTransform의 로컬 좌표로 변환
                    Vector2 localPoint;
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(
                        dragBoundary,
                        touch.position,
                        mainCamera,  // Screen Space - Camera 모드에서는 카메라 필요
                        out localPoint
                    );

                    // 로컬 좌표를 dragBoundary의 크기로 제한
                    Vector2 clampedPosition = new Vector2(
                        Mathf.Clamp(localPoint.x, dragBoundary.rect.xMin, dragBoundary.rect.xMax),
                        Mathf.Clamp(localPoint.y, dragBoundary.rect.yMin, dragBoundary.rect.yMax)
                    );

                    // 제한된 로컬 좌표를 월드 좌표로 변환
                    Vector3 worldPosition;
                    RectTransformUtility.ScreenPointToWorldPointInRectangle(
                        dragBoundary,
                        RectTransformUtility.WorldToScreenPoint(mainCamera, dragBoundary.TransformPoint(clampedPosition)),
                        mainCamera,
                        out worldPosition
                    );

                    dragTarget.transform.position = new Vector3(worldPosition.x, worldPosition.y, dragTarget.transform.position.z);
                    return;
                }


                Physics.Raycast(touchPosition, transform.forward, out RaycastHit hit, 100f);
                if (hit.collider != null)
                {
                    moveTouchedObject = hit.transform.gameObject;

                    switch (moveTouchedObject.tag)
                    {
                        case "Flower":
                            if (flowerTouched)
                            {
                                bCanFlowerAcquired = flowerManager.AcquireDewEffect();

                                if (flowerAnim != null)
                                    flowerAnim.timeSclae = 5f;
                            }
                            break;
                        case "PureShower":
                            if (isShowerSwiping)
                            {
                                showerSwipeTimer += Time.deltaTime;
                                if (showerSwipeTimer >= PURE_SHOWER_SWIPETIME)
                                {
                                    isShowerSwiping = false;
                                    showerSwipeTimer = 0f;
                                    MainManager.instance.gameInfo.showerFlag = false;
                                }
                            }
                            break;
                            
                        default:
                            InitFlowerTouch();
                            break;
                    }
                }
                else
                {
                    moveTouchedObject = null;
                    InitFlowerTouch();
                }
            }
            #endregion
            #region Touch End
            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                if (isDragging)
                {
                    isDragging = false;
                    dragTarget = null;
                    dragOffset = Vector3.zero;
                    return;
                }
                InitFlowerTouch();
                isShowerSwiping = false;
                showerSwipeTimer = 0f;
            }
            #endregion
        }

        void InitFlowerTouch()
        {
            if (!flowerTouched)
                return;

            flowerManager.counter = 0;

            if (!bCanFlowerAcquired)
                return;

            if (flowerAnim != null)
            {
                flowerAnim.timeSclae = 1f;
                flowerAnim = null;
            }
            flowerManager.MoveToTargetPos();
            bCanFlowerAcquired = false;
            flowerTouched = false;
        }
    }
}