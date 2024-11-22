using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectRestManager : MonoBehaviour
{
    private bool canRest; // 접속 휴식 보상을 받을수 있나? 체크


    public void CheckCanRest()
    {
        //종료시간이랑 접속시간이랑 시간 비교 하고 10분넘었으면 canRest true시키면됨

    }



    void Start()
    {
        CheckCanRest();
        if(canRest)
        {
            OnRestRewardPanel();
        }
    }

    void OnRestRewardPanel()
    {
        //보상패널켜기
    }

    void OffRestRewardPanel()
    {
        //보상패널끄기
    }

}
