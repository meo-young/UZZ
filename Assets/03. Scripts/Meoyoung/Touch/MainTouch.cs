using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private bool isDragging = false;
    private GameObject dragTarget = null;
    private Camera mainCamera;
    private Vector3 dragOffset;

    private void Awake()
    {
        gardenUI = FindFirstObjectByType<GardenUI>();
        mainCamera = Camera.main;
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
                        case "DragItem":
                            if(firstTouchedObject.GetComponent<DragItem>().isLocekd)
                                break;
                            isDragging = true;
                            dragTarget = firstTouchedObject;
                            dragOffset = dragTarget.transform.position - touchPosition;
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
                    Vector3 newPosition = touchPosition + dragOffset;
                    dragTarget.transform.position = new Vector3(newPosition.x, newPosition.y, dragTarget.transform.position.z);
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

                #endregion
                #region Touch End
                if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    if(isDragging)
                    {
                        isDragging = false;
                        dragTarget = null;
                        dragOffset = Vector3.zero;
                        return;
                    }
                    InitFlowerTouch();
                }
                #endregion
            }
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