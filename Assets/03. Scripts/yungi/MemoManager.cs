using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoManager : MonoBehaviour
{
    [SerializeField] private GameObject memoPanel;


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void openMemoUI()
    {
        memoPanel.SetActive(true);
    }

    public void closeMemoUI()
    {
        memoPanel.SetActive(false);
    }
}
