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


    public float holdTime = 10.0f; // Ŭ���� �ϱ� ���� �ʿ��� �ð�
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
    [Header("ȣ���� ����Ʈ")]
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

        if (countTimer <= 0 && !isStart) //���۽�
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
        // ����� ��ġ �Է� ����
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
                      
                        //timer = 0f; // Ÿ�̸� �ʱ�ȭ
                    }
                }
            }
            else if (touch.phase == TouchPhase.Moved && isHolding)
            {
                float moveDistance = Vector2.Distance(touch.position, initialTouchPosition);
                if (moveDistance >= requiredMoveDistance)
                {
                    moveTimer = 0f; // �̵��� �����Ǹ� Ÿ�̸� �ʱ�ȭ
                  
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

        //��ġ����
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;

            // ��ġ�� ���۵� ���
            if (touch.phase == TouchPhase.Began)
            {
                isDragging = true;
            }

            // ��ġ�� �����̴� ���� ��� (�巡�� ��)
            if (touch.phase == TouchPhase.Moved && isDragging)
            {
                // ��ġ�� �̵��ϴ� ���ȿ��� Raycast�� ����Ͽ� Ȯ��
                if (Physics.Raycast(ray, out hit))
                {
                    // ���� ��ġ�� ������Ʈ�� SpiritManager�� ������ �ְ� id�� 201���� Ȯ��
                    SpiritManager currentSpiritManager = hit.transform.GetComponent<SpiritManager>();
                    if (currentSpiritManager != null && currentSpiritManager.id == 201)
                    {
                        tiger_minigame = true;  // ������ id�� 201�� ������Ʈ ���� ������ true
                        Debug.Log("Dragging on SpiritManager with ID 201, tiger_minigame is true");
                    }
                    else
                    {
                        tiger_minigame = false;  // �ٸ� ������Ʈ�� id�� 201�� �ƴϸ� false�� ����
                        Debug.Log("Moved away or SpiritManager ID is not 201, tiger_minigame is false");
                    }
                }
                else
                {
                    tiger_minigame = false;  // ��ġ�� �ٸ� ���� ������ false�� ����
                    Debug.Log("No object hit, tiger_minigame is false");
                }
            }

            // ��ġ�� ������ ��
            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isDragging = false;
                tiger_minigame = false;  // ��ġ�� ������ false�� ����
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
