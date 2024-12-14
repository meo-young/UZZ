using System.Collections.Generic;
using UnityEngine;

public class TutoFlowerManager : MonoBehaviour
{
    [Header("# Tuto Object")]
    [SerializeField] TutorialManager tutoManager;
    [SerializeField] GameObject tutoHighlight;
    [SerializeField] GameObject flowertutoPanel4;
    [SerializeField] GameObject flowerVFX;

    [Header("# Tuto FlowerInfo")]
    [SerializeField] TutoDew tutoDew;
    [SerializeField] float maxShakeTime;
    [SerializeField] float acquireInterval;
    [SerializeField] Transform acquireEffectSpawnPos;
    [SerializeField] Transform acquireEffectTargetPos;
    [SerializeField] ItemAcquireFx itemPrefab;
    [SerializeField] GameObject daisyTouchVFX;

    private List<ItemAcquireFx> items;
    private float counter;
    private float intervalCounter;
    private bool finishShakeEvent;
    private void Awake()
    {
        InitAcquireVariable();
    }

    private void Start()
    {
        items = new List<ItemAcquireFx>();
    }
    void ShowAcquireEffect()
    {
        int randCount = Random.Range(7, 15);
        for (int i = 0; i < randCount; ++i)
        {
            var itemFx = GameObject.Instantiate<ItemAcquireFx>(itemPrefab, this.transform);
            itemFx.Explosion(acquireEffectSpawnPos.position, 10.0f);
            items.Add(itemFx);
        }
        Instantiate(daisyTouchVFX, acquireEffectSpawnPos);
    }
    public void MoveToTargetPos()
    {
        if (items == null)
            return;

        for (int i = 0; i < items.Count; i++)
            items[i].Move(acquireEffectTargetPos.position);

        GetDew();
        InitAcquireVariable();
    }

    public bool AcquireDewEffect()
    {
        if (finishShakeEvent) // 최대 흔들기 시간이 지난 경우 터치를 인식하지 않음
            return false;

        counter += Time.deltaTime;

        if (counter - intervalCounter > 0)
        {
            intervalCounter += acquireInterval;
            ShowAcquireEffect();
        }

        if (counter > maxShakeTime)
            finishShakeEvent = true;

        return true;
    }

    void InitAcquireVariable()
    {
        finishShakeEvent = false;
        intervalCounter = acquireInterval;
        counter = 0;
    }

    void GetDew()
    {
        tutoManager.OnNextDialogueHandler();
        tutoHighlight.SetActive(false);
        flowertutoPanel4.SetActive(false);
        flowerVFX.SetActive(false);
        StartCoroutine(tutoDew.Count(100, 0));
    }
}
