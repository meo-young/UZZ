﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    public static VFXManager instance;

    [Header("# Work VFX")]
    public GameObject growthVFX;
    public GameObject dewVFX;

    [Header("# Likeability VFX")]
    public GameObject likeabilityVFX;
    public GameObject levelUpVFX;

    [Header("# Flower VFX")]
    public GameObject flowerGrowthVFX;
    public GameObject flowerStepUpVFX;
    public GameObject flowerStepUpWaitVFX;
    public GameObject flowerLevelUpVFX;

    [Header("# Touch VFX")]
    public GameObject defaultTouchVFX;
    public GameObject daisyTouchVFX;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        #region Init
        if (growthVFX.activeSelf)
            growthVFX.SetActive(false);

        if(dewVFX.activeSelf)
            dewVFX.SetActive(false);
        #endregion
    }
}
