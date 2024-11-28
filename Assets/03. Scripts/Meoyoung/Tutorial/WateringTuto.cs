using UnityEngine;

public class WateringTuto : MonoBehaviour
{
    [Header("# VFX")]
    [SerializeField] GameObject vfxObject;
    [SerializeField] GameObject tutoHighlight;

    [Header("# Watering Pure")]
    [SerializeField] GameObject pure;
    [SerializeField] GameObject wateringPure;
    [SerializeField] GameObject wateringHelpPure;

    [Header("# Watering UI")]
    [SerializeField] GameObject wateringTutoObject3;
    private int counter = 0;

    public void Watering()
    {
        if (!vfxObject.activeSelf)
            return;

        vfxObject.SetActive(false);

        switch (counter)
        {
            case 0:
                if (tutoHighlight.activeSelf)
                    tutoHighlight.SetActive(false);

                if (pure.activeSelf)
                    pure.SetActive(false);

                if (!wateringPure.activeSelf)
                    wateringPure.SetActive(true);
                break;

            case 1:
                if (tutoHighlight.activeSelf)
                    tutoHighlight.SetActive(false);

                if (pure.activeSelf)
                    pure.SetActive(false);

                if (!wateringPure.activeSelf)
                    wateringPure.SetActive(true);
                break;
            case 2:
                if (pure.activeSelf)
                    pure.SetActive(false);
                if (!wateringHelpPure.activeSelf)
                    wateringHelpPure.SetActive(true);
                if (!wateringTutoObject3.activeSelf)
                    wateringTutoObject3.SetActive(true);
                break;
        }

        counter++;
    }
}
