using UnityEngine;

public class TutoFlowerUI : MonoBehaviour
{
    [SerializeField] TutorialManager tutoManager;
    [SerializeField] GameObject vfxObject;
    [SerializeField] GameObject flowerUIVfx;
    [SerializeField] GameObject flowerUI;
    [SerializeField] GameObject flowerTutoPanel1;
    [SerializeField] GameObject flowerTutoPanel2;
    [SerializeField] GameObject tutoHighlight;

    public void FlowerNextHandler()
    {
        if (!vfxObject.activeSelf)
            return;

        vfxObject.SetActive(false);
        flowerUIVfx.SetActive(true);
        flowerTutoPanel1.SetActive(false);
        flowerTutoPanel2.SetActive(true);
        flowerUI.SetActive(true);
    }


    public void CloseFlowerUI()
    {
        flowerUIVfx.SetActive(false);
        flowerUI.SetActive(false);
        flowerTutoPanel2.SetActive(false);
        tutoHighlight.SetActive(false);
        tutoManager.OnNextDialogueHandler();
    }
}
