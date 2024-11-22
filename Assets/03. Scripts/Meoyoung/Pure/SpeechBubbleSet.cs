using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBubbleSet : MonoBehaviour
{
    [Header("Work Speech")]
    public GameObject[] fieldWorkHelpBubble;
    public GameObject[] fieldWorkHelpSuccessBubble;


    private void Awake()
    {
        #region Init
        for(int i=0; i<fieldWorkHelpBubble.Length; i++)
        {
            if (fieldWorkHelpBubble[i].activeSelf)
                fieldWorkHelpBubble[i].SetActive(false);
            if (fieldWorkHelpSuccessBubble[i].activeSelf)
                fieldWorkHelpSuccessBubble[i].SetActive(false);
        }
        #endregion
    }
}
