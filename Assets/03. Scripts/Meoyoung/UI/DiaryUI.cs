using System.Collections.Generic;
using System.IO;
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
    public struct StoryData            // 스토리 데이터테이블
    {
        public string title;                // 스토리 제목
        public int imageIndex;              // 스토리 미리보기 이미지
        public string description;          // 스토리 설명 텍스트
        public int requiredDew;             // 스토리 필요재화
    }

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
    [SerializeField] StoryData[] storyData;
    [SerializeField] TextAsset storyDataTable;
    [SerializeField] Sprite[] storyImages;

    [Header("# Diary UI")]
    [SerializeField] GameObject diaryUI;

    [Header("# Story UI")]
    [SerializeField] GameObject storyUI;
    [SerializeField] GameObject prevBtn;
    [SerializeField] GameObject nextBtn;
    [SerializeField] Text descriptionText;
    [SerializeField] Text titleText;
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
    private float Dew => MainManager.instance.gameInfo.dew;

    private void Awake()
    {
        if(instance == null)
            instance = this;

        if(diaryUI.activeSelf)
            diaryUI.SetActive(false);

        UpdateDiaryDataTable();
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

    public void OnMainAppreciationBtnHandler()
    {
        if (diaryInfo.level == currentIndex)
        {
            MainManager.instance.gameInfo.dew -= storyData[currentIndex].requiredDew;
            diaryInfo.level++;
        }

        SceneManager.LoadScene("Story");
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


    #region DataTable
    void UpdateDiaryDataTable()             // DiaryDataTable 초기화
    {
        storyData = new StoryData[100]; 
        StringReader reader = new StringReader(storyDataTable.text);
        bool head = false;
        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            if (!head)
            {
                head = true;
                continue;
            }
            string[] values = line.Split('\t');
            storyData[int.Parse(values[0])].title = values[1];
            storyData[int.Parse(values[0])].imageIndex = int.Parse(values[2]);
            storyData[int.Parse(values[0])].description = values[3];
            storyData[int.Parse(values[0])].requiredDew = int.Parse(values[4]);
        }
    }
    #endregion
}
