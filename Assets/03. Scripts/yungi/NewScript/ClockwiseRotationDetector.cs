using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClockwiseRotationDetector : MonoBehaviour
{
    private List<Vector2> touchPositions = new List<Vector2>();
    private float minimumMovement = 0.1f; // 터치 이동 최소 거리 (조정 가능)
    private int clockwiseRotations = 0;
    private float totalAngle = 0f;

    public TMP_Text countDown;

    public float countTimer = 3;

    public bool isStart;

    public TMP_Text clearT;

    public Slider slider;


    [SerializeField] RewardManager rewardManager;
    [SerializeField] GameObject rewardPanel;

    [SerializeField] GameObject prueAnim;

    [SerializeField] private Inventory inventory;
    private void OnEnable()
    {
        prueAnim.SetActive(true);
        Init();
    }

    private void Init()
    {
        isStart = false;
        countTimer = 3;
        clockwiseRotations = 0;
    }
    void Update()
    {
        if (countTimer <= 0 && !isStart) //시작시
        {

            isStart = true;
            countDown.gameObject.SetActive(false);
            clearT.gameObject.SetActive(false);
            slider.gameObject.SetActive(true);
           


        }
        else if (!isStart)
        {
            countDown.text = countTimer.ToString("F0");
            countTimer -= Time.deltaTime;
        }

        if (!isStart)
            return;


        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position); // 수정된 부분
               
                if (touchPositions.Count == 0 || Vector2.Distance(touchPosition, touchPositions[touchPositions.Count - 1]) > minimumMovement)
                {

                    touchPositions.Add(touchPosition);
                    if (touchPositions.Count > 2)
                    {
                        int lastIndex = touchPositions.Count - 1;
                        Vector2 p1 = touchPositions[lastIndex - 2];
                        Vector2 p2 = touchPositions[lastIndex - 1];
                        Vector2 p3 = touchPositions[lastIndex];

                        Vector2 v1 = p2 - p1;
                        Vector2 v2 = p3 - p2;

                        float angle = -Vector2.SignedAngle(v1, v2);
                        totalAngle += angle;

                        if (Mathf.Abs(totalAngle) >= 360f)
                        {
                            if (totalAngle > 0)
                            {
                                clockwiseRotations++;
                                Debug.Log("Clockwise rotations: " + clockwiseRotations);
                                slider.value = (float)clockwiseRotations / 10;
                            }
                            totalAngle = 0f;
                        }

                        if (clockwiseRotations >= 10)
                        {
                            StartCoroutine(ShowRewardMessage1());
                            
                            // 클리어 처리 로직 추가
                            clockwiseRotations = 0;
                        }
                    }
                }
        }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                touchPositions.Clear();
                totalAngle = 0f;
            }
        }
        else
        {
            touchPositions.Clear();
            totalAngle = 0f;
        }
    }


    private IEnumerator ShowRewardMessage1()
    {

        slider.gameObject.SetActive(false);
        
        clearT.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);


        clearT.gameObject.SetActive(false);
        rewardPanel.SetActive(true);
        rewardPanel.GetComponentInChildren<TMP_Text>().text = "푸르의 이상한 포션 획득!";
        for (int i = 0; i < ItemManager.instance.itemList.Length; i++)
        {
            if (ItemManager.instance.itemList[i].id == 0)
            {
                rewardPanel.GetComponentsInChildren<Image>()[1].sprite = ItemManager.instance.itemList[i].itemImage;
                inventory.AddItem(ItemManager.instance.itemList[i], 1);
                break;
            }
        }

        yield return new WaitForSeconds(3f);

        rewardPanel.SetActive(false);
        gameObject.SetActive(false);
        rewardManager.RewardRandom(reward_T.mini);

        prueAnim.SetActive(false);
        GameManager.Instance.OnOffUIStart(true);
    }
}
