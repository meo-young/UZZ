using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static QuizManager;

public class OgeulaeMiniGame : MonoBehaviour
{
    [System.Serializable]
    public struct Fishes
    {
        public GameObject prefab;
        public GameObject prefab2;
        public int index;
        public string name;
        public string moonfish_Flavortext;
        public int score;
        public int score2;
        public float chance;
        public float chance2;
        public float speed;
        public int touch;
        public int touch2;

        public float moonfish_reward_1;
        public float moonfish_reward_1b;
        public float moonfish_reward_2;
        public float moonfish_reward_2b;
        public float moonfish_reward_3;
        public float moonfish_reward_3b;
        public float moonfish_reward_4;
        public float moonfish_reward_4b;
    }




    

    public TMP_Text countDown;
    public TMP_Text gameTime;

    public float countTimer = 3;

    public bool isStart;
    public bool isEnd;


    public Transform[] spawnPoints;
    public Fishes[] fishes;
    public UZZ_DataTable UZZ_DataTable;

    public float respawnTime;
    public float rTime;
    public float fTime;
    public int count;
    // Start is called before the first frame update

    public int spawnCount;
    
    float totalWeight = 0f;


    [SerializeField] private Inventory inventory;
    [SerializeField] private TMP_Text lastScore;
    [SerializeField] private GameObject rewardMessage1;
    [SerializeField] private TMP_Text rewardTextName;
    [SerializeField] private TMP_Text rewardTextDes;
    [SerializeField] private GameObject rewardMessage2; 
    [SerializeField] private GameObject countDownG;
    [SerializeField] private GameObject question;
    [SerializeField] private Image rewardItemImage;
    [SerializeField] private GameObject UI_goGarret;
    [SerializeField] private Collection collection;
    [SerializeField] private SpiritManager spiritManager_Og;

    [SerializeField] List<GameObject> catched = new List<GameObject>();


    [SerializeField] private MouseTouch mouseTouch;

    private void OnEnable()
    {
        Init();
    }


    void Init()
    {
        spiritManager_Og.CanTalk(false);
        mouseTouch.miniGameT = MiniGame.Ogre;
        UI_goGarret.SetActive(false);
        GameManager.Instance.score = 0;
        fTime = 30f;
        countTimer = 3f;
        spawnCount = 0;
        isEnd = false;
        isStart = false;


        lastScore.gameObject.SetActive(false);


        countDown.gameObject.SetActive(true);
        question.SetActive(true);
        rewardMessage1.SetActive(false);
        rewardMessage2.SetActive(false);
        catched.Clear();
    }
    private void Awake()
    {
        
        for (int i = 0; i < UZZ_DataTable.OhgueraeMinigameTable.Count; ++i)
        {
            fishes[i].index = UZZ_DataTable.OhgueraeMinigameTable[i].index;
            fishes[i].name = UZZ_DataTable.OhgueraeMinigameTable[i].moonfish_Name;
            fishes[i].moonfish_Flavortext = UZZ_DataTable.OhgueraeMinigameTable[i].moonfish_Flavortext;
            fishes[i].score = UZZ_DataTable.OhgueraeMinigameTable[i].moonfish_Score; 
            fishes[i].score2 = UZZ_DataTable.OhgueraeMinigameTable[i].moonfish_Score_b; 
            fishes[i].chance =UZZ_DataTable.OhgueraeMinigameTable[i].moonfish_Chance;
            fishes[i].chance2 = UZZ_DataTable.OhgueraeMinigameTable[i].moonfish_Chance_b;
            fishes[i].speed = UZZ_DataTable.OhgueraeMinigameTable[i].moonfish_Speed;
            fishes[i].touch = UZZ_DataTable.OhgueraeMinigameTable[i].moonfish_Touch;
            fishes[i].touch2 = UZZ_DataTable.OhgueraeMinigameTable[i].moonfish_Touch_b;


            fishes[i].moonfish_reward_1 = UZZ_DataTable.OhgueraeMinigameTable[i].moonfish_reward_1;
            fishes[i].moonfish_reward_1b = UZZ_DataTable.OhgueraeMinigameTable[i].moonfish_reward_1b;
            fishes[i].moonfish_reward_2 = UZZ_DataTable.OhgueraeMinigameTable[i].moonfish_reward_2;
            fishes[i].moonfish_reward_2b = UZZ_DataTable.OhgueraeMinigameTable[i].moonfish_reward_2b;
            fishes[i].moonfish_reward_3 = UZZ_DataTable.OhgueraeMinigameTable[i].moonfish_reward_3;
            fishes[i].moonfish_reward_3b = UZZ_DataTable.OhgueraeMinigameTable[i].moonfish_reward_3b;
            fishes[i].moonfish_reward_4 = UZZ_DataTable.OhgueraeMinigameTable[i].moonfish_reward_4; 
            fishes[i].moonfish_reward_4b = UZZ_DataTable.OhgueraeMinigameTable[i].moonfish_reward_4b;





            totalWeight += fishes[i].chance;
        }
        
      
    }
    int RandomValue()
    {
        float randomValue = UnityEngine.Random.value * totalWeight;

        // 랜덤 값이 어느 범위에 속하는지 확인하여 항목 선택
        foreach (var fish in fishes)
        {
            randomValue -= fish.chance;
            if (randomValue <= 0)
                return fish.index;
        }

        return fishes[fishes.Length-1].index;
    }

        
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Init();
        if (isEnd)
            return;
        if (countTimer <= 0 && !isStart) //시작시
        {
            gameTime.gameObject.SetActive(true);
            isStart = true;
            countDown.gameObject.SetActive(false);
            question.SetActive(false);
           


        }
        else if (!isStart)
        {
            countDown.text = countTimer.ToString("F0");
            countTimer -= Time.deltaTime;
        }

