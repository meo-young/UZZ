using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class DiaryInfo
{
    public int level;
}

public class DiaryUI : MonoBehaviour
{
    public static DiaryUI instance;

    public struct CharacterData        // 캐릭터 데이터테이블
    {
        public string height;               // 키
        public string weight;               // 몸무게
        public string goodInfo;             // 잘하는 것
        public string badInfo;              // 못하는 것
        public int imageIndex;              // 캐릭터 이미지
    }

    [Header("# Diary Info")]
    public DiaryInfo diaryInfo;
    [SerializeField] Sprite[] storyImages;

    [Header("# Diary UI")]
    [SerializeField] GameObject diaryUI;

    [Header("# Story UI")]
    [SerializeField] GameObject storyUI;
    [SerializeField] GameObject prevBtn;
    [SerializeField] GameObject nextBtn;
    [SerializeField] Text descriptionText;
    [SerializeField] TMP_Text titleText;
    [SerializeField] Image storyPreviewImage;
    [SerializeField] GameObject subAppreciateBtn;

    [Header("# Character UI")]
    [SerializeField] GameObject characterUI;
    [SerializeField] Image characterImage;
    [SerializeField] Text heightText;
    [SerializeField] Text weightText;
    [SerializeField] Text goodInfoText;
    [SerializeField] Text badInfoText;

    [Header("# Character Info")]
    [SerializeField] Sprite[] characterImages;


    [Header("# Appreciation UI")]
    [SerializeField] GameObject apprecitaionPanel;
    [SerializeField] Text appreciationStoryTitle;
    [SerializeField] Image appreciationStoryPreviewImage;
    [SerializeField] GameObject mainAppreciateBtn;
    [SerializeField] GameObject outlinePanel;

    private int currentIndex;
    private LoadDiaryDatatable loadDiary;
    private StoryData[] storyData => loadDiary.storyData;

    private float Dew => MainManager.instance.gameInfo.dew;
    private void Awake()
    {
        if(instance == null)
            instance = this;

        if(diaryUI.activeSelf)
            diaryUI.SetActive(false);

        loadDiary = GetComponent<LoadDiaryDatatable>();
    }
    #region DiaryFunction
    public void OnDiaryBtnHandler()         // Diary 활성화 기능
    {
        if (diaryUI.activeSelf)
            diaryUI.SetActive(false);
        else if(!diaryUI.activeSelf)
        {
            diaryUI.SetActive(true);
            CheckIndex();
            ControlBtnVisibility();
            InitDiaryInfo();
        }

        if(!storyUI.activeSelf)
            storyUI.SetActive(true);

        if(characterUI.activeSelf)
            characterUI.SetActive(false);
    }
    #endregion

    #region Story Function
    public void OnStoryBtnHandler()
    {
        if(characterUI.activeSelf)
            characterUI.SetActive(false);

        if(!storyUI.activeSelf)
            storyUI.SetActive(true);
    }

    public void OnNextBtnHandler()          // Next Btn 기능
    {
        currentIndex++;
        ControlBtnVisibility();
        InitDiaryInfo();
    }

    void ControlNextBtnVisibility()         // Next Btn 활성화 기능
    {
        if (currentIndex == diaryInfo.level)
            nextBtn.SetActive(false);
        else
            nextBtn.SetActive(true);
    }

    public void OnPrevBtnHandler()          // Prev Btn 기능
    {
        currentIndex--;
        ControlBtnVisibility();
        InitDiaryInfo();
    }

    void ControlPrevBtnVisibility()         // Prev Btn 활성화 기능
    {
        if(currentIndex == 0)
            prevBtn.SetActive(false);
        else
            prevBtn.SetActive(true);
    }

    void ControlBtnVisibility()             // Btn 활성화 기능
    {
        ControlPrevBtnVisibility();
        ControlNextBtnVisibility();        
    }

    public void OnSubAppreciationBtnHandler()
    {
        appreciationStoryTitle.text = storyData[currentIndex].title;
        appreciationStoryPreviewImage.sprite = storyImages[storyData[currentIndex].imageIndex];
        outlinePanel.SetActive(true);
        apprecitaionPanel.SetActive(true);
    }

    public void DeactiveAppreciationPanel()
    {
        outlinePanel.SetActive(false);
        apprecitaionPanel.SetActive(false);
    }

    // 감상하기 버튼
    public void OnMainAppreciationBtnHandler()
    {
        if (diaryInfo.level == currentIndex)
        {
            MainManager.instance.gameInfo.dew -= storyData[currentIndex].requiredDew;
            diaryInfo.level++;
        }
        PlayerPrefs.SetInt("Plus", storyData[currentIndex].result);
        SceneManager.LoadScene("Story_1");
    }

    void CheckIndex()                       // Current Index를 현재 Level로 초기화
    {
        currentIndex = diaryInfo.level;
    }

    void IsAvailableAppreicate()            // 재화가 충분하면 버튼 활성화. 없다면 비활성화
    {
        if(diaryInfo.level !=  currentIndex)
        {
            mainAppreciateBtn.GetComponent<Button>().enabled = true;
            return;
        }

        if(Dew < storyData[currentIndex].requiredDew)
        {
            mainAppreciateBtn.GetComponent<Button>().enabled = false;
        }
        else
        {
            mainAppreciateBtn.GetComponent<Button>().enabled = true;
        }
    }

    void InitDiaryInfo()                    // Current Index에 따라 Diary 내용 초기화
    {
        titleText.text = storyData[currentIndex].title;
        storyPreviewImage.sprite = storyImages[storyData[currentIndex].imageIndex];
        descriptionText.text = storyData[currentIndex].description;
        IsAvailableAppreicate();
    }

    #endregion

    #region Character Function
    public void OnCharacterBtnHandler()
    {
        if (!characterUI.activeSelf)
            characterUI.SetActive(true);

        if(storyUI.activeSelf)
            storyUI.SetActive(false);
    }


    #endregion

}
