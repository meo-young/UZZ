using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using static OgeulaeMiniGame;

public class MetorMiniGame : MonoBehaviour
{
    // Start is called before the first frame update


    [SerializeField] private Collection collection;
    [SerializeField] private Inventory inventory;
    [SerializeField] private GameObject mouse;
    [SerializeField] private GameObject[] meteor;
    
    [SerializeField] private GameObject noticeMessage;
    [SerializeField] private GameObject rewardMessage;
    [SerializeField] private GameObject rewardUI;
    
    public Meteor[] meteors;

    
    public UZZ_DataTable UZZ_DataTable;
    float totalWeight = 0f;
    private int _touchCnt = 0;

    [Header("For Debug")]
    [SerializeField] private float messageShowTime = 1.5f;
    [SerializeField] private int needTouchCnt = 5;

    private int selectedItemIndex;

    
    [SerializeField] private GameObject VFX_meteorite_jewel;
    [SerializeField] private GameObject VFX_meteorite_myth;
    [SerializeField] private GameObject VFX_meteorite_space;
    [SerializeField] private GameObject VFX_meteorite_stone;

    [SerializeField] private MouseTouch mouseTouch;
    [System.Serializable]
    public struct Meteor
    {
        public Sprite sprite;
        public int index;
        public string name;
        public string flavortext;
        public float chance;
        public int count;
        public int grade;
    }

    RaycastHit hit;
    GameObject VFX_meteorite;

    private void OnEnable()
    {
        Init();
    }


    void Init()
    {
        Camera.main.orthographicSize = 12;
        mouseTouch.miniGameT = MiniGame.Meteor;
        _touchCnt = 0;
        rewardMessage.SetActive(false);
        rewardUI.SetActive(false);
        OffMeteor();
        meteor[0].SetActive(true);
    
        noticeMessage.SetActive(true);
    }

    void OffMeteor()
    {
        for (int i = 0; i < meteor.Length; i++)
        {
            meteor[i].SetActive(false);
        }
    }
    

    private void Awake()
    {

        for (int i = 0; i < UZZ_DataTable.SaintMinigameTable.Count; ++i)
        {
            meteors[i].index = UZZ_DataTable.SaintMinigameTable[i].index;
            meteors[i].name = UZZ_DataTable.SaintMinigameTable[i].meteor_Name;
            meteors[i].chance = UZZ_DataTable.SaintMinigameTable[i].meteor_Chance;
            meteors[i].count = UZZ_DataTable.SaintMinigameTable[i].count;
            meteors[i].grade = UZZ_DataTable.SaintMinigameTable[i].meteor_Grade;
            meteors[i].flavortext = UZZ_DataTable.SaintMinigameTable[i].meteor_Flavortext;
            totalWeight += meteors[i].chance;
        }


    }
 

    public void ClickMeteor()
    {
        _touchCnt++;
        

        if (_touchCnt > 50)
        {
            StartCoroutine(ShowRewardMessage(messageShowTime));
        }
        else if(_touchCnt > 40)
        {
            OffMeteor();
            meteor[4].SetActive(true);
        }
        else if(_touchCnt > 30)
        {
            OffMeteor();
            meteor[3].SetActive(true);
        }
        else if(_touchCnt > 20)
        {
            OffMeteor();
            meteor[2].SetActive(true);
        }
        else if(_touchCnt > 10)
        {
            OffMeteor();
            meteor[1].SetActive(true);
        }

    }

    public void RewardClick()
    {
        StartCoroutine(ShowRewardMessage2());
    }

