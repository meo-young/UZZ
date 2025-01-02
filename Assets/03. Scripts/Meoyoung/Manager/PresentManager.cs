using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PresentInfo
{
    public bool presentFlag;
    public float presentTimer;
    public List<string> items;
}


public class PresentManager : MonoBehaviour
{
    public static PresentManager instance;

    [SerializeField] int presentProbabilty = 5;
    [SerializeField] float presentInterval = 120f;
    public float presentAfterAnimationSeconds = 5;

    [HideInInspector] public PresentInfo presentInfo;

    private PureController pc;
    private LoadPresentDatatable loadPresent;
    private Sprite beforehandSprite;
    private string beforehandName;
    private PresentUI presentUI;
    private List<string> notMyItems;


    private void Awake()
    {
        if (instance == null)
            instance = this;

        notMyItems = new List<string>();
        loadPresent = FindFirstObjectByType<LoadPresentDatatable>();
    }

    private void Start()
    {
        pc = MainManager.instance.pureController;
        presentUI = MainManager.instance.presentUI;

        if (presentInfo.presentTimer < presentInterval)
            pc.ChangeState(pc._walkState);

        CheckMyItems();
    }
    private void Update()
    {
        // 선물을 받은지 2분이 경과하거나, 처음 받는 상태인 경우 선물확률 체크
        // 선물확률에 걸리면 푸르는 선물 준비상태로 전이

        if (!presentInfo.presentFlag && presentInfo.presentTimer == 0)
            CheckProbabilty(presentProbabilty);
        else
            CheckPresentReactiveTime(presentInterval);
    }


    void CheckMyItems()
    {
        Debug.Log(presentInfo.items.Count);
        Debug.Log(loadPresent.presentDatas.Count);
        for (int i = 0; i < loadPresent.presentDatas.Count; i++)
        {
            notMyItems.Add(loadPresent.presentDatas[i].GetName());
        }

        for (int i = 0; i < presentInfo.items.Count; i++)
        {
            notMyItems.Remove(presentInfo.items[i]);
        }
    }

    void CheckProbabilty(int probability)
    {
        if (MainManager.instance.gameInfo.cycleFlag)
            return;

        if (!pc.CheckPureAvailable())
            return;

        int randomNumber = Random.Range(0, 100);
        if (randomNumber < probability)
        {
            int randNum = Utility.instance.GetRandomNumber(notMyItems.Count);
            beforehandSprite = loadPresent.GetPresentSprite(randNum);
            beforehandName = loadPresent.presentDatas[randNum].GetName();
            pc.ChangeState(pc._presentReadyState);
        }
        else
            pc.ChangeState(pc._walkState);


        MainManager.instance.gameInfo.cycleFlag = true;
        presentInfo.presentFlag = true;
    }

    void CheckPresentReactiveTime(float interval)
    {
        presentInfo.presentTimer += Time.deltaTime;

        if (presentInfo.presentTimer > interval)
        {
            presentInfo.presentTimer = 0f;
            presentInfo.presentFlag = false;
        }
    }

    public void SetPresentImage()
    {
        presentUI.ShowPresentUI(beforehandSprite, beforehandName);
        presentInfo.items.Add(beforehandName);
        notMyItems.Remove(beforehandName);
    }
}
