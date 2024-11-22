using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestNoticeManager : MonoBehaviour
{
    [SerializeField] QuestManager questManager;
    [SerializeField] DialogSystem dialogSystem;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void touchQuestNotice()
    {
        dialogSystem.start_DialogSystem(100, 0, DialogSystem.TalkType.Quest);
        gameObject.SetActive(false);
    }
}
