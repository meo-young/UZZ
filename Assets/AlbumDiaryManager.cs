using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlbumDiaryManager : MonoBehaviour
{
    [SerializeField] public GameObject UI_albumdiary_panel;

    public void OnAlbumDiaryPanel()
    {
        UI_albumdiary_panel.SetActive(true);
    }
    public void OffAlbumDiaryPanel()
    {
        UI_albumdiary_panel.SetActive(false);
    }
}
