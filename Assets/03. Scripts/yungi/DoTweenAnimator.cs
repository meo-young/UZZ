using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoTweenAnimator : MonoBehaviour
{

    public void clickButton(GameObject gameObject)
    {
        gameObject.transform.DOScale(0.9f, 0.05f);
    }

    public void restoreButton(GameObject gameObject)
    {
        gameObject.transform.DOScale(1.0f, 0.05f);
    }

    public void clickBigButton(GameObject gameObject)
    {
        gameObject.transform.DOScale(1.35f, 0.05f);
    }
    public void restoreBigButton(GameObject gameObject)
    {
        gameObject.transform.DOScale(1.45f, 0.05f);
    }

}
