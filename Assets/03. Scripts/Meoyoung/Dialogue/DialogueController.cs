using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static DialogueManager;

public class DialogueController : MonoBehaviour
{
    [Header("# Fade Speed")]
    [SerializeField] float fadeSpeed = 2f;

    [Header("# Next Scene")]
    [SerializeField] string nextSceneName = "Main";

    [Header("# Dialogue UI Info")]
    [SerializeField] Image blackImage;
    [SerializeField] SpriteRenderer[] popUpImages;
    [SerializeField] GameObject[] backgroundImages;
    [SerializeField] GameObject firstChoicePanel;
    [SerializeField] GameObject secondChoicePanel;
    [SerializeField] Text firstChoiceText;
    [SerializeField] Text secondChoiceText;
    [SerializeField] Text dialogueText;
    [SerializeField] Text nameText;
    [SerializeField] Button dialoguePanelBtn;
    [SerializeField] GameObject nextBtn;

    private DialogueInfo[] dialogues => DialogueManager.instance.dialogueInfos;
    private int currentIndex;
    private void Awake()
    {
        OffChoicePanel();
    }


    private void Start()
    {
        currentIndex = 0;
        ChangeDialogue();
    }

    #region Choice Function
    public void OnFirstChoiceHandler()
    {
        currentIndex = dialogues[currentIndex].nextIndex1;
        ChangeDialogue();
    }

    public void OnSecondChoiceHandler()
    {
        OffChoicePanel();

        if (dialogues[currentIndex].nextIndex2 == 0)
        {
            currentIndex = dialogues[currentIndex].nextIndex1;
            ChangeDialogue();
        }
        else
        {
            currentIndex = dialogues[currentIndex].nextIndex2;
            ChangeDialogue();
        }
    }
    void OnChoicePanel()
    {
        OffChoicePanel();

        if (!firstChoicePanel.activeSelf)
            firstChoicePanel.SetActive(true);

        if (!secondChoicePanel.activeSelf)
        {
            if (dialogues[currentIndex].dialogue2 != "")
                secondChoicePanel.SetActive(true);
        }
    }
    void OffChoicePanel()
    {
        if (firstChoicePanel.activeSelf)
            firstChoicePanel.SetActive(false);

        if (secondChoicePanel.activeSelf)
            secondChoicePanel.SetActive(false);
    }
    #endregion
    public void OnNextBtnHandler()
    {
        currentIndex++;
        ChangeDialogue();
    }

    void CheckDialogueType()
    {
        switch (dialogues[currentIndex].type)
        {
            case DialogueManager.Type.Choice:
                nextBtn.SetActive(false);
                dialoguePanelBtn.enabled = false;
                break;
            case DialogueManager.Type.Skip:
                nextBtn.SetActive(false);
                dialoguePanelBtn.enabled = false;
                SetTransition();
                return;
            case DialogueManager.Type.Dialog:
                nextBtn.SetActive(true);
                dialoguePanelBtn.enabled = true;
                break;
        }
    }

    void ChangeDialogue()
    {
        CheckSound();
        ChangeBackgroundImage();
        CheckDialogueType();
        CheckLastDialogue();
        SetName();

        if (dialogues[currentIndex].type == DialogueManager.Type.Choice)
        {
            firstChoiceText.text = dialogues[currentIndex].dialogue1;
            secondChoiceText.text = dialogues[currentIndex].dialogue2;

            OnChoicePanel();
        }
        else
        {
            OffChoicePanel();
            dialogueText.text = dialogues[currentIndex].dialogue1;
        }
    }

    void SetName()
    {
        nameText.text = dialogues[currentIndex].name;
    }

    void ChangeBackgroundImage()
    {
        InitBackgroundImage();
        backgroundImages[dialogues[currentIndex].backgroundIndex].SetActive(true);
    }

    void InitBackgroundImage()
    {
        for(int i=0; i<backgroundImages.Length; i++)
        {
            backgroundImages[i].SetActive(false);
        }
    }

    void SetTransition()
    {
        switch (dialogues[currentIndex].transition)
        {
            case 0: // FadeIn_B
                StartCoroutine(FadeIn(blackImage, fadeSpeed, OnNextBtnHandler));
                break;
            case 1: // FadeOut_B
                StartCoroutine(FadeOut(blackImage, fadeSpeed, OnNextBtnHandler));
                break;
            case 4: // Dissolve
                backgroundImages[dialogues[currentIndex - 1].backgroundIndex].SetActive(true);
                StartCoroutine(FadeOut(backgroundImages[dialogues[currentIndex].backgroundIndex].GetComponent<SpriteRenderer>(), fadeSpeed, OnNextBtnHandler));
                break;
            case 5: // Popup
                StartCoroutine(FadeOut(popUpImages[dialogues[currentIndex].backgroundIndex], fadeSpeed, OnNextBtnHandler));
                break;
            case 6: // PopOut
                StartCoroutine(FadeIn(popUpImages[dialogues[currentIndex].backgroundIndex], fadeSpeed, OnNextBtnHandler));
                break;
        }
    }

    void CheckLastDialogue()
    {
        if (currentIndex == 0)
            return;

        if (dialogues[currentIndex-1].nextIndex1 == -1)
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }

    void CheckSound()
    {
        StorySound.instance.PlaySE(dialogues[currentIndex].se);
        StorySound.instance.PlaySFX(dialogues[currentIndex].ambience);
    }

    #region Fade
    IEnumerator FadeOut(Image _img, float _speed, Action callback)
    {
        Color color = _img.color;
        color.a = 0f;

        float elapsedTime = 0f;

        while (elapsedTime < _speed)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, elapsedTime / _speed);
            _img.color = color;
            yield return null;
        }

        color.a = 1f;
        _img.color = color;

        callback();
    }

    IEnumerator FadeIn(Image _img, float _speed, Action callback)
    {
        Color color = _img.color;
        color.a = 1f;

        float elapsedTime = 0f;
        while (elapsedTime < _speed)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, elapsedTime / _speed);
            _img.color = color;
            yield return null;
        }

        color.a = 0f;
        _img.color = color;

        callback();
    }

    IEnumerator FadeOut(SpriteRenderer _img, float _speed, Action callback)
    {
        Color color = _img.color;
        color.a = 0f;

        float elapsedTime = 0f;

        while (elapsedTime < _speed)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, elapsedTime / _speed);
            _img.color = color;
            yield return null;
        }

        color.a = 1f;
        _img.color = color;

        callback();
    }

    IEnumerator FadeIn(SpriteRenderer _img, float _speed, Action callback)
    {
        Color color = _img.color;
        color.a = 1f;

        float elapsedTime = 0f;
        while (elapsedTime < _speed)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, elapsedTime / _speed);
            _img.color = color;
            yield return null;
        }

        color.a = 0f;
        _img.color = color;

        callback();
    }
    #endregion
}
