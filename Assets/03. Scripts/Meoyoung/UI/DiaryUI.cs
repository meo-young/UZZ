using UnityEngine;

public class DiaryUI : MonoBehaviour
{
    [SerializeField] GameObject diaryUI;

    public void OnDiaryBtnHandler()
    {
        diaryUI.SetActive(!diaryUI.activeSelf);
    }
}
