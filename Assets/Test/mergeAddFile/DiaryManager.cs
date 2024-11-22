using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiaryManager : MonoBehaviour
{
    public enum Category
    {
        Mainstory,
        Sidestory,
        DailyDiary,
        Collection
    }
    [SerializeField] private GameObject uiDiary;

    [SerializeField] private GameObject[] buttonUnchecked;
    [SerializeField] private GameObject[] buttonChecked;
    [SerializeField] private GameObject[] diaryUIs;

    [Space(10f)] 
    [SerializeField] private GameObject ui_moneywarning;

    private void Start()
    {
        ClickButton((int)Category.Mainstory);
        ui_moneywarning.SetActive(false);
    }

    public void OnUIDiary()
    {
        uiDiary.SetActive(true);
    }

    public void OffUIDiary()
    {
        uiDiary.SetActive(false);
    }

    public void ClickButton(int num)
    {
        TurnOffButtonChecked();
        TurnOffAllDiaryUIs();
        TurnOnButtonUnchecked();

        buttonChecked[num].SetActive(true);
        buttonUnchecked[num].SetActive(false);
        diaryUIs[num].SetActive(true);

    }

    public void ClickPlayButton()
    {
        ui_moneywarning.SetActive(true);
    }

    public void ClickExitButton()
    {
        ui_moneywarning.SetActive(false);
    }

    private void TurnOffButtonChecked()
    {
        for (int i = 0;  i < buttonChecked.Length; i++) 
        {
            buttonChecked[i].SetActive(false);
        }
    }

    private void TurnOffAllDiaryUIs()
    {
        for (int i = 0; i < diaryUIs.Length; i++)
        {
            diaryUIs[i].SetActive(false);
        }
    }

    private void TurnOnButtonUnchecked()
    {
        for (int i = 0; i < buttonUnchecked.Length; i++) 
        {
            buttonUnchecked[i].SetActive(true);
        }
    }
    




}
