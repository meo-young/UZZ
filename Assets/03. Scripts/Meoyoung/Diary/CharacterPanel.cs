using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPanel : MonoBehaviour
{
    [SerializeField] GameObject presentScript;
    [SerializeField] GameObject presentScriptBackground;
    
    private LoadPresentDatatable loadPresent;

    private Text presentName;
    private TMP_Text presentScriptText;

    private void Awake()
    {
        loadPresent = FindFirstObjectByType<LoadPresentDatatable>();
        presentName = presentScript.GetComponentInChildren<Text>();
        presentScriptText = presentScript.GetComponentInChildren<TMP_Text>();
    }


    private void OnEnable()
    {
        if(presentScript.activeSelf)
            presentScript.SetActive(false);

        if(presentScriptBackground.activeSelf)
            presentScriptBackground.SetActive(false);
    }

    public void ShowPresentScript(int index)
    {
        presentScript.SetActive(true);
        presentName.text = loadPresent.presentDatas[index].GetName();
        presentScriptText.text = loadPresent.presentDatas[index].GetText();
        presentScriptBackground.SetActive(true);
    }

    public void HidePresentScript()
    {
        presentScript.SetActive(false);
        presentScriptBackground.SetActive(false);
    }
}
