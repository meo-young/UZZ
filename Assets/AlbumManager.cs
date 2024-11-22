using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlbumManager : MonoBehaviour
{
    [SerializeField] public GameObject UI_album_panel,UI_quest_pansel;

    public void OnAlbumPanel()
    {
        UI_album_panel.SetActive(true);
    }
    public void OffAlbumPanel()
    {
        UI_album_panel.SetActive(false);
    }

    public void OnQuestPanel()
    {
        UI_quest_pansel.SetActive(true);
    }
    public void OffQuestPanel()
    {
        UI_quest_pansel.SetActive(false);
    }
}
