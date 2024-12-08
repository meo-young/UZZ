using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static DialogueManager;

public class DialogueController : MonoBehaviour
{
    [SerializeField] int currentIndex;
    [Header("# Fade Speed")]
    [SerializeField] float fadeSpeed = 3f;

    [Header("# Dialogue UI Info")]
    [SerializeField] GameObject blackImage;
    [SerializeField] List<Sprite> backgroundImgs;
    [SerializeField] GameObject[] backgroundImages;
    [SerializeField] GameObject firstChoicePanel;
    [SerializeField] GameObject secondChoicePanel;
    [SerializeField] Text firstChoiceText;
    [SerializeField] Text secondChoiceText;
    [SerializeField] Text dialogueText;
    [SerializeField] Text nameText;
    [SerializeField] Image backgroundImage;

    private DialogueInfo[] dialogues => DialogueManager.instance.dialogueInfos;

    private void Awake()
    {
        OffChoicePanel();
    }


    private void Start()
    {
        currentIndex = 0;
        ChangeDialogueText();
    }

    public void OnFirstChoiceHandler()
    {
        currentIndex = dialogues[currentIndex].nextIndex1;
        ChangeDialogueText();
    }

    public void OnSecondChoiceHandler()
    {
        OffChoicePanel();

        if (dialogues[currentIndex].nextIndex2 == 0)
        {
            currentIndex = dialogues[currentIndex].nextIndex1;
            ChangeDialogueText();
        }
        else
        {
            currentIndex = dialogues[currentIndex].nextIndex2;
            ChangeDialogueText();
        }
    }

    public void OnNextBtnHandler()
    {
        if (dialogues[currentIndex].type == Type.Choice)
            return;
        CheckLastDialogue();
        SetTransition();

        currentIndex++;
        ChangeDialogueText();
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

    void ChangeDialogueText()
    {
        ChangeBackgroundImage();

        if (dialogues[currentIndex].type == Type.Choice)
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

    void ChangeBackgroundImage()
    {
        //backgroundImage.sprite = backgroundImgs[dialogues[currentIndex].backgroundIndex];
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
            case 0:
                StartCoroutine(FadeIn(fadeSpeed));
                break;
            case 1:
                StartCoroutine(FadeOut(fadeSpeed));
                break;
        }
    }

    void CheckLastDialogue()
    {
        if (dialogues[currentIndex].nextIndex1 == -1)
        {
            SceneManager.LoadScene("Main");
        }
    }

    IEnumerator FadeOut(float _speed)
    {
        Color color = blackImage.GetComponent<Image>().color;
        color.a = 0f;

        float elapsedTime = 0f;

        while (elapsedTime < _speed)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, elapsedTime / _speed);
            blackImage.GetComponent<Image>().color = color;
            yield return null;
        }

        color.a = 1f;
        blackImage.GetComponent<Image>().color = color;
    }

    IEnumerator FadeIn(float _speed)
    {
        Color color = blackImage.GetComponent<Image>().color;
        color.a = 1f;

        float elapsedTime = 0f;
        while (elapsedTime < _speed)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, elapsedTime / _speed);
            blackImage.GetComponent<Image>().color = color;
            yield return null;
        }

        color.a = 0f;
        blackImage.GetComponent<Image>().color = color;
    }
}
