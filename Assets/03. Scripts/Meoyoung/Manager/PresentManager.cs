using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Constant;



[System.Serializable]
public class PresentInfo
{
    public bool presentFlag;
    public float presentTimer;
    public List<int> items;
}


public class PresentManager : MonoBehaviour
{
    public static PresentManager instance;

    public PresentInfo presentInfo;

    private PureController pc;
    private LoadPresentDatatable loadPresent;
    private PresentContents presentContents;
    private Sprite beforehandSprite;
    private string beforehandName;
    private int beforehandIndex;
    private PresentUI presentUI;
    private List<int> notMyItems;


    private void Awake()
    {
        if (instance == null)
            instance = this;

        notMyItems = new List<int>();
        loadPresent = FindFirstObjectByType<LoadPresentDatatable>();
        presentContents = FindFirstObjectByType<PresentContents>();
    }

    private void Start()
    {
        pc = MainManager.instance.pureController;
        presentUI = MainManager.instance.presentUI;

        if (!MainManager.instance.gameInfo.showerFlag && presentInfo.presentTimer < PRESENT_INTERVAL)
        {
            Debug.Log("선물 확률 체크 안됨");
            pc.ChangeState(pc._walkState);
        }
        CheckMyItems();
    }
    private void Update()
    {
        // 선물을 받은지 2분이 경과하거나, 처음 받는 상태인 경우 선물확률 체크
        // 선물확률에 걸리면 푸르는 선물 준비상태로 전이

        if (!presentInfo.presentFlag && presentInfo.presentTimer == 0 && !MainManager.instance.gameInfo.showerFlag)
            CheckProbabilty(PRESENT_PROBABILITY);
        else
            CheckPresentReactiveTime(PRESENT_INTERVAL);
    }


    void CheckMyItems()
    {
        for (int i = 0; i < loadPresent.presentDatas.Count; i++)
        {
            notMyItems.Add(loadPresent.presentDatas[i].GetImageIndex());
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
            beforehandIndex = loadPresent.presentDatas[randNum].GetImageIndex();
            pc.ChangeState(pc._presentReadyState);
        }
        else
        {
            Debug.Log("선물 확률 걸리지 않음");
            pc.ChangeState(pc._walkState);
        }
            


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
        presentInfo.items.Add(beforehandIndex);
        notMyItems.Remove(beforehandIndex);
        presentContents.UpdateImages();
    }
}
