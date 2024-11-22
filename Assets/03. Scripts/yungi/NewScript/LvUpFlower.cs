using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LvUpFlower : MonoBehaviour
{
    public Flower flower;
    // Start is called before the first frame update
    public TMP_Text flower_Level_Text;
    public TMP_Text dew_Cost_Text;

    [SerializeField] private GameObject VFX_LVUP;
    public Transform LV;
    private void OnEnable()
    {
        Init();
    }
    public void Init()
    {
        flower_Level_Text.text = "单捞瘤 采 Lv" + flower.flowerLv + " -> Lv" + (flower.flowerLv + 1);
        dew_Cost_Text.text = 0 + " / " + GameManager.Instance.dew;
    }
    public void LvBtn()
    {

        Instantiate(VFX_LVUP, LV);
        flower.flowerLv++;
        flower.maxFlower = 100 * flower.flowerLv;
        flower_Level_Text.text = "单捞瘤 采" + "Lv" + flower.flowerLv + " -> Lv" + (flower.flowerLv + 1);
        dew_Cost_Text.text = 0 + " / " + GameManager.Instance.dew;
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
