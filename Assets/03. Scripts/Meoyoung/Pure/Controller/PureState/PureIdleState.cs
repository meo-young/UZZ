﻿using UnityEngine;
using static Constant;

public class PureIdleState : MonoBehaviour, IControllerState
{
    PureController pc;
    public void OnStateEnter(PureController controller)
    {
        if (pc == null)
            pc = controller;
        GetWorkPoint();
        pc.pureAnimationSet.pureIdle.SetActive(true);
    }

    public void OnStateUpdate()
    {
        if (!MainManager.instance.gameInfo.cycleFlag)
            return;

        if (MainManager.instance.gameInfo.showerFlag)
        {
            pc.ChangeState(pc._showerState);
            return;
        }

        if (!pc.pureStat.pureInfo.autoText)
        {
            pc.autoText.ShowIdleRandomText(pc.basePos, 0);
            pc.pureStat.SetTrueAutoText();
        }
        #region LevelUp Check
        if (pc.isLevelUp)
            return;
        #endregion
        #region Cycle Check
        pc.cycleCheckTimer += Time.deltaTime;
        if (!pc.flowerEventCheckFlag && pc.cycleCheckTimer > INGAME_CYCLE_TIME)
        {
            pc.Initialize();
            pc.ChangeState(pc._walkState);
            MainManager.instance.gameInfo.cycleFlag = true;
        }
        #endregion
        #region AutoWork
        if (pc.autoWorkCheckFlag)
            return;

        if (pc.cycleCheckTimer < FIELDWORK_AUTO_CHECK_TIME)
            return;

        // 자동작업에 실패한 경우 꽃 이벤트 확인
        if(pc.fieldWorkManager.CheckAutoWorkProbability())
        {
            pc.fieldWorkState = pc.fieldWorkManager.CheckAvailableFieldWork();
            if (pc.fieldWorkState == null)
                pc.flowerEventCheckFlag = pc.flowerManager.CheckFlowerEventProbability(); // 가능한 작업이 없는 경우 꽃 이벤트 확인
            else
                pc.ChangeState(pc._workState);
        }
        else
        {
            pc.flowerEventCheckFlag = pc.flowerManager.CheckFlowerEventProbability();
        }

        pc.autoWorkCheckFlag = true;
        #endregion
    }

    public void OnStateExit()
    {
        pc.pureAnimationSet.pureIdle.SetActive(false);
    }

    void GetWorkPoint()
    {
        if (pc.fieldWorkState.type == FieldWorkType.None) // 작업을 한 상태가 아닌 경우 return
            return;

        int growPoint = (int)(pc.fieldWorkState.growPoint);

        // 정령의 힘 이벤트 시
        if(pc.spiritEventCheckFlag)
        {
            growPoint = (int)(growPoint * SPIRIT_EVENT_GROWTH_REWARD);
            pc.spiritEventCheckFlag = false;
        }

        // 광고보기 선택 시
        if(pc.spiritAdvertisementFlag)
        {
            growPoint *= 2;
            pc.spiritAdvertisementFlag = false;
        }

        if(MainManager.instance.gameInfo.mealFlag)
            growPoint = (int)(growPoint * (1 - PURE_MEAL_DISADVANTAGE));

        Instantiate(pc.vfxManager.growthVFX);
        pc.flowerManager.GetFlowerExp(growPoint);

        Instantiate(pc.vfxManager.dewVFX);
        MainManager.instance.dewUI.Count(growPoint);

        pc.Initialize();
        MainManager.instance.gameInfo.cycleFlag = false;
    }
}
