using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class DiaryInfo
{
    public int level;
}

public class DiaryManager : MonoBehaviour
{
    public static DiaryManager instance;

    public DiaryInfo diaryInfo;
    [SerializeField] Sprite[] storyImages;

    [Header("# Button")]
    [SerializeField] GameObject storyBtn;
    [SerializeField] GameObject characterBtn;

    private LoadDiaryDatatable loadDiary;
    private StoryPanel storyPanel;
    private CharacterPanel characterPanel;

    private RectTransform storyBtnRect;
    private RectTransform characterBtnRect;
    private Vector3 storyBtnCurPos;
    private Vector3 characterBtnCurPos;

    private int currentIndex;
    private string title => loadDiary.storyDatas[currentIndex].GetTitle();
    private int imageIndex => loadDiary.storyDatas[currentIndex].GetImageIndex();
    private string description => loadDiary.storyDatas[currentIndex].GetDescription();
    private int result => loadDiary.storyDatas[currentIndex ].GetResult();
    private int requiredDew => loadDiary.storyDatas[currentIndex ].GetRequiredDew();

    private void Awake()
    {
        if (instance == null)
            instance = this;

        currentIndex = 0;

        storyPanel = FindFirstObjectByType<StoryPanel>();
        characterPanel = FindFirstObjectByType<CharacterPanel>();
        loadDiary = GetComponent<LoadDiaryDatatable>();
        storyBtnRect = storyBtn.GetComponent<RectTransform>();
        characterBtnRect = characterBtn.GetComponent<RectTransform>();
        storyBtnCurPos = storyBtnRect.anchoredPosition;
        characterBtnCurPos = characterBtnRect.anchoredPosition;

        if (this.gameObject.activeSelf)
            this.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        InitializeIndex();
        ControlBtnVisibility();
        storyPanel.InitDiaryInfo(title, storyImages[imageIndex], description);

        if (diaryInfo.level == currentIndex || MainManager.instance.gameInfo.dew < loadDiary.storyDatas[currentIndex].GetRequiredDew())
            storyPanel.UnableAppreciationBtn();
        else
            storyPanel.EnableAppreciationBtn();
    }

    // 메인화면의 Diary 버튼 핸들러
    public void OnDiaryBtnHandler()         
    {
        this.gameObject.SetActive(!this.gameObject.activeSelf);

        if (characterPanel.gameObject.activeSelf)
            characterPanel.gameObject.SetActive(false);

        if (!storyPanel.gameObject.activeSelf)
            storyPanel.gameObject.SetActive(true);
    }

    // Current Index를 현재 Level로 초기화
    void InitializeIndex()                       
    {
        currentIndex = diaryInfo.level;
    }

    // 이전, 다음 버튼 활성화 컨트롤
    void ControlBtnVisibility()             
    {
        ControlPrevBtnVisibility();
        ControlNextBtnVisibility();
    }

    // 현재 스토리가 1번째 스토리면 이전 버튼 비활성화
    void ControlPrevBtnVisibility()         
    {
        if (currentIndex == 0)
            storyPanel.OffPrevBtn();
        else
            storyPanel.OnPrevBtn();
    }

    // 현재 스토리가 최근에 풀린 스토리면 다음 버튼 비활성화
    void ControlNextBtnVisibility()         
    {
        if (currentIndex == diaryInfo.level)
            storyPanel.OffNextBtn();
        else
            storyPanel.OnNextBtn();
    }

    // 스토리 패널 버튼 핸들러
    public void OnStoryBtnHandler()
    {
        if (characterPanel.gameObject.activeSelf)
            characterPanel.gameObject.SetActive(false);

        if (!storyPanel.gameObject.activeSelf)
            storyPanel.gameObject.SetActive(true);

        storyBtnRect.anchoredPosition = new Vector3(storyBtnCurPos.x, storyBtnCurPos.y + 30, storyBtnCurPos.z);
        characterBtnRect.anchoredPosition = characterBtnCurPos;
    }

    // 캐릭터 패널 버튼 핸들러
    public void OnCharacterBtnHandler()
    {
        if (!characterPanel.gameObject.activeSelf)
            characterPanel.gameObject.SetActive(true);

        if (storyPanel.gameObject.activeSelf)
            storyPanel.gameObject.SetActive(false);

        storyBtnRect.anchoredPosition = storyBtnCurPos;
        characterBtnRect.anchoredPosition = new Vector3(characterBtnCurPos.x, characterBtnCurPos.y + 30, characterBtnCurPos.z);
    }

    // 다음 버튼 누르면 다음 스토리로 이동
    public void OnNextBtnHandler()          
    {
        currentIndex++;
        ControlBtnVisibility();
        storyPanel.InitDiaryInfo(title, storyImages[imageIndex], description);
    }

    // 이전 버튼 누르면 이전 스토리로 이동
    public void OnPrevBtnHandler()          
    {
        currentIndex--;
        ControlBtnVisibility();
        storyPanel.InitDiaryInfo(title, storyImages[imageIndex], description);
    }

    // 감상하기 버튼 누르면 세부 스토리 패널 최신화
    public void OnSubAppreciationBtnHandler()
    {
        storyPanel.UpdateAppreciationInfo(title, storyImages[imageIndex]);
    }


    // 감상하기 버튼
    public void OnMainAppreciationBtnHandler()
    {
        if (diaryInfo.level == currentIndex)
        {
            MainManager.instance.gameInfo.dew -= requiredDew;
            diaryInfo.level++;
        }
        PlayerPrefs.SetInt("Plus", result);
        SceneManager.LoadScene("Story_1");
    }
}
