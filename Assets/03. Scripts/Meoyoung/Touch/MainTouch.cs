using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTouch : MonoBehaviour
{
    private FlowerManager flowerManager;
    private bool flowerTouched;
    private Animator flowerAnim;
    private int dustTouchCounter;

    private PureController pc;

    private GameObject firstTouchedObject;
    private GameObject moveTouchedObject;


    private void Start()
    {
        flowerManager = MainManager.instance.flowerManager;
        pc = MainManager.instance.pureController;
        flowerTouched = false;
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
                        case "Pure_PresentGive":
                            SoundManager.instance.PlaySFX(SFX.PureSound.TOUCH);
                            pc.presentManager.ShowPresentUI();
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
                            dustTouchCounter++;
                            if (dustTouchCounter == 5)
                            {
                                flowerManager.GetBigEventReward();
                                dustTouchCounter = 0;
                            }
                            break;
                        case "FlowerStepUp":
                            Destroy(firstTouchedObject);
                            flowerManager.FlowerStepUp();
                            break;
                    }
                }

                Instantiate(VFXManager.instance.defaultTouchVFX, effectPos, Quaternion.identity);

            }
            #endregion
            #region Touch Move
            if (touch.phase == TouchPhase.Moved)
            {
                Physics.Raycast(touchPosition, transform.forward, out RaycastHit hit, 100f);
                if (hit.collider != null)
                {
                    moveTouchedObject = hit.transform.gameObject;

                    switch (moveTouchedObject.tag)
                    {
                        case "Flower":
                            if (flowerTouched)
                            {
                                if (!flowerManager.AcquireDewEffect())
                                {
                                    InitFlowerTouch();
                                    break;
                                }

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
            }
            #endregion
            #region Touch End
            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                InitFlowerTouch();
            }
            #endregion
        }
    }

    void InitFlowerTouch()
    {
        if (!flowerTouched)
            return;

        flowerTouched = false;
        if(flowerAnim != null)
        {
            flowerAnim.timeSclae = 1f;
            flowerAnim = null;
        }
        flowerManager.MoveToTargetPos();
    }
}
