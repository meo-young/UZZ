using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    public static VFXManager instance;

    [Header("# Work VFX")]
    public GameObject growthVFX;
    public GameObject dewVFX;
    public GameObject[] successVFX;
    public GameObject fieldWorkLevelUpVFX;

    [Header("# Likeability VFX")]
    public GameObject likeabilityVFX;
    public GameObject levelUpVFX;

    [Header("# Flower VFX")]
    public GameObject flowerGrowthVFX;
    public GameObject flowerStepUpVFX;
    public GameObject flowerStepUpWaitVFX;
    public GameObject flowerLevelUpVFX;
    public GameObject flowerAcquireReadyVFX;

    [Header("# Touch VFX")]
    public GameObject defaultTouchVFX;
    public GameObject daisyTouchVFX;
    public GameObject flowerEventTouchVFX;

    [Header("# Dust VFX")]
    public GameObject miniDustVFX;
    public GameObject bigDustVFX;

    [Header("# 뽑기 VFX")]
    public GameObject furnitureVFX;

    [Header("# 샤워 VFX")]
    public GameObject showerVFX;

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
