using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresentUI : MonoBehaviour
{
    private PureController pc;
    private void Start()
    {
        pc = MainManager.instance.pureController;
    }
    public void OnClickReceiveBtn()
    {
        Debug.Log("받기 버튼을 클릭했습니다");
        pc.ChangeState(pc._walkState);
        this.gameObject.SetActive(false);
    }
}
