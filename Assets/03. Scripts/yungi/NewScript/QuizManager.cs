using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    [System.Serializable]
    public class QuizC
    {
        public int index;
        public DType dtype;
        [TextArea]
        public string dialog;   
        public bool answer;
    }

    public QuizC[] quizC;
    [SerializeField]
    public Quiz quiz;//엑셀파일

    public TMP_Text text;
    private bool isStart;
    public bool answer;

    [SerializeField] private int count;
    public GameObject[] OX;

    [SerializeField] private GameObject rewardMessage;
    [SerializeField] private GameObject title;
    [SerializeField] private GameObject question;
    [SerializeField] private GameObject correct;
    [SerializeField] private GameObject fail;
    [SerializeField] private CameraController cameraController;

    private int currentQuestionIndex = 0; // 현재 풀고 있는 문제의 인덱스
    private int totalQuestions = 3; // 풀 문제 수
    private int correctAnswers = 0; // 정답 맞춘 개수
    [SerializeField] Inventory inventory;

    private float timer;
    private float startTimer = 2f;

    private void Awake()
    {
        int iDex = 1;
        for (int i = 0; i < quiz.Entities.Count; ++i)
        {
            quizC[iDex].index = quiz.Entities[i].index;
            quizC[iDex].dtype = quiz.Entities[i].type;
            quizC[iDex].dialog = quiz.Entities[i].dialog;
            quizC[iDex].answer = quiz.Entities[i].answer;

          
            iDex++;
        }
    }
    private void OnEnable()
    {
        Init();
    }

 
    void Init()
    {
        cameraController.GoQuiz();
        currentQuestionIndex = 0;
        correctAnswers = 0;

        text.text = quizC[0].dialog;
        isStart = false;

        OnOffUI(true);
        for (int i = 0; i < OX.Length; i++)
        {
            OX[i].SetActive(false);
        }
    }

    private void Update()
    {
        if (isStart)
            return;
        timer += Time.deltaTime;
        if(timer>startTimer)
        {
            
            isStart = true;
            for (int a = 0; a < OX.Length; a++)
            {
                OX[a].SetActive(true);
            }
            ShowNextQuestion(); //
            

        }

    }

    private void OnOffUI(bool value)
    {
        title.SetActive(value);
        question.SetActive(value);
        OXOnOff(value);
    }

    private void OXOnOff(bool value)
    {
        for (int a = 0; a < OX.Length; a++)
        {
            OX[a].SetActive(value);
        }
    }

    
    
    public void O()
    {
        CheckAnswer(true);
        OnOffUI(false);
    }

    public void X()
    {
        CheckAnswer(false);
        OnOffUI(false);
    }

    private void CheckAnswer(bool userAnswer)
    {
        if (userAnswer == answer)
        {
            correct.SetActive(true);
            correctAnswers++;
        }
        else
        {
            fail.SetActive(true);
        }

        StartCoroutine(ShowNextQuestionCoroutine());
    }

  
    private IEnumerator ShowNextQuestionCoroutine()
    {
        yield return new WaitForSeconds(1f); // 결과 보여주기 시간
       
        correct.SetActive(false);
        fail.SetActive(false);

        currentQuestionIndex++;

        if (currentQuestionIndex < totalQuestions)
        {
            ShowNextQuestion(); // 다음 문제로 넘어감
            OnOffUI(true);
        }
        else
        {
            StartCoroutine(ShowRewardMessage());
        }
    }


    private void ShowNextQuestion()
    {
        int i = Random.Range(1, quiz.Entities.Count + 1); // 무작위로 문제 선택
        text.text = quizC[i].dialog;
        answer = quizC[i].answer;
    }


    private IEnumerator ShowRewardMessage()
    {
        
        

        yield return new WaitForSeconds(1f);

        rewardMessage.SetActive(true);
        switch (correctAnswers)
        {
            case 0:
                for (int i = 0; i < ItemManager.instance.itemList.Length; i++)
                {
                    if (ItemManager.instance.itemList[i].id == 12)
                    {
                        rewardMessage.GetComponentInChildren<TMP_Text>().text = ItemManager.instance.itemList[i].itemName;
                        rewardMessage.GetComponentsInChildren<Image>()[1].sprite = ItemManager.instance.itemList[i].itemImage;
                        inventory.AddItem(ItemManager.instance.itemList[i], 1);
                        break;
                    }
                }
                break;
            case 1:
                for (int i = 0; i < ItemManager.instance.itemList.Length; i++)
                {
                    if (ItemManager.instance.itemList[i].id == 13)
                    {
                        rewardMessage.GetComponentInChildren<TMP_Text>().text = ItemManager.instance.itemList[i].itemName;
                        rewardMessage.GetComponentsInChildren<Image>()[1].sprite = ItemManager.instance.itemList[i].itemImage;
                        inventory.AddItem(ItemManager.instance.itemList[i], 1);
                        break;
                    }
                }
                break;
            case 2:
                for (int i = 0; i < ItemManager.instance.itemList.Length; i++)
                {
                    if (ItemManager.instance.itemList[i].id == 14)
                    {
                        rewardMessage.GetComponentInChildren<TMP_Text>().text = ItemManager.instance.itemList[i].itemName;
                        rewardMessage.GetComponentsInChildren<Image>()[1].sprite = ItemManager.instance.itemList[i].itemImage;
                        inventory.AddItem(ItemManager.instance.itemList[i], 1);
                        break;
                    }
                }
                break;
            case 3:
                for (int i = 0; i < ItemManager.instance.itemList.Length; i++)
                {
                    if (ItemManager.instance.itemList[i].id == 15)
                    {
                        rewardMessage.GetComponentInChildren<TMP_Text>().text = ItemManager.instance.itemList[i].itemName;
                        rewardMessage.GetComponentsInChildren<Image>()[1].sprite = ItemManager.instance.itemList[i].itemImage;
                        inventory.AddItem(ItemManager.instance.itemList[i], 1);
                        break;
                    }
                }
                break;

        }

        yield return new WaitForSeconds(2f);

        rewardMessage.SetActive(false);
        gameObject.SetActive(false);
        GameManager.Instance.OnOffUIStart(true);
        cameraController.goGarret();
    }

}
