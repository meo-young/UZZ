using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private GameObject   questNotice;
    [SerializeField] private DialogSystem dialogSystem;
    

    void Start()
    {
        questNotice.SetActive(true);
    }

    void Update()
    {
        
    }



}
