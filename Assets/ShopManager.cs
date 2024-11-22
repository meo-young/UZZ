using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private GameObject uiShop;

    [SerializeField] private GameObject uiHarris;
    [SerializeField] private GameObject uiNurung;
    [SerializeField] private GameObject uiBomyeoreum;

    [SerializeField] private GameObject UI_Start_underBar;
    [SerializeField] private GameObject UI_Start_SideBar;

    void Start()
    {
        uiShop.SetActive(false);
        uiHarris.SetActive(false);
        uiNurung.SetActive(false);
        uiBomyeoreum.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnUIShop()
    {
        uiShop.SetActive(true);
        UI_Start_SideBar.SetActive(false);
        UI_Start_underBar.SetActive(false);
    }

    public void OffUIShop()
    {
        uiShop.SetActive(false);
        
    }

    public void ClickButtonHarris()
    {

        uiHarris.SetActive(true);
        UI_Start_SideBar.SetActive(false);
        UI_Start_underBar.SetActive(false);
    }
    public void OffButtonHarris()
    {

        uiHarris.SetActive(false);
        UI_Start_underBar.SetActive(true);
        UI_Start_SideBar.SetActive(true);
    }

    public void ClickButtonNurung()
    {

        uiNurung.SetActive(true);
        uiBomyeoreum.SetActive(false);

    }
    public void ClickPlyToNurung()
    {

       
        uiBomyeoreum.SetActive(false);

    }

    public void ClickButtonBomYeoreum()
    {

        uiBomyeoreum.SetActive(true);
       
    }
}
