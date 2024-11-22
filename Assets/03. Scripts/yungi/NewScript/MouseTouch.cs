using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum MiniGame
{
    Ogre,
    tiger,
    Meteor,
    Default

}

public class MouseTouch : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject startPrefab;
    public GameObject tailPrefab;
    public GameObject touchPrefab;
    public bool tiger_minigame;
    public MiniGame miniGameT;
    public bool isTouched;
    float starSpawnsTime;
    float tailSpawnsTime;
    public float sDefaultTIme = 0.05f;
    public float tDefaultTIme = 0.05f;

    private bool isDragging;

    [Header("[Click Particle]")]
    public ParticleSystem particleEffect;

    [Header("[오그래]")]
    public GameObject VFX_ogre_succes;
    public GameObject VFX_ogre_fail;

    [Header("생떽쥐]")]
    [SerializeField] private GameObject VFX_Meteorite_touch;



    public GraphicRaycaster raycaster; // Canvas에 추가한 GraphicRaycaster
    public EventSystem eventSystem; // EventSystem 객체를 할당해야 합니다.


    private void Start()
    {

    }
    void Update()
    {



        Touch();




        //if (Input.GetMouseButtonDown(0))
        //{



        //    // 마우스 클릭 위치 얻기
        //    Vector2 mouseP = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        //        // 히트된 위치에 파티클 재생
        //        ParticleSystem instantiatedParticle = Instantiate(particleEffect, mouseP, Quaternion.identity);
        //        instantiatedParticle.Play();

        //}
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

    public void Touch()
    {
        switch (miniGameT)
        {
            case MiniGame.tiger:
                {
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
                            touchPrefab.SetActive(false);
                            Debug.Log("Touch ended or canceled, tiger_minigame is false");
                        }
                    }
                    if (!tiger_minigame)
                        return;
                    starSpawnsTime += Time.deltaTime;
                    tailSpawnsTime += Time.deltaTime;

                    if (Input.GetMouseButton(0))
                    {
                        if (starSpawnsTime >= sDefaultTIme)
                        {
                            StartCreat();
                            starSpawnsTime = 0;
                        }
                    }

                    if (Input.GetMouseButton(0))
                    {
                        if (tailSpawnsTime >= tDefaultTIme)
                        {
                            TailCreat();
                            tailSpawnsTime = 0;
                        }
                    }

                    if (Input.GetMouseButtonDown(0) && !isTouched)
                    {
                        isTouched = true;
                        touchPrefab.SetActive(true);
                    }
                    if (Input.GetMouseButtonUp(0) && isTouched)
                    {
                        isTouched = false;
                        touchPrefab.SetActive(false);
                    }
                    break;
                }

            case MiniGame.Ogre:
                {


                    if (Input.touchCount > 0)
                    {
                        Touch touch = Input.GetTouch(0);
                        
                        // 터치가 시작된 순간에만 실행
                        if (touch.phase == TouchPhase.Began)
                        {
                            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

                            // 2D Raycast로 터치한 오브젝트 확인
                            RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);

                            // 터치한 오브젝트가 있을 때
                            if (hit.collider != null)
                            {
                                // 오브젝트의 태그가 "Fish"일 경우
                                if (hit.collider.CompareTag("Fish"))
                                {
                                    // 이펙트를 터치 위치에 소환
                                    Instantiate(VFX_ogre_succes, touchPosition, Quaternion.identity);
                                }
                            }
                            else
                            {
                                Instantiate(VFX_ogre_fail, touchPosition, Quaternion.identity);
                            }
                            
                        }
                    }
                    break;

                }
            case MiniGame.Meteor:
                {
                    if (Input.touchCount > 0)
                    {
                        Touch touch = Input.GetTouch(0);

                        // 터치가 시작된 순간에만 실행
                        if (touch.phase == TouchPhase.Began)
                        {
                            // PointerEventData 생성
                            PointerEventData pointerEventData = new PointerEventData(eventSystem);
                            pointerEventData.position = touch.position;

                            // Raycast 결과를 담을 리스트
                            List<RaycastResult> results = new List<RaycastResult>();

                            // Raycast 실행
                            raycaster.Raycast(pointerEventData, results);

                            // 터치한 UI 요소가 있을 때
                            if (results.Count > 0)
                            {
                                // 터치한 첫 번째 UI 요소 확인
                                GameObject touchedObject = results[0].gameObject;

                                // 오브젝트의 이름 또는 특정 조건으로 비교
                                if (touchedObject.tag == "Meteor")
                                {
                                    
                                    // 이펙트를 터치 위치에 소환
                                    Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                                    Instantiate(VFX_Meteorite_touch, touchPosition, Quaternion.identity);
                                }
                            }
                            else
                            {
                                // 터치한 UI 요소가 없을 때의 처리
                            }
                        }
                    }
                    break;
                }
            case MiniGame.Default:
                {
                    if (Input.touchCount > 0)
                    {
                        Touch touch = Input.GetTouch(0);

                        // 터치가 시작된 순간에만 실행
                        if (touch.phase == TouchPhase.Began)
                        {
                            // PointerEventData 생성
                            PointerEventData pointerEventData = new PointerEventData(eventSystem);
                            pointerEventData.position = touch.position;

                            // Raycast 결과를 담을 리스트
                            List<RaycastResult> results = new List<RaycastResult>();

                            // Raycast 실행
                            raycaster.Raycast(pointerEventData, results);

                            // 터치한 UI 요소가 있을 때
                            if (results.Count > 0)
                            {
                                // 터치한 첫 번째 UI 요소 확인
                                GameObject touchedObject = results[0].gameObject;
                                Debug.Log(touchedObject);
                                // 오브젝트의 이름 또는 특정 조건으로 비교
                                if (touchedObject.tag == "Drop")
                                {

                                    // 이펙트를 터치 위치에 소환
                                    touchedObject.GetComponent<SkeletonAnimation>().timeScale = 13f;
                                    Debug.Log("드랍 터치");
                                }
                            }
                            else
                            {
                                // 터치한 UI 요소가 없을 때의 처리
                            }
                        }
                    }
                    break;
                }

        }
    }
   
}


