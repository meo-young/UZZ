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

    [Header("[���׷�]")]
    public GameObject VFX_ogre_succes;
    public GameObject VFX_ogre_fail;

    [Header("������]")]
    [SerializeField] private GameObject VFX_Meteorite_touch;



    public GraphicRaycaster raycaster; // Canvas�� �߰��� GraphicRaycaster
    public EventSystem eventSystem; // EventSystem ��ü�� �Ҵ��ؾ� �մϴ�.


    private void Start()
    {

    }
    void Update()
    {



        Touch();




        //if (Input.GetMouseButtonDown(0))
        //{



        //    // ���콺 Ŭ�� ��ġ ���
        //    Vector2 mouseP = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        //        // ��Ʈ�� ��ġ�� ��ƼŬ ���
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
                        
                        // ��ġ�� ���۵� �������� ����
                        if (touch.phase == TouchPhase.Began)
                        {
                            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

                            // 2D Raycast�� ��ġ�� ������Ʈ Ȯ��
                            RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);

                            // ��ġ�� ������Ʈ�� ���� ��
                            if (hit.collider != null)
                            {
                                // ������Ʈ�� �±װ� "Fish"�� ���
                                if (hit.collider.CompareTag("Fish"))
                                {
                                    // ����Ʈ�� ��ġ ��ġ�� ��ȯ
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

                        // ��ġ�� ���۵� �������� ����
                        if (touch.phase == TouchPhase.Began)
                        {
                            // PointerEventData ����
                            PointerEventData pointerEventData = new PointerEventData(eventSystem);
                            pointerEventData.position = touch.position;

                            // Raycast ����� ���� ����Ʈ
                            List<RaycastResult> results = new List<RaycastResult>();

                            // Raycast ����
                            raycaster.Raycast(pointerEventData, results);

                            // ��ġ�� UI ��Ұ� ���� ��
                            if (results.Count > 0)
                            {
                                // ��ġ�� ù ��° UI ��� Ȯ��
                                GameObject touchedObject = results[0].gameObject;

                                // ������Ʈ�� �̸� �Ǵ� Ư�� �������� ��
                                if (touchedObject.tag == "Meteor")
                                {
                                    
                                    // ����Ʈ�� ��ġ ��ġ�� ��ȯ
                                    Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                                    Instantiate(VFX_Meteorite_touch, touchPosition, Quaternion.identity);
                                }
                            }
                            else
                            {
                                // ��ġ�� UI ��Ұ� ���� ���� ó��
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

                        // ��ġ�� ���۵� �������� ����
                        if (touch.phase == TouchPhase.Began)
                        {
                            // PointerEventData ����
                            PointerEventData pointerEventData = new PointerEventData(eventSystem);
                            pointerEventData.position = touch.position;

                            // Raycast ����� ���� ����Ʈ
                            List<RaycastResult> results = new List<RaycastResult>();

                            // Raycast ����
                            raycaster.Raycast(pointerEventData, results);

                            // ��ġ�� UI ��Ұ� ���� ��
                            if (results.Count > 0)
                            {
                                // ��ġ�� ù ��° UI ��� Ȯ��
                                GameObject touchedObject = results[0].gameObject;
                                Debug.Log(touchedObject);
                                // ������Ʈ�� �̸� �Ǵ� Ư�� �������� ��
                                if (touchedObject.tag == "Drop")
                                {

                                    // ����Ʈ�� ��ġ ��ġ�� ��ȯ
                                    touchedObject.GetComponent<SkeletonAnimation>().timeScale = 13f;
                                    Debug.Log("��� ��ġ");
                                }
                            }
                            else
                            {
                                // ��ġ�� UI ��Ұ� ���� ���� ó��
                            }
                        }
                    }
                    break;
                }

        }
    }
   
}


