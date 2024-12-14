using UnityEngine;
using UnityEngine.EventSystems;


public class TouchManager : MonoBehaviour
{
    private bool canTouch;
    [SerializeField] private GameObject[] ui_untouchable_Objects;

    [Header("이렇게 해도 되고, 다음 구현 떄 고민해 봐도 되고")]
    //[SerializeField] private AlbumDiaryManager albumDiaryManager;
    //[SerializeField] private AlbumManager albumManager;
    //[SerializeField] private AudioManager audioManager;
    //[SerializeField] private DiaryManager diaryManager;
    //[SerializeField] private ShopManager shopManager;
    //[SerializeField] private SystemManager systemManager;
    //[SerializeField] private TestManager testManager;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private MQ_dialogSystem mQ_DialogSystem;


    [Header("근데 얘는 무조건 테스트")]
    [SerializeField] private GameObject drop_1;
    [SerializeField] private GameObject drop_2;
    private bool isAnimationPlaying;


    private int pointerID = 0;
    Animator anim;
    GameObject click_obj;
    public float requiredMoveDistance = 1f;
    public bool isHolding;
    public Vector2 initialTouchPosition;
    private float particleSpawnTimer = 0f;
    public float particleSpawnInterval = 0.5f;
    public GameObject flowerDragParticlePrefab;
    public GameObject bubbleTouchParticlePrefab;
    public float moveTimer = 0f;
    public float resetTime = 0.5f;

    public ItemAcquireFx petalGoodsPrefabItem;
    public ItemAcquireFx dewGoodsPrefabItem;
    public Transform target;

    public GameObject PrueTransform;
    public GameObject touchPoint;
    public GameObject UI_Flower;
    void Start()
    {
        canTouch = true;

#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
        pointerID = -1;
#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
        pointerID = 0;
#endif

    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < ui_untouchable_Objects.Length; i++)
        {
            if (ui_untouchable_Objects[i].active == true) { canTouch = false; cameraController.setCameraRestriction(true); break; }
            else { canTouch = true; cameraController.setCameraRestriction(false); }
        }



