using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 싱글톤 패턴을 사용하기 위한 인스턴스 변수
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
    // 인스턴스에 접근하기 위한 프로퍼티

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
            // 인스턴스가 없는 경우에 접근하려 하면 인스턴스를 할당해준다.
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
        // 인스턴스가 존재하는 경우 새로생기는 인스턴스를 삭제한다.
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        // 아래의 함수를 사용하여 씬이 전환되더라도 선언되었던 인스턴스가 파괴되지 않는다.
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


