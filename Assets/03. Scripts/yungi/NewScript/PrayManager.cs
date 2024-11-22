using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PrayManager : MonoBehaviour
{
    [SerializeField] private GameObject PrayUI;

    [SerializeField] private TMP_Text tree_Lv_Text;
    [SerializeField] private TMP_Text dew_Cost_Text;
    public UZZ_DataTable UZZ_DataTable;
    public Pray[] prays;


    [SerializeField] private GameObject VFX_LVUP;
    [SerializeField] private GameObject VFX_LVUP_5;
    [SerializeField] private Transform VFX_SpawnPoint;
    [System.Serializable]
    public struct Pray
    {
        public int index;
        public int tree_Level;
        public int pray_Cost_Dew;
        public int pray_Flower;
        public int pray_Dew;

    }
    private void Awake()
    {
        Debug.Log(UZZ_DataTable.PrayTable.Count);
        for (int i = 0; i < UZZ_DataTable.PrayTable.Count; ++i)
        {
            prays[i].index = UZZ_DataTable.PrayTable[i].index;
            prays[i].tree_Level = UZZ_DataTable.PrayTable[i].tree_Level;
            prays[i].pray_Cost_Dew = UZZ_DataTable.PrayTable[i].pray_Cost_Dew;
            prays[i].pray_Flower = UZZ_DataTable.PrayTable[i].pray_Flower;
            prays[i].pray_Dew = UZZ_DataTable.PrayTable[i].pray_Dew;
        }


    }

    private void Refresh()
    {
        tree_Lv_Text.text = "µµ±úºñ ³ª¹« Lv" + GameManager.Instance.treeLv + " -> Lv" + (GameManager.Instance.treeLv + 1);
        dew_Cost_Text.text = prays[GameManager.Instance.treeLv].pray_Cost_Dew.ToString();
    }
    public void OnUI()
    {
        Refresh();
        PrayUI.SetActive(true);

    }

    public void OffUI()
    {
        PrayUI.SetActive(false);
       
    }


    public void LvUp()
    {
        if (GameManager.Instance.treeLv >= 49)
            return;
       
        if (GameManager.Instance.dew < prays[GameManager.Instance.treeLv].pray_Cost_Dew)
            return;
        
        GameManager.Instance.treeLv++;
        GameManager.Instance.dew -= prays[GameManager.Instance.treeLv].pray_Cost_Dew;
        if(GameManager.Instance.treeLv%5==0)
        {
            Instantiate(VFX_LVUP_5, VFX_SpawnPoint);
        }
        else
        {
            Instantiate(VFX_LVUP, VFX_SpawnPoint);
        }
        
        GameManager.Instance.WindowFromPray();
        Refresh();
    }

   
}
