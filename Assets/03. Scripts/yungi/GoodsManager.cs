using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GoodsManager : MonoBehaviour
{   
    [SerializeField] private TextMeshProUGUI dew_Text;
    [SerializeField] private TextMeshProUGUI ginko_Text;
    [SerializeField] private TextMeshProUGUI beads_Text;

    private int dew_amount;
    private int ginko_amount;
    private int beads_amount;

    void Start()
    {
        
    }

    void Update()
    {
        updateGoods();
    }

    public void updateGoods()
    {
        //�ٵ� ������ �����غ��ϱ� ��� ���� �ٲ� ���� ������Ʈ �ص� �� �� ���⵵ ��
        dew_Text.text = dew_amount.ToString();
        ginko_Text.text = ginko_amount.ToString();
        beads_Text.text = beads_amount.ToString();
    }
}
