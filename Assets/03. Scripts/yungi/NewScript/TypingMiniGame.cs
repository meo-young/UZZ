using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypingMiniGame : MonoBehaviour
{
    private int tC;

    private int clearCount;

    private Touch tempTouchs;
    private Vector3 touchedPos;
    private bool touchOn;
   

    private void Update()
    {
        touchOn = false;
        if (Input.touchCount>0)
        {


            for (int i = 0; i < Input.touchCount; i++)
            {

                tempTouchs = Input.GetTouch(i);
                if (tempTouchs.phase == TouchPhase.Began)
                {
                    tC++;

                    break;
                }
            }
        }
        Debug.Log(tC);

        if(tC>30)
        {
            tC = 0;
            clearCount++;
            //이미지바뀌게하기
            
        }
        if (clearCount >= 3)
        {
            clearCount = 0;
            Clear();
            
        }

    }

    void Clear()
    {
        //보상 UI추가
    }
}
