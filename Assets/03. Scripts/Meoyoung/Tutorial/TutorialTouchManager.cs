using UnityEngine;

public class TutorialTouchManager : MonoBehaviour
{
    [SerializeField] GameObject defaultTouchVFX;
    [SerializeField] TutorialManager tutoManager;
    [SerializeField] TutoFlowerManager flowerManager;
    [SerializeField] GameObject flowerVFX;

    [Header("# Tutorial CG Image")]
    [SerializeField] GameObject tutoCG0;
    [SerializeField] GameObject tutoCG1;
    [SerializeField] GameObject tutoCG2;

    private GameObject firstTouchedObject;
    private GameObject moveTouchedObject;
    private bool flowerTouched;
    private Animator flowerAnim;
    private bool imageTouched;
    private float flowerCounter;

    private void Start()
    {
        flowerCounter = 0f;
        flowerTouched = false;
        imageTouched = false;
    }

    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            Vector3 effectPos = new(touchPosition.x, touchPosition.y, touchPosition.z + 10);
            if (touch.phase == TouchPhase.Began)
            {
                Debug.Log("Touched");
                switch (tutoManager.GetTouchType())
                {
                    case "Full":
                        tutoManager.OnNextDialogueHandler();
                        break;
                }
                Physics.Raycast(touchPosition, transform.forward, out RaycastHit hit, 100f);
                if (hit.collider != null)
                {
                    firstTouchedObject = hit.transform.gameObject;
                    Debug.Log(firstTouchedObject.gameObject.tag);

                    switch (firstTouchedObject.tag)
                    {
                        case "Pure_HelpWork":
                            firstTouchedObject.GetComponent<WateringHelp>().DeactiveObejct();
                            break;
                        case "Big_Dust":
                            firstTouchedObject.GetComponent<TutoBigDust>().BigDustNextHandler();
                            break;
                        case "Flower":
                            if (tutoManager.GetTextType() == "Flower2")
                            {
                                if(flowerVFX.activeSelf)
                                {
                                    flowerTouched = true;
                                    flowerAnim = firstTouchedObject.GetComponent<Animator>();
                                }
                            }
                            break;
                        case "CG0":
                            if (imageTouched)
                            {
                                tutoCG0.SetActive(false);
                                tutoCG1.SetActive(true);
                            }
                            imageTouched = true;
                            break;
                        case "CG1":
                            if (!tutoCG2.activeSelf)
                                tutoCG2.SetActive(true);
                            else
                            {
                                tutoCG1.SetActive(false);
                                tutoCG2.SetActive(false);
                                tutoManager.OnNextDialogueHandler();
                            }
                            break;
                    }
                }

                Instantiate(defaultTouchVFX, effectPos, Quaternion.identity);
            }

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
                                else
                                {
                                    if (flowerAnim != null)
                                        flowerAnim.timeSclae = 5f;

                                    flowerCounter += Time.deltaTime;
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
            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                InitFlowerTouch();
            }
        }

        void InitFlowerTouch()
        {
            if (!flowerTouched)
                return;

            flowerTouched = false;
            if (flowerAnim != null)
            {
                flowerAnim.timeSclae = 1f;
                flowerAnim = null;
            }

            if(flowerCounter > 1)
                flowerManager.MoveToTargetPos();

            flowerCounter = 0f;
        }
    }
}