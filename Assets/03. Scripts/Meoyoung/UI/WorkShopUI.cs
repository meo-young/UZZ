using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WorkShopUI : MonoBehaviour
{
    [Header("# WorkShop")]
    [SerializeField] GameObject workShopCanvas;


    [Header("# Dew")]
    [SerializeField] GameObject dewFront;
    [SerializeField] GameObject dewBehind;
    [SerializeField] GameObject dewPanel;


    [Header("# Equipment")]
    [SerializeField] GameObject equipFront;
    [SerializeField] GameObject equipBehind;
    [SerializeField] GameObject equipPanel;

    [Header("# Equipment Contents")]
    [SerializeField] Image[] icons;
    [SerializeField] TMP_Text[] steps, levels;
    [SerializeField] Text[] btnLevels;
    [SerializeField] TMP_Text[] points;
    [SerializeField] TMP_Text[] btn_Prices;
    [SerializeField] GameObject[] lockImages;

    private void OnEnable()
    {
        OnEquipmentBtnHandler();
    }
    public void OnWorkShopCanvasHandler()
    {
        workShopCanvas.SetActive(!workShopCanvas.activeSelf);
    }

    public void OnEquipmentBtnHandler()
    {
        equipFront.SetActive(true);
        equipBehind.SetActive(false);
        equipPanel.SetActive(true);
        dewFront.SetActive(false);
        dewBehind.SetActive(true);
        dewPanel.SetActive(false);
    }

    public void OnDewBtnHandler()
    {
        equipFront.SetActive(false);
        equipBehind.SetActive(true);
        equipPanel.SetActive(false);
        dewFront.SetActive(true);
        dewBehind.SetActive(false);
        dewPanel.SetActive(true);
    }

    public void OnPurchaseBtnHandler(int _index)
    {
        MainManager.instance.fieldWorkManager.FieldWorkLevelUp(_index);
    }

    public void ShowEquipmentItems(FieldWork[] _fieldWork, Sprite[] _icons, int[] _prices)
    {
        for(int i=0; i<icons.Length; i++)
        {
            // 레벨이 0이면 잠김
            if (_fieldWork[i].level == 0 && _fieldWork[i-1].level < 10)
            {
                lockImages[i].SetActive(true);
                continue;
            }

            // 레벨이 1이상이면 잠김해제
            lockImages[i].SetActive(false);

            // 레벨에 따른 데이터 할당
            icons[i].sprite = _icons[i];
            levels[i].text = "Lv" + _fieldWork[i].level.ToString();
            steps[i].text = (_fieldWork[i].step + 1).ToString() + "단계";
            points[i].text = _fieldWork[i].dewPoint.ToString();
            btnLevels[i].text = "Lv" + (_fieldWork[i].level+1).ToString();
            btn_Prices[i].text = _prices[i].ToString();
        }
    }
}
