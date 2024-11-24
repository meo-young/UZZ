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
    public struct StoryData            // ���丮 ���������̺�
    {
        public string title;                // ���丮 ����
        public int imageIndex;              // ���丮 �̸����� �̹���
        public string description;          // ���丮 ���� �ؽ�Ʈ
        public int requiredDew;             // ���丮 �ʿ���ȭ
    }

    public struct CharacterData        // ĳ���� ���������̺�
    {
        public string height;               // Ű
        public string weight;               // ������
        public string goodInfo;             // ���ϴ� ��
        public string badInfo;              // ���ϴ� ��
        public int imageIndex;              // ĳ���� �̹���
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
    public void OnDiaryBtnHandler()         // Diary Ȱ��ȭ ���
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

    public void OnNextBtnHandler()          // Next Btn ���
    {
        currentIndex++;
        ControlBtnVisibility();
        InitDiaryInfo();
    }

    void ControlNextBtnVisibility()         // Next Btn Ȱ��ȭ ���
    {
        if (currentIndex == diaryInfo.level)
            nextBtn.SetActive(false);
        else
            nextBtn.SetActive(true);
    }

    public void OnPrevBtnHandler()          // Prev Btn ���
    {
        currentIndex--;
        ControlBtnVisibility();
        InitDiaryInfo();
    }

    void ControlPrevBtnVisibility()         // Prev Btn Ȱ��ȭ ���
    {
        if(currentIndex == 0)
            prevBtn.SetActive(false);
        else
            prevBtn.SetActive(true);
    }

    void ControlBtnVisibility()             // Btn Ȱ��ȭ ���
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

    void CheckIndex()                       // Current Index�� ���� Level�� �ʱ�ȭ
    {
        currentIndex = diaryInfo.level;
    }

    void IsAvailableAppreicate()            // ��ȭ�� ����ϸ� ��ư Ȱ��ȭ. ���ٸ� ��Ȱ��ȭ
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

    void InitDiaryInfo()                    // Current Index�� ���� Diary ���� �ʱ�ȭ
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
    void UpdateDiaryDataTable()             // DiaryDataTable �ʱ�ȭ
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