        if (!isStart)
            return;

        fTime -= Time.deltaTime;
        rTime += Time.deltaTime;
        gameTime.text = fTime.ToString("F0");
        if (rTime > respawnTime&&spawnCount<15)
        {
            rTime = 0;
            spawnCount++;
            for (int i = 0; i < 3; i++)
            {
                Spawn();
            }
           
        }
            

        if (fTime <= 0 && !isEnd)
        {
            isStart = false;
            gameTime.gameObject.SetActive(false);
           
            StartCoroutine(ShowRewardMessage1());
        }
            
    }

    void Spawn()
    {
        int i = RandomValue()-1;
        int j = Random.Range(0, spawnPoints.Length);

        int ran = Random.Range(0, 100);
        
        if(ran<10)
        {
            GameObject f = Instantiate(fishes[i].prefab2, spawnPoints[j]);
            f.GetComponent<Fish>().Init(fishes[i].index, fishes[i].name, fishes[i].speed, fishes[i].touch2, fishes[i].score2);
        }
        else
        {
            GameObject f = Instantiate(fishes[i].prefab, spawnPoints[j]);
            f.GetComponent<Fish>().Init(fishes[i].index, fishes[i].name, fishes[i].speed, fishes[i].touch, fishes[i].score);
        }
        
        
    }
    


    public void RewardClick()
    {
        StartCoroutine(ShowRewardMessage2());
        lastScore.gameObject.SetActive(false);
    }

    private IEnumerator ShowRewardMessage1()
    {
        
        isEnd = true;
        lastScore.text = GameManager.Instance.score.ToString();
        lastScore.gameObject.SetActive(true);
       

        yield return new WaitForSeconds(1f);

        rewardMessage1.SetActive(true);

        float[] probabilities = new float[fishes.Length];
        

        if (GameManager.Instance.score <= 1200)
        {
            for (int i = 0; i < fishes.Length; i++)
            {
                probabilities[i] = fishes[i].moonfish_reward_1;  // 예: 0.02, 0.03 등

               
            }
            int selectedItemIndex = SelectItemByProbability(probabilities);
            // 선택된 아이템에 따라 보상 처리 (예: fishes[selectedItemIndex])
            Debug.Log("Selected item: " + selectedItemIndex);
            rewardMessage1.GetComponentsInChildren<TMP_Text>()[0].text = fishes[selectedItemIndex].name;
            float a = Random.Range(0, 1);
            if (a < fishes[selectedItemIndex].moonfish_reward_1b)
            {
                rewardItemImage.sprite = fishes[selectedItemIndex].prefab2.GetComponent<SpriteRenderer>().sprite;
                collection.GetFish(selectedItemIndex + 1);
            }
            else
            {
                rewardItemImage.sprite = fishes[selectedItemIndex].prefab.GetComponent<SpriteRenderer>().sprite;
                collection.GetFish(selectedItemIndex);
            }
            rewardMessage1.GetComponentsInChildren<TMP_Text>()[1].text = fishes[selectedItemIndex].moonfish_Flavortext;

            
        }
        else if (GameManager.Instance.score <= 1700)
        {
            for (int i = 0; i < fishes.Length; i++)
            {
                probabilities[i] = fishes[i].moonfish_reward_2;  // 예: 0.02, 0.03 등


            }
            int selectedItemIndex = SelectItemByProbability(probabilities);
            // 선택된 아이템에 따라 보상 처리 (예: fishes[selectedItemIndex])
            Debug.Log("Selected item: " + selectedItemIndex);
            rewardMessage1.GetComponentsInChildren<TMP_Text>()[0].text = fishes[selectedItemIndex].name;
            int a = Random.Range(0, 100);
            if (a < fishes[selectedItemIndex].moonfish_reward_2b)
            {
                rewardItemImage.sprite = fishes[selectedItemIndex].prefab2.GetComponent<SpriteRenderer>().sprite;
                collection.GetFish(selectedItemIndex + 1);
            }
            else
            {
                rewardItemImage.sprite = fishes[selectedItemIndex].prefab.GetComponent<SpriteRenderer>().sprite;
                collection.GetFish(selectedItemIndex);
            }
            rewardMessage1.GetComponentsInChildren<TMP_Text>()[1].text = fishes[selectedItemIndex].moonfish_Flavortext;
        }
        else if (GameManager.Instance.score <= 2200)
        {
            for (int i = 0; i < fishes.Length; i++)
            {
                probabilities[i] = fishes[i].moonfish_reward_3;  // 예: 0.02, 0.03 등


            }
            int selectedItemIndex = SelectItemByProbability(probabilities);
            // 선택된 아이템에 따라 보상 처리 (예: fishes[selectedItemIndex])
            Debug.Log("Selected item: " + selectedItemIndex);
            rewardMessage1.GetComponentsInChildren<TMP_Text>()[0].text = fishes[selectedItemIndex].name;
            int a = Random.Range(0, 100);
            if (a < fishes[selectedItemIndex].moonfish_reward_3b)
            {
                rewardItemImage.sprite = fishes[selectedItemIndex].prefab2.GetComponent<SpriteRenderer>().sprite;
                collection.GetFish(selectedItemIndex + 1);
            }
            else
            {
                rewardItemImage.sprite = fishes[selectedItemIndex].prefab.GetComponent<SpriteRenderer>().sprite;
                collection.GetFish(selectedItemIndex);
            }
            rewardMessage1.GetComponentsInChildren<TMP_Text>()[1].text = fishes[selectedItemIndex].moonfish_Flavortext;
        }
        else
        {
            for (int i = 0; i < fishes.Length; i++)
            {
                probabilities[i] = fishes[i].moonfish_reward_4;  // 예: 0.02, 0.03 등


            }
            int selectedItemIndex = SelectItemByProbability(probabilities);
            // 선택된 아이템에 따라 보상 처리 (예: fishes[selectedItemIndex])
            Debug.Log("Selected item: " + selectedItemIndex);
            rewardMessage1.GetComponentsInChildren<TMP_Text>()[0].text = fishes[selectedItemIndex].name;
            int a = Random.Range(0, 100);
            if (a < fishes[selectedItemIndex].moonfish_reward_4b)
            {
                rewardItemImage.sprite = fishes[selectedItemIndex].prefab2.GetComponent<SpriteRenderer>().sprite;
                collection.GetFish(selectedItemIndex + 1);
            }
            else
            {
                rewardItemImage.sprite = fishes[selectedItemIndex].prefab.GetComponent<SpriteRenderer>().sprite;
                collection.GetFish(selectedItemIndex);
            }
            rewardMessage1.GetComponentsInChildren<TMP_Text>()[1].text = fishes[selectedItemIndex].moonfish_Flavortext;
        }




    }

    private int SelectItemByProbability(float[] probabilities)
    {
        float totalSum = 0f;
        foreach (float value in probabilities)
        {
            totalSum += value;
        }

        // 0과 totalSum 사이의 랜덤 float 값 생성
        float randomPoint = Random.Range(0f, totalSum);
        float cumulativeSum = 0f;

        for (int i = 0; i < probabilities.Length; i++)
        {
            cumulativeSum += probabilities[i];
            if (randomPoint < cumulativeSum)
            {
                return i; // 선택된 아이템의 인덱스를 반환
            }
        }

        return 0; // 기본값
    }

    private IEnumerator ShowRewardMessage2()
    {

        //달멸치 획득
        if (GameManager.Instance.score <= 1200)
        {
            rewardMessage2.GetComponentInChildren<TMP_Text>().text = "달물고기";
            for (int i = 0; i < ItemManager.instance.itemList.Length; i++)
            {
                if (ItemManager.instance.itemList[i].id== 24 )
                {
                    rewardMessage2.GetComponentsInChildren<Image>()[1].sprite = ItemManager.instance.itemList[i].itemImage;
                    inventory.AddItem(ItemManager.instance.itemList[i], 1);
                    break;
                }
            }
          
        }
        else if (GameManager.Instance.score <= 1700)
        {
            rewardMessage2.GetComponentInChildren<TMP_Text>().text = "달우럭";
            for (int i = 0; i < ItemManager.instance.itemList.Length; i++)
            {
                if (ItemManager.instance.itemList[i].id == 25)
                {
                    rewardMessage2.GetComponentsInChildren<Image>()[1].sprite = ItemManager.instance.itemList[i].itemImage;
                    inventory.AddItem(ItemManager.instance.itemList[i], 1);
                    break;
                }
            }
        }
        else if (GameManager.Instance.score <= 2200)
        {
            rewardMessage2.GetComponentInChildren<TMP_Text>().text = "달고등어";
            for (int i = 0; i < ItemManager.instance.itemList.Length; i++)
            {
                if (ItemManager.instance.itemList[i].id == 26)
                {
                    rewardMessage2.GetComponentsInChildren<Image>()[1].sprite = ItemManager.instance.itemList[i].itemImage;
                    Inventory.Instance.AddItem(ItemManager.instance.itemList[i], 1);
                    break;
                }
            }
        }
        else
        {
            rewardMessage2.GetComponentInChildren<TMP_Text>().text = "달금붕어";
            for (int i = 0; i < ItemManager.instance.itemList.Length; i++)
            {
                if (ItemManager.instance.itemList[i].id == 27)
                {
                    rewardMessage2.GetComponentsInChildren<Image>()[1].sprite = ItemManager.instance.itemList[i].itemImage;
                    Inventory.Instance.AddItem(ItemManager.instance.itemList[i], 1);
                    break;
                }
            }
        }


        rewardMessage1.SetActive(false);
        rewardMessage2.SetActive(true);
        question.SetActive(false);
        yield return new WaitForSeconds(3f);

        rewardMessage2.SetActive(false);
        gameObject.SetActive(false);
        UI_goGarret.SetActive(true);

        mouseTouch.miniGameT = MiniGame.Default;
        GameManager.Instance.OnOffUIStart(true);
        spiritManager_Og.CanTalk(true);
    }
}
