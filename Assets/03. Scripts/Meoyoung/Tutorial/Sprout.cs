using Spine;
using UnityEngine;

public class Sprout : MonoBehaviour
{
    [SerializeField] float nextDialogueTime;
    [SerializeField] TutorialManager tutoManager;

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
            tutoManager.OnNextDialogueHandler();
        }
    }

}
