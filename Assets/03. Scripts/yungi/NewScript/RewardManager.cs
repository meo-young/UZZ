using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static RewardManager;

public class RewardManager : MonoBehaviour
{
    public UZZ_DataTable UZZ_DataTable;

    [System.Serializable]
    public struct rewards
    {
        
        public int index;
        public reward_T reward_Type;
        public bool reward_Petal;
        public int reward_Petal_Count;
        public bool reward_Dew;
        public int reward_Dew_Count;
        public bool reward_Beads;
        public int reward_Beads_Count;

    }

     public rewards[] reward;

    [SerializeField]  private GameObject UI_Reward;
    [SerializeField]  private GameObject petal;
    [SerializeField]  private GameObject dew;
    [SerializeField]  private GameObject bead;

    [Header("�޽�����")]
    [SerializeField] private TMP_Text rewardNameText;
    [SerializeField] private TMP_Text rewardDescText;
    [SerializeField] private GameObject AdBtn;
    [SerializeField] private GameObject OkBtn;
    [SerializeField] private GameObject text_ballon;

    [Header("����")]
    [SerializeField] private Image image;
    [SerializeField] private Sprite[] sprites;
  

    [Header("�޽Ŀ�")]
    [SerializeField]  private GameObject UI_Reward_Rest;
    [SerializeField] private GameObject petal_Rest;
    [SerializeField] private GameObject dew_Rest;
    [SerializeField] private GameObject bead_Rest;


    public Reward Reward;
    public Reward Reward_Rest;
    private void Awake()
    {
        

        for (int i = 0; i < UZZ_DataTable.RewardTable.Count; ++i)
        {
       

            reward[i].index = UZZ_DataTable.RewardTable[i].index;
            reward[i].reward_Type = UZZ_DataTable.RewardTable[i].reward_Type;
            reward[i].reward_Petal= UZZ_DataTable.RewardTable[i].reward_Petal;
            reward[i].reward_Petal_Count = UZZ_DataTable.RewardTable[i].reward_Petal_Count;
            reward[i].reward_Dew = UZZ_DataTable.RewardTable[i].reward_Dew;
            reward[i].reward_Dew_Count = UZZ_DataTable.RewardTable[i].reward_Dew_Count;
            reward[i].reward_Beads = UZZ_DataTable.RewardTable[i].reward_Beads;
            reward[i].reward_Beads_Count = UZZ_DataTable.RewardTable[i].reward_Beads_Count;




        }
     
        
        

    }

    private void Start()
    {
        //�޽ĺ���
        RewardRandom(reward_T.rest);
    }


