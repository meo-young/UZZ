using UnityEngine;

public class PureWatering : MonoBehaviour
{
    [SerializeField] float fieldWorkTime;

    [Header("# Camera")]
    [SerializeField] TutorialCamera cam;

    [Header("# Object")]
    [SerializeField] GameObject pure;
    [SerializeField] GameObject sprout;
    [SerializeField] GameObject flower;
    [SerializeField] GameObject tutoFlower1;
    [SerializeField] GameObject tutoFlower2;

    [Header("# UI")]
    [SerializeField] GameObject uiCanvas;
    [SerializeField] GameObject workPanel3;

    private float fieldWorkCounter;
    private int activationCounter;

    private void Start()
    {
        activationCounter = 0;
    }

    private void OnEnable()
    {
        fieldWorkCounter = 0;
    }

    private void Update()
    {
        fieldWorkCounter += Time.deltaTime;
        if (fieldWorkCounter > fieldWorkTime)
        {
            fieldWorkCounter = 0;
            if (this.gameObject.activeSelf)
            {
                uiCanvas.SetActive(false);
                switch (activationCounter)
                {
                    case 0:
                        cam.SetCameraPosition();
                        if (!pure.activeSelf)
                            pure.SetActive(true);
                        if (!sprout.activeSelf)
                            sprout.SetActive(true);
                        break;
                    case 1:
                        cam.SetCameraPosition();
                        tutoFlower1.SetActive(false);
                        tutoFlower2.SetActive(true);
                        if (!pure.activeSelf)
                            pure.SetActive(true);
                        if (sprout.activeSelf)
                            sprout.SetActive(false);
                        if (!flower.activeSelf)
                            flower.SetActive(true);
                        if(workPanel3.activeSelf)
                            workPanel3.SetActive(false);
                        break;
                }
                this.gameObject.SetActive(false);
                activationCounter++;
            }
        }
    }
}
