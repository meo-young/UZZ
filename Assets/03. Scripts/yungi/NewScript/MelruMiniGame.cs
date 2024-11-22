using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static RewardManager;

public class MelruMiniGame : MonoBehaviour
{
    public Image food;
    public float ctimer;
    public int check=0;

    public TMP_Text countDown;

    public float countTimer = 3;

    public bool isStart;
    public float respawnTimer=1;
    public bool offFood;

    public TMP_Text clearT;
    public TMP_Text failT;
    public int fail;

    [SerializeField] RewardManager rewardManager;
    [SerializeField] GameObject rewardPanel;
    private void Update()
    {
        
        if(fail>=1 && isStart)
        {
            isStart = false;
            StartCoroutine(GameFail());
            
         
        }
        if (check >= 5 &&isStart)
        {
            isStart = false;
            StartCoroutine(ShowRewardMessage1());
           
        }
        if (countTimer <= 0 && !isStart && check<=4)
        {

            isStart = true;
            countDown.gameObject.SetActive(false);

            food.gameObject.SetActive(true);
            food.rectTransform.localPosition = new Vector3(0, 1000, 0);

        }
        else
        {
            countDown.text = countTimer.ToString("F0");
            countTimer -= Time.deltaTime;
        }

        if (!isStart)
            return;

        if(offFood)
        {
            respawnTimer -= Time.deltaTime;
            if (respawnTimer <= 0)
            {
                respawnTimer = 1;
                offFood = false;
                food.gameObject.SetActive(true);
                food.rectTransform.localPosition = new Vector3(0, 1000, 0);
            }
        }


        if (offFood)
            return;
        
          
        
        ctimer += Time.deltaTime;
        Vector3 currentPos = food.rectTransform.localPosition;

        currentPos.y -=  Time.deltaTime;

        food.rectTransform.localPosition = currentPos;

        if (Input.touchCount > 0)
        {
            
            Touch touch = Input.GetTouch(0);

            // 터치 시작 혹은 움직일 때 localPosition을 (0, 0, 0)으로 설정합니다.
            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
            {
                offFood = true;
                if (ctimer > 1.8 && ctimer < 2.2)
                    check++;
                else
                    fail++;
                ctimer = 0;
                food.gameObject.SetActive(false);
            }
        }
        else
        {
            

            currentPos.y -= 500 * Time.deltaTime;

            food.rectTransform.localPosition = currentPos;


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


    }
    private IEnumerator GameFail()
    {

        failT.gameObject.SetActive(true);


        yield return new WaitForSeconds(3f);

        gameObject.SetActive(false);

        GameManager.Instance.OnOffUIStart(true);
    }
}
