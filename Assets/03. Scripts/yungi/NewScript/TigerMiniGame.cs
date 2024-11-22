using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TigerMiniGame : MonoBehaviour
{


    public TMP_Text countDown;

    public float countTimer = 3;

    public bool isStart;


    public TMP_Text clearT;
    public TMP_Text failT;
    public int fail;


    public float holdTime = 10.0f; // 클리어 하기 위해 필요한 시간
    public float timer = 0f;
    public bool isHolding = false;

    public Slider slider;

    public float requiredMoveDistance = 1f;
    private Vector2 initialTouchPosition;
    private bool hasMoved = false;
    private float moveTimer = 0f;
    public float resetTime = 0.5f;

    [SerializeField] private GameObject rewardMessage;
    [SerializeField] private GameObject title;
    [SerializeField] private GameObject countDownG;
    [SerializeField] private GameObject question;
    [SerializeField] private GameObject correct;

    [SerializeField] private Inventory invenotry;
    [SerializeField] private SpiritManager spiritManager_ho;
    [SerializeField] private CameraController cameraController;
    [Header("호냥이 이펙트")]
    [SerializeField] private GameObject startPrefab;
    [SerializeField] private GameObject tailPrefab;
    [SerializeField] private GameObject touchPrefab;
    [SerializeField] private bool tiger_minigame;
    [SerializeField] private bool isTouched;
    float starSpawnsTime;
    float tailSpawnsTime;
    public float sDefaultTIme = 0.05f;
    public float tDefaultTIme = 0.05f;
    
    private bool isDragging;

    [SerializeField] private MouseTouch mouseTouch;
    private void OnEnable()
    {
        Init();
    }
    void Init()
    {
        spiritManager_ho.gameObject.GetComponent<Animator>()._AnimState = Animator.AnimState.Work;
        cameraController.GoHonyang();
        spiritManager_ho.CanTalk(false);
        mouseTouch.miniGameT = MiniGame.tiger;
        countTimer = 3;
        timer = 0;
        question.SetActive(true);
        slider.gameObject.SetActive(true);
        slider.value = timer;
        Camera.main.orthographicSize = 12;
        
      
        
    }
    
    private void Update()
    {
       



        //
        if (fail >= 1)
        {
            failT.gameObject.SetActive(true);
        }

        if (countTimer <= 0 && !isStart) //시작시
        {

            isStart = true;
            countDown.gameObject.SetActive(false);
           


        }
        else if(!isStart)
        {
            countDown.text = countTimer.ToString("F0");
            countTimer -= Time.deltaTime;
        }

        if (!isStart)
            return;
        // 모바일 터치 입력 감지
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.CompareTag("Spirit"))
                    {
                        isHolding = true;
                        initialTouchPosition = touch.position;
                      
                        //timer = 0f; // 타이머 초기화
                    }
                }
            }
            else if (touch.phase == TouchPhase.Moved && isHolding)
            {
                float moveDistance = Vector2.Distance(touch.position, initialTouchPosition);
                if (moveDistance >= requiredMoveDistance)
                {
                    moveTimer = 0f; // 이동이 감지되면 타이머 초기화
                  
                }
            }



            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isHolding = false;
                
            }
        }

        if (isHolding)
        {
            moveTimer += Time.deltaTime;
            spiritManager_ho.gameObject.GetComponent<Animator>().timeSclae = 2.5f;
            if (moveTimer < resetTime)
            {

                timer += Time.deltaTime;
                slider.value = timer / holdTime;
                if (timer >= holdTime)
                {

                    isStart = false;
                    clearT.gameObject.SetActive(true);

                    isHolding = false;
                    timer = 0f;
                    StartCoroutine(ShowRewardMessage());
                    question.SetActive(false);
                    slider.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            spiritManager_ho.gameObject.GetComponent<Animator>().timeSclae = 1f;
        }

        //터치관련
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;

            // 터치가 시작된 경우
            if (touch.phase == TouchPhase.Began)
            {
                isDragging = true;
            }

            // 터치가 움직이는 중일 경우 (드래그 중)
            if (touch.phase == TouchPhase.Moved && isDragging)
            {
                // 터치가 이동하는 동안에도 Raycast를 사용하여 확인
                if (Physics.Raycast(ray, out hit))
                {
                    // 현재 터치된 오브젝트가 SpiritManager를 가지고 있고 id가 201인지 확인
                    SpiritManager currentSpiritManager = hit.transform.GetComponent<SpiritManager>();
                    if (currentSpiritManager != null && currentSpiritManager.id == 201)
                    {
                        tiger_minigame = true;  // 여전히 id가 201인 오브젝트 위에 있으면 true
                        Debug.Log("Dragging on SpiritManager with ID 201, tiger_minigame is true");
                    }
                    else
                    {
                        tiger_minigame = false;  // 다른 오브젝트나 id가 201이 아니면 false로 변경
                        Debug.Log("Moved away or SpiritManager ID is not 201, tiger_minigame is false");
                    }
                }
                else
                {
                    tiger_minigame = false;  // 터치가 다른 곳에 있으면 false로 변경
                    Debug.Log("No object hit, tiger_minigame is false");
                }
            }

            // 터치가 끝났을 때
            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isDragging = false;
                tiger_minigame = false;  // 터치가 끝나면 false로 설정
                Debug.Log("Touch ended or canceled, tiger_minigame is false");
            }
        }



        
    }

    

    private IEnumerator ShowRewardMessage()
    {



        yield return new WaitForSeconds(1f);

        rewardMessage.SetActive(true);

        int a = Random.Range(0, 100);
        
        if(a<25)
        {
            for (int i = 0; i < ItemManager.instance.itemList.Length; i++)
            {
                if (ItemManager.instance.itemList[i].id == 16)
                {
                    rewardMessage.GetComponentInChildren<TMP_Text>().text = ItemManager.instance.itemList[i].itemName;
                    rewardMessage.GetComponentsInChildren<Image>()[1].sprite = ItemManager.instance.itemList[i].itemImage;
                    invenotry.AddItem(ItemManager.instance.itemList[i], 1);
                    break;
                }
            }
        }
        else if(a<50)
        {
            for (int i = 0; i < ItemManager.instance.itemList.Length; i++)
            {
                if (ItemManager.instance.itemList[i].id == 17)
                {
                    rewardMessage.GetComponentInChildren<TMP_Text>().text = ItemManager.instance.itemList[i].itemName;
                    rewardMessage.GetComponentsInChildren<Image>()[1].sprite = ItemManager.instance.itemList[i].itemImage;
                    invenotry.AddItem(ItemManager.instance.itemList[i], 1);
                    break;
                }
            }
        }
        else if(a<75)
        {
            for (int i = 0; i < ItemManager.instance.itemList.Length; i++)
            {
                if (ItemManager.instance.itemList[i].id == 18)
                {
                    rewardMessage.GetComponentInChildren<TMP_Text>().text = ItemManager.instance.itemList[i].itemName;
                    rewardMessage.GetComponentsInChildren<Image>()[1].sprite = ItemManager.instance.itemList[i].itemImage;
                    invenotry.AddItem(ItemManager.instance.itemList[i], 1);
                    break;
                }
            }
        }
        else if(a<100)
        {
            for (int i = 0; i < ItemManager.instance.itemList.Length; i++)
            {
                if (ItemManager.instance.itemList[i].id == 19)
                {
                    rewardMessage.GetComponentInChildren<TMP_Text>().text = ItemManager.instance.itemList[i].itemName;
                    rewardMessage.GetComponentsInChildren<Image>()[1].sprite = ItemManager.instance.itemList[i].itemImage;
                    invenotry.AddItem(ItemManager.instance.itemList[i], 1);
                    break;
                }
            }
        }

        yield return new WaitForSeconds(3f);

        rewardMessage.SetActive(false);
        gameObject.SetActive(false);

        Camera.main.orthographicSize = 18;

        mouseTouch.miniGameT = MiniGame.Default;
        spiritManager_ho.CanTalk(true);
        GameManager.Instance.OnOffUIStart(true);
        cameraController.goGarret();
        spiritManager_ho.gameObject.GetComponent<Animator>().timeSclae = 1f;
    }

    void StartCreat()
    {
        Vector3 mPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mPosition.z = 0;
        Instantiate(startPrefab, mPosition, Quaternion.identity);

    }
    void TailCreat()
    {
        Vector3 mPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mPosition.z = 0;
        Instantiate(tailPrefab, mPosition, Quaternion.identity);
    }
}
