using Spine;
using UnityEngine;

public class Sprout : MonoBehaviour
{
    [SerializeField] float nextDialogueTime;
    [SerializeField] TutorialManager tutoManager;
    [SerializeField] GameObject flowerObject;
    [SerializeField] GameObject flower2;
    [SerializeField] TutorialCamera cam;
    [SerializeField] GameObject uiCanvas;

    private bool eventFlag = false;
    private float counter;

    private void OnEnable()
    {
        counter = 0;
    }

    private void Update()
    {
        if (eventFlag)
            return;

        counter += Time.deltaTime;
        if(counter > nextDialogueTime)
        {
            eventFlag = true;
            uiCanvas.SetActive(true);
            this.gameObject.SetActive(false);
            if(flower2.activeSelf)
                flower2.SetActive(false);
            flowerObject.SetActive(true);
            cam.InitCamera();
            tutoManager.OnNextDialogueHandler();
        }
    }

}
