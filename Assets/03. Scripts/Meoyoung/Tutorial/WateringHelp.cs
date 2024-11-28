using UnityEngine;

public class WateringHelp : MonoBehaviour
{
    [SerializeField] GameObject vfxObject;
    [SerializeField] GameObject workPanel5;
    [SerializeField] GameObject tutoHighlight;
    [SerializeField] GameObject pure;
    [SerializeField] TutorialManager tutoManager;

    public void DeactiveObejct()
    {
        if (!vfxObject.activeSelf)
            return;

        vfxObject.SetActive(false);
        workPanel5.SetActive(false);
        tutoManager.OnNextDialogueHandler();
        tutoHighlight.SetActive(false);
        pure.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
