using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PureAnimationSet : MonoBehaviour
{
    public static PureAnimationSet instance;

    [Serializable]
    public class FieldWorkIndex
    {
        public GameObject[] animIndex = new GameObject[5];
    }

    [Header("# Pure Animation")]
    [Header("# Available")]
    public GameObject pureIdle;
    public GameObject pureWalk;
    [Space(10)]
    [Header("# Base")]
    public List<GameObject> pureBaseAnimations;
    [Space(10)]
    [Header("# Interaction")]
    public List<GameObject> pureInteractionAnimations;
    [Space(10)]
    [Header("# Present")]
    public GameObject purePresentReady;
    public GameObject purePresentGive;
    [Space(10)]
    [Header("# Field Work")]
    public FieldWorkIndex[] pureFieldWorkIndex;
    public FieldWorkIndex[] pureHelpFieldWorkIndex;


    private SpeechBubbleSet bubbleSet;
    private FieldWork[] fieldWorkArray => FieldWorkManager.instance.fieldWorkArray;

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }

    private void Start()
    {
        #region Init Animation
        if (!pureIdle.activeSelf)
            pureWalk.SetActive(true);

        if (pureWalk.activeSelf)
            pureWalk.SetActive(false);

        if(purePresentReady.activeSelf)
            purePresentReady.SetActive(false);

        if(purePresentGive.activeSelf)
            purePresentGive.SetActive(false);

        for(int i =0; i<pureBaseAnimations.Count; i++)
        {
            if (pureBaseAnimations[i].activeSelf)
                pureBaseAnimations[i].SetActive(false);
        }
        #endregion

        #region Init Script
        bubbleSet = MainManager.instance.speechBubbleSet;
        #endregion
    }

    public void ShowRandomBaseAnimation(int index)
    {
        pureBaseAnimations[index].SetActive(!pureBaseAnimations[index].activeSelf);
    }

    public void ShowRandomInteractionAnimation(int index)
    {
        pureInteractionAnimations[index].SetActive(!pureInteractionAnimations[index].activeSelf);
    }

    #region FieldWork Animation
    public void ControlFieldWorkAnimation(FieldWorkType state)
    {
        if ((int)state == 5)
            return;

        // 해당 작업의 Step에 지정된 애니메이션을 활성화, 비활성화
        pureFieldWorkIndex[(int)state].animIndex[fieldWorkArray[(int)state].step]
            .SetActive(!pureFieldWorkIndex[(int)state].animIndex[fieldWorkArray[(int)state].step].activeSelf);
    }

    public void ControlFieldWorkHelpAnimation(FieldWorkType state)
    {
        if ((int)state == 5)
            return;
        // 해당 작업의 Step에 지정된 애니메이션을 활성화, 비활성화
        pureHelpFieldWorkIndex[(int)state].animIndex[fieldWorkArray[(int)state].step]
            .SetActive(!pureHelpFieldWorkIndex[(int)state].animIndex[fieldWorkArray[(int)state].step].activeSelf);

        bubbleSet.fieldWorkHelpBubble[(int)state].SetActive(!bubbleSet.fieldWorkHelpBubble[(int)state].activeSelf);
    }

    public void ShowFieldWorkHelpSuccessText(FieldWorkType state)
    {
        if ((int)state == 5)
            return;

        VFXManager.instance.successVFX[(int)state].SetActive(true);
        bubbleSet.fieldWorkHelpSuccessBubble[(int)state].SetActive(!bubbleSet.fieldWorkHelpSuccessBubble[(int)state].activeSelf);
    }
    #endregion
}
