using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class AutoGrowInfo
{
    public int level;
}

public class AutoGrowManager : MonoBehaviour
{
    public static AutoGrowManager instance;

    public AutoGrowInfo autoGrowInfo;
    [SerializeField] LoadAutoGrowthData loadAutoGrow;
    [SerializeField] WorkShopUI workShopUI;

    private float counter;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        counter = 0;
    }

    private void Start()
    {
        workShopUI.UpdateDewInfo(loadAutoGrow.autoGrowData[autoGrowInfo.level].imageIndex, autoGrowInfo.level + 1, loadAutoGrow.autoGrowData[autoGrowInfo.level + 1].price);

        if (loadAutoGrow.autoGrowData[autoGrowInfo.level + 1].price == 0)
            workShopUI.UnableDewBtn();

        GetOfflineGrowth(Utility.instance.GetIntervalDateTime());
    }

    private void Update()
    {
        counter += Time.deltaTime;

        if (counter > 1)
        {
            counter = 1;
            GetOnlineGrowth(counter);

            counter = 0;
        }
    }

    void GetOnlineGrowth(float _multiflier)
    {
        MainManager.instance.flowerManager.GetFlowerExp(loadAutoGrow.autoGrowData[autoGrowInfo.level].growthProduced * _multiflier, -1);
    }

    void GetOfflineGrowth(float _multiflier)
    {
        MainManager.instance.flowerManager.GetFlowerExp(loadAutoGrow.autoGrowData[autoGrowInfo.level].outGrowthProduced * _multiflier, -1);
    }

    public void AutoGrowLevelUp()
    {
        if (MainManager.instance.gameInfo.dew < loadAutoGrow.autoGrowData[autoGrowInfo.level + 1].price)
            return;

        MainManager.instance.dewUI.Count(-loadAutoGrow.autoGrowData[autoGrowInfo.level+1].price);
        autoGrowInfo.level++;
        workShopUI.UpdateDewInfo(loadAutoGrow.autoGrowData[autoGrowInfo.level].imageIndex, autoGrowInfo.level + 1, loadAutoGrow.autoGrowData[autoGrowInfo.level + 1].price);

        if (loadAutoGrow.autoGrowData[autoGrowInfo.level + 1].price == 0)
            workShopUI.UnableDewBtn();
    }


}
