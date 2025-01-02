using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PresentUI : MonoBehaviour
{
    private PureController pc;
    private Image presentImage;
    private TMP_Text presentName;

    private void Awake()
    {
        presentImage = GetComponentsInChildren<Image>()[2];
        presentName = GetComponentInChildren<TMP_Text>();
    }

    private void Start()
    {
        pc = MainManager.instance.pureController;

        if(this.gameObject.activeSelf)
            this.gameObject.SetActive(false);
    }
    public void OnClickReceiveBtn()
    {
        pc.ChangeState(pc._walkState);
        this.gameObject.SetActive(false);
    }

    public void ShowPresentUI(Sprite sprite, string name)
    {
        SoundManager.instance.PlaySFX(SFX.Ambience.TEXT);
        this.gameObject.SetActive(true);
        presentName.text = name;
        presentImage.sprite = sprite;
    }
}
