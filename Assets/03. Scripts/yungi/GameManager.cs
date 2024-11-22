using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // �̱��� ������ ����ϱ� ���� �ν��Ͻ� ����
    private static GameManager _instance;

    public int marble;
    public int leaf;
    public int treeLv=0;
    public int petal;
    public int dew;
    public int bead;
    public int score;
    public OgeulaeMiniGame OgeulaeMiniGame;
    public RewardManager RewardManager;

    [SerializeField]
    private TMP_Text petalT;
    [SerializeField]
    private TMP_Text dewT;
    [SerializeField]
    private TMP_Text beadT;

    [SerializeField]
    private TMP_Text scoreT;


    [SerializeField] GameObject[] tree_Window_Change;
    // �ν��Ͻ��� �����ϱ� ���� ������Ƽ

    [SerializeField] private GameObject ui_Start;


  
    private void Start()
    {
        
    }

    public void WindowFromPray()
    {
        for (int i = 0; i < tree_Window_Change.Length; i++)
        {
            tree_Window_Change[i].SetActive(false);
        }

        tree_Window_Change[treeLv / 5].SetActive(true);
        
    }
        
    
    public static GameManager Instance
    {
        get
        {
            // �ν��Ͻ��� ���� ��쿡 �����Ϸ� �ϸ� �ν��Ͻ��� �Ҵ����ش�.
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;

                if (_instance == null)
                    Debug.Log("no Singleton obj");
            }
            return _instance;
        }
    }

    public void Catch(int score)
    {

        this.score += score;
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        // �ν��Ͻ��� �����ϴ� ��� ���λ���� �ν��Ͻ��� �����Ѵ�.
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        // �Ʒ��� �Լ��� ����Ͽ� ���� ��ȯ�Ǵ��� ����Ǿ��� �ν��Ͻ��� �ı����� �ʴ´�.
        DontDestroyOnLoad(gameObject);
    }


    private void FixedUpdate()
    {
        petalT.text = petal.ToString();
        dewT.text = dew.ToString();
        beadT.text = bead.ToString();

        scoreT.text = score.ToString();
    }

    public void OnOffUIStart(bool value)
    {
        ui_Start.SetActive(value);
    }
}


