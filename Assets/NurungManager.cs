using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NurungManager : MonoBehaviour
{
    [SerializeField] private GameObject ui_nurung;
    [SerializeField] private GameObject ui_nurung_moneywaring;
    [SerializeField] private Text text_nurung_moneywaring;
   

    [SerializeField] private GameObject ui_nurung_makeFurniture;

    [SerializeField] private GameObject ui_nurung_result;

    [SerializeField] private GameObject ui_dust_conversation;



    [Header ("For Debug")]
    [SerializeField] private float time = 3f;


    void Start()
    {
        ui_nurung.SetActive(false);
        ui_nurung_moneywaring.SetActive(false);
        ui_nurung_makeFurniture.SetActive(false);
        ui_nurung_result.SetActive(false);
        ui_dust_conversation.SetActive(false);


    }


    void Update()
    {

    }

    public void ClickButtonMakeFurniture(int index)
    {
        ui_nurung_moneywaring.SetActive(true);
      
    }

    
    public void RequestMakeFurniture()
    {
        StartCoroutine(MakeFurniture(time));
    }

    private IEnumerator MakeFurniture(float time)
    {
        ui_nurung_moneywaring.SetActive(false);
        ui_nurung_makeFurniture.SetActive(true);

        yield return new WaitForSeconds(time);

        ui_nurung_makeFurniture.SetActive(false);
        ui_nurung_result.SetActive(true);

    }

    public void ClickButtonCloseMoneywarning()
    {
        ui_nurung_moneywaring.SetActive(false);
    }

    public void ClickButtonCheck()
    {
        ui_nurung_result.SetActive(false);
        ui_dust_conversation.SetActive(true);
    }

    public void ClickDustButtonCheck()
    {
        
        
        ui_dust_conversation.SetActive(false);
    }

    public void ClickButtonCloseNurungUI()
    {
        ui_nurung.SetActive(false);
        ui_nurung_moneywaring.SetActive(false);
        ui_nurung_makeFurniture.SetActive(false);
        ui_nurung_result.SetActive(false);
    }
}
