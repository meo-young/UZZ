using UnityEngine;

public class TutoBigDust : MonoBehaviour
{
    [SerializeField] TutorialManager tutoManager;
    [SerializeField] GameObject vfxObject;
    [SerializeField] GameObject tutoHighlight;
    [SerializeField] GameObject dustPanel;

    public void BigDustNextHandler()
    {
        if (!vfxObject.activeSelf)
            return;

        vfxObject.SetActive(false);

        tutoHighlight.SetActive(false);
        dustPanel.SetActive(false);
        tutoManager.OnNextDialogueHandler();
        this.gameObject.SetActive(false);
    }
}
