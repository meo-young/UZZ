using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MailboxManager : MonoBehaviour
{
    //[SerializeField] QuestManager questManager;
    [SerializeField] GameObject ui_letter_background;
    [SerializeField] GameObject ui_letter;
    [SerializeField] TextMeshProUGUI text_letter_pad;
    [SerializeField] TextMeshProUGUI text_letter_button;

    [Header("������Ÿ�Կ��� �̷��� ������ �ʿ��ϴ�")]
    public int num_letter;
    public string[] data_letter_pads;
    public string[] data_letter_button;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void touchMailbox()
    {
        ui_letter_background.SetActive(true);
    }

    public void offUI_letter_background()
    {
        ui_letter_background.SetActive(false);
    }

    public void offUI_letter()
    {
        ui_letter.SetActive(false);
    }

    public void onUI_letter()
    {
        //�����, ������Ÿ�� �� �Լ��Դϴ�~
        ui_letter.SetActive(true);
    }

    public void touchButton(int index)
    {
        ui_letter.SetActive(true);
        text_letter_pad.text = data_letter_pads[index];
        text_letter_button.text = data_letter_button[index];
    }

}