        //마우스 클릭 시
        if (Input.touchCount == 1 && canTouch == true)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began && EventSystem.current.IsPointerOverGameObject(pointerID) == false)
            {
                //마우스로 클릭한 좌표 값을 가져오기
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                Vector3 dir = new Vector3(0, 0, 1);

                //해당 좌표에 있는 오브젝트 찾기
                RaycastHit hit;
                Physics.Raycast(pos, transform.forward, out hit, 100f);

                //Hit한 오브젝트의 Collider에 접촉
                if (hit.collider != null)
                {

                    click_obj = hit.transform.gameObject;

                    if (click_obj.tag == "Gardener")
                    {
                        //click_obj.GetComponent<Mediation>().connectGardener();

                        //setTouch(false);
                    }
                    else if (click_obj.tag == "Spirit")
                    {
                        click_obj.GetComponent<SpiritManager>().touchSpirit();
                        //setTouch(false);
                    }
                    else if (click_obj.tag == "Notice_rest")
                    {
                        click_obj.GetComponent<RestNoticeManager>().touchRestNotice();
                    }
                    else if (click_obj.tag == "Notice_work")
                    {
                        click_obj.GetComponent<WorkNoticeManager>().touchWorkNotice();
                    }
                    else if (click_obj.tag == "Notice_quest")
                    {
                        click_obj.GetComponent<QuestNoticeManager>().touchQuestNotice();
                    }
                    else if (click_obj.tag == "Mailbox")
                    {
                        click_obj.GetComponent<MailboxManager>().touchMailbox();
                    }
                    else if (click_obj.tag == "Outside")
                    {
                        Debug.Log("OutSide");
                        cameraController.goGarret_Outside();
                    }
                    else if (click_obj.tag == "Iro")
                    {
                        mQ_DialogSystem.start_DialogSystem(1);
                    }
                    else if (click_obj.tag == "Drop")
                    {

                        GameManager.Instance.dew += (int)click_obj.GetComponent<Dew>().dew;
                        click_obj.GetComponent<Dew>().dew = 0;
                        SpawnParticleAtPosition(touch.position, bubbleTouchParticlePrefab);

                        int randCount = Random.Range(1, 5);
                        for (int i = 0; i < randCount; ++i)
                        {
                            var itemFx = GameObject.Instantiate<ItemAcquireFx>(dewGoodsPrefabItem, this.transform);
                            //itemFx.Explosion(pos, target.position, 1f);
                        }

                    }
                    else if (click_obj.tag == "Flower")
                    {
                        // 드래그가 시작됨
                        isHolding = true;
                        initialTouchPosition = touch.position;

                        // 파티클 생성 타이머 초기화
                        particleSpawnTimer = 0f;
                    }
                    else if (click_obj.tag == "Floor")
                    {


                        // 스크린 좌표를 월드 좌표로 변환


                        // 오브젝트를 해당 위치로 이동
                        touchPoint.transform.position = pos;
                    }
                    else if (click_obj.tag == "Lv")
                    {

                        UI_Flower.GetComponent<LvUpFlower>().flower = click_obj.GetComponentInParent<Flower>();
                        UI_Flower.GetComponent<LvUpFlower>().LV = click_obj.transform;
                        UI_Flower.SetActive(true);
                        
                      
                    }

                }
                
                

            }
            else if (touch.phase == TouchPhase.Moved && isHolding)
            {
                // 드래그 중일 때 파티클 생성 타이머 업데이트
                Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                RaycastHit hit;
                if (Physics.Raycast(touchPos, transform.forward, out hit, 100f))
                {
                    if (hit.collider != null && hit.transform.CompareTag("Flower"))
                    {
                        // 파티클 생성 타이머 업데이트
                        particleSpawnTimer += Time.deltaTime;
                        if(click_obj.GetComponent<Flower>().flower > 1)
                        {
                             
                            GameManager.Instance.petal += (int)click_obj.GetComponent<Flower>().flower;
                            click_obj.GetComponent<Flower>().flower = 0;
                            int randCount = Random.Range(5, 20);
                            for (int i = 0; i < randCount; ++i)
                            {
                                var itemFx = GameObject.Instantiate<ItemAcquireFx>(petalGoodsPrefabItem, this.transform);
                                //itemFx.Explosion(touchPos, target.position, 1f);
                            }
                        }
                        
                        
                        anim = click_obj.GetComponent<Animator>();
                        if (anim != null)
                        {
                            // Animator가 있으면 speed를 사용하여 애니메이션 재생 속도 변경
                            anim.timeSclae = 5f;
                        }
                        else
                        {
                            Debug.Log("Animator 컴포넌트가 없습니다.");
                        }
                        if (particleSpawnTimer >= particleSpawnInterval)
                        {
                            // 파티클 생성
                            SpawnParticleAtPosition(touch.position, flowerDragParticlePrefab);
                            particleSpawnTimer = 0f;  // 타이머 초기화
                        }
                    }
                }
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                if (anim != null)
                {
                    // Animator가 있으면 speed를 사용하여 애니메이션 재생 속도 변경
                    anim.timeSclae = 1f;
                }
                isHolding = false;
            }
        }

        PrueTransform.transform.position = Vector2.MoveTowards(PrueTransform.transform.position, touchPoint.transform.position, 10f * Time.deltaTime);;

    }


    void SpawnParticleAtPosition(Vector3 screenPosition,GameObject obj)
    {
        // 화면 좌표를 월드 좌표로 변환
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, 10f));

        // 파티클 프리팹을 해당 위치에 생성
       
        Instantiate(obj, worldPosition, Quaternion.identity);
        
    }




    public void setTouch(bool val)
    {
        Debug.Log("어딘지는 모르겠지만 호출됨 " + val);
        canTouch = val;
    }

    //private IEnumerator dropAnimation()
    //{
    //    isAnimationPlaying = true;
    //    drop_1.SetActive(false);
    //    drop_2.SetActive(true);
    //    yield return new WaitForSeconds(1.667f);

    //    drop_2.SetActive(false);
    //    drop_1.SetActive(true);
    //    isAnimationPlaying = false;
    //}

}