using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [System.Serializable]
    public struct DialogueInfo
    {
        public string type;
        public string text;
        public string touchType;
        public string choice1, choice2;
    }

    [Header("# Hightlight Transform List")]
    [SerializeField] GameObject hightlight;
    [Space(5)]
    [SerializeField] Transform pure;
    [SerializeField] RectTransform wateringUI;

    [Header("# Dialogue Info")]
    [SerializeField] TextAsset tutorialDatatable;
    [SerializeField] DialogueInfo[] dialogueInfo;
    [SerializeField] TMP_Text dialogueText;
    [SerializeField] GameObject dialogueObject;
    [SerializeField] GameObject tutorialHighlight;
    [Header("# Choice")]
    [SerializeField] Text choiceText1;
    [SerializeField] Text choiceText2;
    [SerializeField] GameObject choiceObject1;
    [SerializeField] GameObject choiceObject2;
    [Header("# Watering Tuto")]
    [SerializeField] GameObject wateringVFX;
    [SerializeField] GameObject wateringTutoObject;
    [SerializeField] GameObject wateringTutoObject2;
    [Header("# Shop Tuto")]
    [SerializeField] GameObject shopTutoObject;
    [SerializeField] GameObject shopVFXObject;
    [Header("# Dust")]
    [SerializeField] GameObject dustObject;
    [SerializeField] GameObject dustTutoPanel;
    [Header("# Level UP")]
    [SerializeField] GameObject levelUpVFX;
    [Header("# Flower")]
    [SerializeField] GameObject flowerVFX;
    [SerializeField] GameObject flowerTutoPanel;
    [Header("# Flower2")]
    [SerializeField] GameObject flower2TutoPanel;
    [Header("# Final")]
    [SerializeField] GameObject finalPanel1;

    private int counter;

    private void Awake()
    {
        UpdateTutorialDataTable();
        DeactiveDialoguePanel();
    }

    private void Start()
    {
        counter = 0;
    }

    public void OnNextDialogueHandler()
    {
        counter++;
        UpdateDialoue();
    }

    public string GetTouchType()
    {
        return dialogueInfo[counter].touchType;
    }

    public string GetTextType()
    {
        return dialogueInfo[counter].text;
    }

    void UpdateDialoue()
    {
        DeactiveDialoguePanel();
        DeactiveChoicePanel();
        switch (dialogueInfo[counter].type)
        {
            case "null":
                return;
            case "Choice":
                UpdateChoiceText();
                break;
            case "Lock":
                switch (dialogueInfo[counter].text)
                {
                    case "Watering":
                        if (!tutorialHighlight.activeSelf)
                            tutorialHighlight.SetActive(true);

                        if (!wateringTutoObject.activeSelf)
                            wateringTutoObject.SetActive(true);
                        break;
                    case "Shop":
                        if (!tutorialHighlight.activeSelf)
                            tutorialHighlight.SetActive(true);

                        if (!shopTutoObject.activeSelf)
                            shopTutoObject.SetActive(true);

                        if (!shopVFXObject.activeSelf)
                            shopVFXObject.SetActive(true);
                        break;
                    case "Watering2":
                        if (!tutorialHighlight.activeSelf)
                            tutorialHighlight.SetActive(true);
                        if (!wateringTutoObject2.activeSelf)
                            wateringTutoObject2.SetActive(true);
                        if (!wateringVFX.activeSelf)
                            wateringVFX.SetActive(true);
                        break;
                    case "Watering3":
                        if (!tutorialHighlight.activeSelf)
                            tutorialHighlight.SetActive(true);
                        if (!wateringVFX.activeSelf)
                            wateringVFX.SetActive(true);
                        break;
                    case "Dust":
                        if (!tutorialHighlight.activeSelf)
                            tutorialHighlight.SetActive(true);
                        dustTutoPanel.SetActive(true);
                        break;
                    case "Flower":
                        if (!tutorialHighlight.activeSelf)
                            tutorialHighlight.SetActive(true);
                        flowerTutoPanel.SetActive(true);
                        flowerVFX.SetActive(true);
                        break;
                    case "Flower2":
                        if (!tutorialHighlight.activeSelf)
                            tutorialHighlight.SetActive(true);
                        flower2TutoPanel.SetActive(true);
                        break;
                    case "Final":
                        if (!tutorialHighlight.activeSelf)
                            tutorialHighlight.SetActive(true);
                        finalPanel1.SetActive(true);
                        break;
                }
                break;
            case "Dust":
                dustObject.SetActive(true);
                dialogueText.text = dialogueInfo[counter].text;
                ActiveDialoguePanel();
                break;
            case "Flower":
                levelUpVFX.SetActive(true);
                Invoke(nameof(OnNextDialogueHandler), 5.0f);
                break;
            default:
                dialogueText.text = dialogueInfo[counter].text;
                ActiveDialoguePanel();
                break;
        }
    }
    void ActiveDialoguePanel()
    {
        if (!dialogueObject.activeSelf)
            dialogueObject.SetActive(true);
    }
    void DeactiveDialoguePanel()
    {
        if (dialogueObject.activeSelf)
            dialogueObject.SetActive(false);
    }

    void DeactiveChoicePanel()
    {
        if (choiceObject1.activeSelf)
            choiceObject1.SetActive(false);
        if (choiceObject2.activeSelf)
            choiceObject2.SetActive(false);
    }

    void UpdateChoiceText()
    {
        choiceText1.text = dialogueInfo[counter].choice1;
        choiceText2.text = dialogueInfo[counter].choice2;
        if (!choiceObject1.activeSelf)
            choiceObject1.SetActive(true);
        if (!choiceObject2.activeSelf)
            choiceObject2.SetActive(true);
    }

    void UpdateTutorialDataTable()
    {
        dialogueInfo = new DialogueInfo[100];
        StringReader reader = new StringReader(tutorialDatatable.text);
        bool head = false;
        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            if (!head)
            {
                head = true;
                continue;
            }

            string[] value = line.Split("\t");

            dialogueInfo[int.Parse(value[0])].type = value[1];
            dialogueInfo[int.Parse(value[0])].text = value[2];
            dialogueInfo[int.Parse(value[0])].touchType = value[3];
            dialogueInfo[int.Parse(value[0])].choice1 = value[4];
            dialogueInfo[int.Parse(value[0])].choice2 = value[5];
        }
    }
}
