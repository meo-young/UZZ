using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager_lagacy : MonoBehaviour
{
    public GameObject UI_shop_panel, UI_shop_background,UI_shop_ingredient_background,
        UI_shop_furniture_background,UI_shop_selling_background,UI_itemAdd_panel,UI_itemAdd_image;

    public TestManager testManager;
    public Sprite[] itemSprite;


    public void OffUIShopPanel()
    {
        UI_shop_panel.SetActive(false);
    }

    public void OnUIShopPanel()
    {
        UI_shop_panel.SetActive(true);
    }
    
    public void OnUIShopIngredient()
    {
        UI_shop_background.SetActive(false);
        UI_shop_ingredient_background.SetActive(true);
    }

    public void OffUIShopIngredient()
    {
        UI_shop_background.SetActive(true);
        UI_shop_ingredient_background.SetActive(false);
    }

    public void OnUIShopFurniture()
    {
        UI_shop_background.SetActive(false);
        UI_shop_furniture_background.SetActive(true);
    }

    public void OffUIShopFurniture()
    {
        UI_shop_background.SetActive(true);
        UI_shop_furniture_background.SetActive(false);
    }
    
    public void OnUIShopSell()
    {
        UI_shop_background.SetActive(false);
        UI_shop_selling_background.SetActive(true);
    }

    public void OffUIShopSell()
    {
        UI_shop_background.SetActive(true);
        UI_shop_selling_background.SetActive(false);
    }

    public void OnBuyUI(int ID)
    {
        UI_itemAdd_panel.SetActive(true);
        UI_itemAdd_image.GetComponent<Image>().sprite = itemSprite[ID];
    }
    public void OffBuyUI()
    {
        UI_itemAdd_panel.SetActive(false);
    }
    public void AddItem(int Count)
    {
       OffBuyUI();
       
        
    }

    public void onBeadsUI()
    {

    }

    public void offBeadsUI()
    {

    }

}
