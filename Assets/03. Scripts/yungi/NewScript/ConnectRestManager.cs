using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectRestManager : MonoBehaviour
{
    private bool canRest; // ���� �޽� ������ ������ �ֳ�? üũ


    public void CheckCanRest()
    {
        //����ð��̶� ���ӽð��̶� �ð� �� �ϰ� 10�гѾ����� canRest true��Ű���

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
        //�����г��ѱ�
    }

    void OffRestRewardPanel()
    {
        //�����гβ���
    }

}
