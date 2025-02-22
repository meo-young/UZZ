using TMPro;
using Unity.VisualScripting;
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

    [Header("# 유리구슬")]
    [SerializeField] GameObject glassFront;
    [SerializeField] GameObject glassBehind;
    [SerializeField] GameObject glassPanel;

    [Header("# Equipment Contents")]
    [SerializeField] Image[] icons;
    [SerializeField] TMP_Text[] steps, levels;
    [SerializeField] Text[] btnLevels;
    [SerializeField] TMP_Text[] points;
    [SerializeField] TMP_Text[] btn_Prices;
    [SerializeField] GameObject[] lockImages;
    [SerializeField] Transform[] buttonPos;

    [Header("# Dew Contents")]
    [SerializeField] GameObject[] dewImages;
    [SerializeField] TMP_Text dewLevel;
    [SerializeField] TMP_Text dewPrice;
    [SerializeField] GameObject dewBtn;


    private void Awake()
    {
        this.gameObject.transform.localScale = Vector3.zero;
    }

    private void OnEnable()
    {
        OnEquipmentBtnHandler();
    }
    public void OnWorkShopCanvasHandler()
    {
        if(transform.localScale == Vector3.zero)
            transform.localScale = Vector3.one;
        else
            transform.localScale = Vector3.zero;
    }

    #region Equipment
    public void OnEquipmentBtnHandler()
    {
        equipFront.SetActive(true);
        equipBehind.SetActive(false);
        equipPanel.SetActive(true);
        dewFront.SetActive(false);
        dewBehind.SetActive(true);
        dewPanel.SetActive(false);
        glassFront.SetActive(false);
        glassBehind.SetActive(true);
        glassPanel.SetActive(false);
    }

    public void OnEquipmentPurchaseBtnHandler(int _index)
    {
        MainManager.instance.fieldWorkManager.FieldWorkLevelUp(_index);
    }

    public void ShowEquipmentItems(FieldWork[] _fieldWork, Sprite[] _icons, int[] _prices)
    {
        for(int i=0; i<icons.Length; i++)
        {
            // ������ 0�̸� ���
            if (_fieldWork[i].level == 0 && _fieldWork[i-1].level < 10)
            {
                lockImages[i].SetActive(true);
                continue;
            }

            // ������ 1�̻��̸� �������
            lockImages[i].SetActive(false);

            // ������ ���� ������ �Ҵ�
            icons[i].sprite = _icons[i];
            levels[i].text = "Lv" + _fieldWork[i].level.ToString();
            steps[i].text = (_fieldWork[i].step + 1).ToString() + "�ܰ�";
            points[i].text = _fieldWork[i].dewPoint.ToString();
            btnLevels[i].text = "Lv" + (_fieldWork[i].level+1).ToString();
            btn_Prices[i].text = _prices[i].ToString();
        }
    }


    public Transform GetFieldWorkButtonPos(int _index)
    {
        return buttonPos[_index];
    }

    #endregion

    #region Dew
    public void OnDewBtnHandler()
    {
        equipFront.SetActive(false);
        equipBehind.SetActive(true);
        equipPanel.SetActive(false);
        dewFront.SetActive(true);
        dewBehind.SetActive(false);
        dewPanel.SetActive(true);
        glassFront.SetActive(false);
        glassBehind.SetActive(true);
        glassPanel.SetActive(false);
    }

    public void UpdateDewInfo(int _index, int _level, int _price)
    {
        for(int i=0; i<dewImages.Length; i++)
            dewImages[i].SetActive(false);

        dewImages[_index].SetActive(true);
        dewLevel.text = _level.ToString();
        dewPrice.text = _price.ToString();
    }

    public void UnableDewBtn()
    {
        dewBtn.SetActive(false);
    }

    public void OnDewPurchaseBtnHandler()
    {
        AutoGrowManager.instance.AutoGrowLevelUp();
    }
    #endregion

    public void OnGlassBtnHandler()
    {
        equipFront.SetActive(false);
        equipBehind.SetActive(true);
        equipPanel.SetActive(false);
        dewFront.SetActive(false);
        dewBehind.SetActive(true);
        dewPanel.SetActive(false);
        glassFront.SetActive(true);
        glassBehind.SetActive(false);
        glassPanel.SetActive(true);
    }
}
