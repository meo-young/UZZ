using UnityEngine;

public class TutoShop : MonoBehaviour
{
    [SerializeField] TutorialManager tutoManager;
    [SerializeField] GameObject vfxObject;
    [SerializeField] GameObject shopLetterBox;
    [SerializeField] GameObject shopPanel;
    [SerializeField] GameObject tutoHighlight;

    public void ShowShopPanel()
    {
        if (!vfxObject.activeSelf)
            return;

        vfxObject.SetActive(false);

        if(!shopPanel.activeSelf)
            shopPanel.SetActive(true);

        if(shopLetterBox.activeSelf)
            shopLetterBox.SetActive(false);
    }

    public void ClickUpgradeBtn()
    {
        if(tutoHighlight.activeSelf)
            tutoHighlight.SetActive(false);

        if(shopPanel.activeSelf)
            shopPanel.SetActive(false);

        tutoManager.OnNextDialogueHandler();
    }
}