    public void RewardRandom(reward_T type, int value = 0)
    {
        int count = 0;
        int first = 0;
        for (int i = 0; i < reward.Length; i++)
        {
            if (reward[i].reward_Type == type)
            {
                first = i;
                count = i;
                break;
            }
                

        }
        Debug.Log("first" + first);
        
        for (int i = 0; i < reward.Length; i++)
        {
            if (reward[i].reward_Type == type)
                count++;

        }
        switch(type)
        {
            case reward_T.rest:
                RewardOff();
                UI_Reward_Rest.SetActive(true);
                rewardNameText.text = "�޽� ����";
                rewardDescText.text = "���ƿԱ���! ��ٷȾ�!";
                AdBtn.SetActive(false);
                OkBtn.SetActive(true);
                text_ballon.SetActive(false);
                break;
            case reward_T.mini:
                RewardOff();
                UI_Reward.SetActive(true);
                rewardNameText.text = "�̴ϰ��� ����";
                rewardDescText.text = "�̴ϰ��� �����Դϴ�";
                AdBtn.SetActive(false);
                OkBtn.SetActive(true);
                text_ballon.SetActive(false);
                break;
            case reward_T.goblin:
                RewardOff();
                UI_Reward.SetActive(true);
                rewardNameText.text = "������ ����";
                rewardDescText.text = "������ ������ �ΰ� ����!\r\n���� ���� ������ �� ��� ������?";
                AdBtn.SetActive(true);
                OkBtn.SetActive(true);
                text_ballon.SetActive(true);
                break;
            case reward_T.ad:
                RewardOff();
                UI_Reward.SetActive(true);
                switch(value)
                {
                    case 0:
                        rewardNameText.text = "����� ����";
                        break;
                    case 1:
                        rewardNameText.text = "����� ����";
                        break;
                    case 2:
                        rewardNameText.text = "������ ����";
                        break;
                }

                rewardDescText.text = "���� ���� �Ƿʰ� �� �ȴٸ�\r\n���� �ϳ� ���ֽǷ��䡦?";
                AdBtn.SetActive(true);
                OkBtn.SetActive(false);
                text_ballon.SetActive(false);
                image.sprite = sprites[value];

                break;
        }

      
        int randomIndex = UnityEngine.Random.Range(first, count);
        Debug.Log("randomIndex" + randomIndex);

        if (type == reward_T.rest)
        {
            RewardOn_rest(
                reward[randomIndex].reward_Petal, reward[randomIndex].reward_Petal_Count,
           reward[randomIndex].reward_Dew, reward[randomIndex].reward_Dew_Count,
           reward[randomIndex].reward_Beads, reward[randomIndex].reward_Beads_Count);
            Reward_Rest.reward_Petal_Count = 0;
            Reward_Rest.reward_Dew_Count = 0;
            Reward_Rest.reward_Beads_Count = 0;
            if (reward != null)
            {
                if (reward[randomIndex].reward_Petal)
                {

                    Reward_Rest.reward_Petal_Count = reward[randomIndex].reward_Petal_Count;
                }

                if (reward[randomIndex].reward_Dew)
                {

                    Reward_Rest.reward_Dew_Count = reward[randomIndex].reward_Dew_Count;
                }

                if (reward[randomIndex].reward_Beads)
                {

                    Reward_Rest.reward_Beads_Count = reward[randomIndex].reward_Beads_Count;
                }
            }
        }
        else
        {
           RewardOn(
           reward[randomIndex].reward_Petal, reward[randomIndex].reward_Petal_Count,
           reward[randomIndex].reward_Dew, reward[randomIndex].reward_Dew_Count,
           reward[randomIndex].reward_Beads, reward[randomIndex].reward_Beads_Count);

            Reward.reward_Petal_Count = 0;
            Reward.reward_Dew_Count = 0;
            Reward.reward_Beads_Count = 0;
            if (reward != null)
            {
                if (reward[randomIndex].reward_Petal)
                {

                    Reward.reward_Petal_Count = reward[randomIndex].reward_Petal_Count;
                }

                if (reward[randomIndex].reward_Dew)
                {

                    Reward.reward_Dew_Count = reward[randomIndex].reward_Dew_Count;
                }

                if (reward[randomIndex].reward_Beads)
                {

                    Reward.reward_Beads_Count = reward[randomIndex].reward_Beads_Count;
                }
            }

        }

        
       
    }

    void RewardOn(bool petal_b,int petal_c, bool dew_b, int dew_c, bool beads_b,int beads_c)
    {
       
        if (petal_b)
        {
            petal.SetActive(true);
            petal.GetComponentInChildren<TMP_Text>().text = petal_c.ToString();
        }

        if (dew_b)
        {
            // ���� �̽� ���� ����
            dew.SetActive(true);
            dew.GetComponentInChildren<TMP_Text>().text = dew_c.ToString();
        }

        if (beads_b)
        {
            // ���� �������� ���� ����
            bead.SetActive(true);
            bead.GetComponentInChildren<TMP_Text>().text = beads_c.ToString();
        }
    }

    void RewardOn_rest(bool petal_b, int petal_c, bool dew_b, int dew_c, bool beads_b, int beads_c)
    {
        
        if (petal_b)
        {
            petal_Rest.SetActive(true);
            petal_Rest.GetComponentInChildren<TMP_Text>().text = petal_c.ToString();
        }

        if (dew_b)
        {
            // ���� �̽� ���� ����
            dew_Rest.SetActive(true);
            dew_Rest.GetComponentInChildren<TMP_Text>().text = dew_c.ToString();
        }

        if (beads_b)
        {
            // ���� �������� ���� ����
            bead_Rest.SetActive(true);
            bead_Rest.GetComponentInChildren<TMP_Text>().text = beads_c.ToString();
        }
    }

    

    public void RewardOff()
    {
        UI_Reward_Rest.SetActive(false);
        UI_Reward.SetActive(false);
        petal.SetActive(false);
        dew.SetActive(false);
        bead.SetActive(false);
    }
}
