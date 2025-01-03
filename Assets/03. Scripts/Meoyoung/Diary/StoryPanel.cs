using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoryPanel : MonoBehaviour
{
    private Button prevBtn;
    private Button nextBtn;
    private Button mainAppreciateBtn;

    private TMP_Text titleText;
    private Text descriptionText;
    private Text appreciationStoryTitle;

    private Image storyPreview;
    private Image appreciationStoryPreview;
    private Image outlinePanel;
    private Image apprecitaionPanel;
    private void Awake()
    {
        Button[] btns = GetComponentsInChildren<Button>();
        TMP_Text[] textsTMP = GetComponentsInChildren<TMP_Text>();
        Text[] texts = GetComponentsInChildren<Text>();
        Image[] images = GetComponentsInChildren<Image>();

        prevBtn = btns[0];
        nextBtn = btns[1];
        mainAppreciateBtn = btns[3];

        titleText = textsTMP[0];
        descriptionText = texts[0];
        appreciationStoryTitle = texts[1];

        storyPreview = images[2];
        outlinePanel = images[5];
        apprecitaionPanel = images[6];
        appreciationStoryPreview = images[7];
    }

    // 스토리 패널 활성화시 감상하기, Outline 패널 비활성화
    private void OnEnable()
    {
        OffAppreciationPanel();
    }

    // 스토리 다음 버튼 비활성화
    public void OffNextBtn()
    {
        nextBtn.enabled = false;
    }

    // 스토리 다음 버튼 활성화
    public void OnNextBtn()
    {
        nextBtn.enabled = true;
    }

    // 스토리 이전 버튼 비활성화
    public void OffPrevBtn()
    {
        prevBtn.enabled = false;
    }

    // 스토리 이전 버튼 활성화
    public void OnPrevBtn()
    {
        prevBtn.enabled = true;
    }

    // Diary 내용 초기화
    public void InitDiaryInfo(string title, Sprite sprite, string description)                    
    {
        titleText.text = title;
        storyPreview.sprite = sprite;
        descriptionText.text = description;
    }

    // 감상하기 버튼 활성화
    public void EnableAppreciationBtn()
    {
        mainAppreciateBtn.enabled = true;
    }


    // 감상하기 버튼 비활성화
    public void UnableAppreciationBtn()
    {
        mainAppreciateBtn.enabled = false;
    }

    // 스토리 감상 패널 초기화
    public void UpdateAppreciationInfo(string title, Sprite imageIndex)
    {
        appreciationStoryTitle.text = title;
        appreciationStoryPreview.sprite = imageIndex;
        outlinePanel.gameObject.SetActive(true);
        apprecitaionPanel.gameObject.SetActive(true);
    }

    // Outline, 감상하기 패널 비활성화
    public void OffAppreciationPanel()
    {
        outlinePanel.gameObject.SetActive(false);
        apprecitaionPanel.gameObject.SetActive(false);
    }
}