    private IEnumerator ShowRewardMessage(float time)
    {
        float[] probabilities = new float[meteors.Length];
        for (int a = 0; a < meteors.Length; a++)
        {
            probabilities[a] = meteors[a].chance;  // ¿¹: 0.02, 0.03 µî


        }
        selectedItemIndex = SelectItemByProbability(probabilities);

        rewardUI.GetComponentsInChildren<TMP_Text>()[0].text = meteors[selectedItemIndex].name;
        rewardUI.GetComponentsInChildren<TMP_Text>()[1].text = meteors[selectedItemIndex].flavortext;
        rewardUI.GetComponentsInChildren<Image>()[1].sprite = meteors[selectedItemIndex].sprite;
        if (meteors[selectedItemIndex].grade == 0)
        {
            VFX_meteorite = Instantiate(VFX_meteorite_space, transform.position, Quaternion.identity);
        }
        else if (meteors[selectedItemIndex].grade == 1)
        {
            VFX_meteorite = Instantiate(VFX_meteorite_myth, transform.position, Quaternion.identity);
        }
        else if (meteors[selectedItemIndex].grade == 2)
        {
            VFX_meteorite = Instantiate(VFX_meteorite_jewel, transform.position, Quaternion.identity);
        }
        else if (meteors[selectedItemIndex].grade == 3)
        {
            VFX_meteorite = Instantiate(VFX_meteorite_stone, transform.position, Quaternion.identity);
        }
        collection.GetMeteor(selectedItemIndex);
        OffMeteor();
        noticeMessage.SetActive(false);
      
        
        yield return new WaitForSeconds(1);

        rewardUI.SetActive(true);


    }
    private int SelectItemByProbability(float[] probabilities)
    {
        float totalSum = 0f;
        foreach (float value in probabilities)
        {
            totalSum += value;
        }

        // 0°ú totalSum »çÀÌÀÇ ·£´ý float °ª »ý¼º
        float randomPoint = Random.Range(0f, totalSum);
        float cumulativeSum = 0f;

        for (int i = 0; i < probabilities.Length; i++)
        {
            cumulativeSum += probabilities[i];
            if (randomPoint < cumulativeSum)
            {
                return i; // ¼±ÅÃµÈ ¾ÆÀÌÅÛÀÇ ÀÎµ¦½º¸¦ ¹ÝÈ¯
            }
        }

        return 0; // ±âº»°ª
    }
    private IEnumerator ShowRewardMessage2()
    {
        switch(meteors[selectedItemIndex].grade)
        {
            case 0:
                for (int i = 0; i < ItemManager.instance.itemList.Length; i++)
                {
                    if (ItemManager.instance.itemList[i].id == 23)
                    {
                        rewardMessage.GetComponentInChildren<TMP_Text>().text = ItemManager.instance.itemList[i].itemName + "È¹µæ!";
                        rewardMessage.GetComponentsInChildren<Image>()[1].sprite = ItemManager.instance.itemList[i].itemImage;
                        inventory.AddItem(ItemManager.instance.itemList[i], 1);
                        break;
                    }
                }
                break;
            case 1:
                for (int i = 0; i < ItemManager.instance.itemList.Length; i++)
                {
                    if (ItemManager.instance.itemList[i].id == 22)
                    {
                        rewardMessage.GetComponentInChildren<TMP_Text>().text = ItemManager.instance.itemList[i].itemName + "È¹µæ!";
                        rewardMessage.GetComponentsInChildren<Image>()[1].sprite = ItemManager.instance.itemList[i].itemImage;
                        inventory.AddItem(ItemManager.instance.itemList[i], 1);
                        break;
                    }
                }
                break;
            case 2:
                for (int i = 0; i < ItemManager.instance.itemList.Length; i++)
                {
                    if (ItemManager.instance.itemList[i].id == 21)
                    {
                        rewardMessage.GetComponentInChildren<TMP_Text>().text = ItemManager.instance.itemList[i].itemName + "È¹µæ!";
                        rewardMessage.GetComponentsInChildren<Image>()[1].sprite = ItemManager.instance.itemList[i].itemImage;
                        inventory.AddItem(ItemManager.instance.itemList[i], 1);
                        break;
                    }
                }
                break;
            case 3:
                for (int i = 0; i < ItemManager.instance.itemList.Length; i++)
                {
                    if (ItemManager.instance.itemList[i].id == 20)
                    {
                        rewardMessage.GetComponentInChildren<TMP_Text>().text = ItemManager.instance.itemList[i].itemName + "È¹µæ!";
                        rewardMessage.GetComponentsInChildren<Image>()[1].sprite = ItemManager.instance.itemList[i].itemImage;
                        inventory.AddItem(ItemManager.instance.itemList[i], 1);
                        break;
                    }
                }
                break;
        }
        //¿Ü°èÀÇ°¡·ç È¹µæ

        rewardUI.SetActive(false);
        rewardMessage.SetActive(true);

        yield return new WaitForSeconds(3f);


        gameObject.SetActive(false);

        mouseTouch.miniGameT = MiniGame.Default;
        GameManager.Instance.OnOffUIStart(true);
        VFX_meteorite.SetActive(false);
        Camera.main.orthographicSize = 18;
    }
}
