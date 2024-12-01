using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldWorkUI : MonoBehaviour
{
    [SerializeField] FieldWorkState workType;
    [SerializeField] Image coolTime;

    private float fieldWorkCoolTime;

    private PureController pureController;
    private FieldWorkManager fieldWorkManager;

    private void Awake()
    {       
        if (this.gameObject.activeSelf)
            this.gameObject.SetActive(false);

    }

    private void Start()
    {
        pureController = MainManager.instance.pureController;
        fieldWorkManager = MainManager.instance.fieldWorkManager;

        fieldWorkCoolTime = fieldWorkManager.fieldWorkArray[(int)workType].coolTime; 
        coolTime.fillAmount = fieldWorkManager.fieldWorkInfo.coolTimeList[(int)workType];
    }

    private void Update()
    {
        // 각 작업중 레벨이 1이상인 작업의 GUI만 활성화
        CheckWorkCoolTime();
    }

    void CheckWorkCoolTime()
    {
        if (fieldWorkManager.fieldWorkInfo.coolTimeList[(int)workType] > 0)
        {
            fieldWorkManager.fieldWorkInfo.coolTimeList[(int)workType] -= Time.deltaTime;
            coolTime.fillAmount = fieldWorkManager.fieldWorkInfo.coolTimeList[(int)workType] / fieldWorkCoolTime;
        }
        else
        {
            fieldWorkManager.fieldWorkArray[(int)workType].available = true;
        }
    }
    #region BtnHandler

    public void DoFieldWork()
    {
        SoundManager.instance.PlaySFX(SFX.Ambience.TOUCH);

        if (!pureController.CheckPureAvailable())
            return;

        if (MainManager.instance.flowerManager.isFlowerEvent)
            return;

        if (fieldWorkManager.fieldWorkInfo.coolTimeList[(int)workType] > 0)
            return;

        if (pureController.fieldWorkState.state != FieldWorkState.None)
            return;

        fieldWorkManager.fieldWorkInfo.coolTimeList[(int)workType] = fieldWorkManager.fieldWorkArray[(int)workType].coolTime;
        fieldWorkManager.fieldWorkArray[(int)workType].available = false;
        pureController.fieldWorkState = fieldWorkManager.fieldWorkArray[(int)workType];
        pureController.ChangeState(pureController._workState);
    }
    #endregion
    #region ImageTransparency
    void ReduceImageTransparency(Image image)
    {
        Color color = image.color;
        color.a = 0.5f;
        image.color = color;
    }

    void RecoveryImageTransparency(Image image)
    {
        Color color = image.color;
        color.a = 1f;
        image.color = color;
    }
    #endregion
}
