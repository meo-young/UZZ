using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HenryMiniGame : MonoBehaviour
{
    public TMP_Text countDown;

    public float countTimer = 3;

    public bool isStart;

    public TMP_Text clearT;
    public TMP_Text failT;

    public int touchCount = 0; // 클리어 하기 위해 필요한 시간
    public int touchTotal = 30;

    public Slider slider;


    [SerializeField] RewardManager rewardManager;
    [SerializeField] GameObject rewardPanel;
    // Update is called once per frame
    void Update()
    {
        if (countTimer <= 0 && !isStart) //시작시
        {

            isStart = true;
            countDown.gameObject.SetActive(false);


        }
        else if (!isStart)
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
                touchCount++;
                slider.value = (float)touchCount / (float)touchTotal;
            }

            
        }
        if(touchCount >= touchTotal)
        {
            isStart = false;
            clearT.gameObject.SetActive(true);

            
            touchCount = 0;
        }
    }

    private IEnumerator ShowRewardMessage1()
    {


        clearT.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);


        clearT.gameObject.SetActive(false);
        rewardPanel.SetActive(true);

        yield return new WaitForSeconds(3f);

        rewardPanel.SetActive(false);
        gameObject.SetActive(false);
        rewardManager.RewardRandom(reward_T.mini);
        GameManager.Instance.OnOffUIStart(true);

    }

   
}
