using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BomYeoreumManager : MonoBehaviour
{
    [SerializeField] private GameObject uiBomYeoreum;
    [SerializeField] private GameObject uiSellConfirm;
    [SerializeField] private GameObject uiChangeFinish;

    [SerializeField] private TMP_Text itemPrice_Petal_Text;
    [SerializeField] private TMP_Text itemPrice_Dew_Text;
    [SerializeField] private TMP_Text sell_confirm_itemNameText;

    [SerializeField] private TMP_Text sell_result_Text;

    public GameObject sellSlot;
    public Item sellItem;
    public int maxCount;
    public int sellCount;

    public Image sell_confirm_itemImage;
    public TMP_Text sell_confirm_itemCountText;


    [SerializeField] private Inventory inventory;
    void Start()
    {
        uiBomYeoreum.SetActive(false);
        uiSellConfirm.SetActive(false);
    }

    public void SelectSellItem()
    {
        sellSlot = EventSystem.current.currentSelectedGameObject;
        sellItem = sellSlot.GetComponent<Slot>().item;
        maxCount = sellSlot.GetComponent<Slot>().itemCount;

        sellCount = 1;
    }

    private void LateUpdate()
    {
        sell_confirm_itemCountText.text = sellCount.ToString();
    }

    public void OffUIBomYeoreum()
    {
        uiBomYeoreum.SetActive(false);
        uiSellConfirm.SetActive(false);
    }

    public void ClickSellButton()
    {
        if(sellItem == null)
        {
            return;
        }
        uiSellConfirm.SetActive(true);
        
        sell_confirm_itemImage.sprite = sellItem.itemImage;
        sell_confirm_itemNameText.text = sellItem.itemName;


    }

    public void PlusBtn()
    {
        if (sellCount >= maxCount)
            return;
        sellCount++;
    }
    
    public void MinusBtn()
    {
        if (sellCount <= 1)
            return;
        sellCount--;
    }

    public void ClickCloseSellConfirmButton()
    {
        uiSellConfirm.SetActive(false);
        uiChangeFinish.SetActive(true);

        sellSlot.GetComponent<Slot>().SetSlotCount(-1 * sellCount);
        int reward_Dew = sellItem.itemPrice_Dew * sellCount;
        int reward_Petal = sellItem.itemPrice_Petal * sellCount;

        itemPrice_Dew_Text.text = reward_Dew.ToString();
        itemPrice_Petal_Text.text = reward_Petal.ToString();

        sell_result_Text.text = $"꽃잎 {reward_Petal}개와 이슬 {reward_Dew}개를 얻었어!";
        
    }

    public void ClickCloseChangeFinsh()
    {

        GameManager.Instance.dew = sellItem.itemPrice_Dew * sellCount;
        GameManager.Instance.petal = sellItem.itemPrice_Petal * sellCount;


        sellSlot = null;
        sellItem = null;
        uiChangeFinish.SetActive(false);
        
    }
}
