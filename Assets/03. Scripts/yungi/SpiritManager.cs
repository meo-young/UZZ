using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritManager : MonoBehaviour
{
    [SerializeField] private DialogSystem dialogSystem;
    [SerializeField] public int id;
    [SerializeField] private string name2;

    private bool isFirstConversation; //시간대가 바뀐 후의 첫 대화인가?
    [SerializeField] private int time;
    private bool canTalk = true;
    private void OnMouseDown()
    {
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() == true || !canTalk)
            return;
        touchSpirit();
    }

    public void CanTalk(bool value)
    {
        canTalk = value;
    }

    public void touchSpirit()
    {
        //if (isFirstConversation)
        //{
        //    isFirstConversation = false;
        //    dialogSystem.start_DialogSystem(id, time, DialogSystem.TalkType.Spirit);
        //}
        //else
        //{
        //    dialogSystem.start_DialogSystem(id, 4, DialogSystem.TalkType.Spirit);
        //}
        Debug.Log("touchSpirit");
        dialogSystem.start_DialogSystem(id, time, DialogSystem.TalkType.Spirit);
    }


    public void set_FirstConversation(bool val, int _time)
    {
        isFirstConversation = val;
        time = _time;
    }
}
