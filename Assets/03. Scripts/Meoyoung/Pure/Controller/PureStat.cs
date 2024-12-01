using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


[System.Serializable]
public class PureInfo
{
    public int level = 1;
    public int likeability;
    public bool autoText;
    public bool interactionText;
}


public class PureStat : MonoBehaviour
{
    public static PureStat instance;

    [System.Serializable]
    public struct LikeabilityInfo
    {
        public int requiredExp;
        public int totalExp;
        public int defaultAnimationIndex;
        public int interactionAnimationIndex;
    }


    [Header("# Stat")]
    [Tooltip("랜덤하게 걷는 시간")]
    public float walkRandomTime;
    [Tooltip("걷는 속도")]
    public float walkSpeed;

    [Space(10)]

    [Header("Likeability")]
    public TextAsset likeabilityDataTable;
    public PureInfo pureInfo;
    [Tooltip("레벨에 필요한 호감도 리스트")]
    public LikeabilityInfo[] likeabilityInfo;
    [Tooltip("호감도 GUI")]
    public GameObject likeabilityUI;
    [Tooltip("UI를 띄울 때 기준으로 할 타겟")]
    public Transform target;
    public Vector3 likeabilityPos;

    [Space(10)]

    [Header("# Animation Timer")]
    public float baseAnimationPlayTime;

    [Space(10)]

    [Header("# etc.")]
    [Tooltip("작업 소요 시간")]
    public float workRequiredTime;
    [Tooltip("레벨업 이펙트를 위한 VFX Manager")]
    [SerializeField] VFXManager vfxManager;
    [SerializeField] PureController pc;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        if (likeabilityUI.activeSelf)
            likeabilityUI.SetActive(false);
    }

    private void Start()
    {
        UpdateLikeabilityInfo();
    }
    public void GetLikeability(int _likeability)
    {
        SoundManager.instance.PlaySFX(SFX.Ambience.LIKE);

        pureInfo.likeability += _likeability;
        if (pureInfo.likeability > likeabilityInfo[pureInfo.level].requiredExp)
        {
            SoundManager.instance.PlaySFX(SFX.Ambience.LEVELUP);
            PureInteractionText.instance.UpdatePureInteractionDialogue();
            pureInfo.level++;
            pc.isLevelUp = true;
            pureInfo.likeability = 0;
            Instantiate(vfxManager.levelUpVFX, target.position, Quaternion.identity);
        }
        ShowLikeabilityUI();
    }

    void ShowLikeabilityUI()
    {
        likeabilityUI.SetActive(true);

        float likeabilityValue = (float)pureInfo.likeability / likeabilityInfo[pureInfo.level].requiredExp;

        Transform targetPos;
        if (pc.fieldWorkState.state != FieldWorkState.None)
        {
            targetPos = pc.ReturnWorkPurePosition();
        }
        else
        {
            targetPos = target;
        }
        Vector3 likeabilityPanelPos = targetPos.position + likeabilityPos;
        likeabilityUI.transform.position = likeabilityPanelPos;
        likeabilityUI.GetComponent<LikeabilityUI>().ReflectionValue(likeabilityValue, pureInfo.level);

        Invoke(nameof(SetActiveFalseUI), 3);
    }

    void SetActiveFalseUI()
    {
        if (likeabilityUI.activeSelf)
            likeabilityUI.SetActive(false);

        pc.isLevelUp = false;
    }

    public void SetTrueInteractionText()
    {
        pureInfo.interactionText = true;
        Invoke(nameof(SetFalseInteractionText), 10);
    }

    void SetFalseInteractionText()
    {
        pureInfo.interactionText = false;
    }

    public void SetTrueAutoText()
    {
        pureInfo.autoText = true;
        Invoke(nameof(SetFalseAutoText), 10);
    }

    void SetFalseAutoText()
    {
        pureInfo.autoText = false;
    }

    void UpdateLikeabilityInfo()
    {
        likeabilityInfo = new LikeabilityInfo[100];

        StringReader reader = new StringReader(likeabilityDataTable.text);
        bool head = false;
        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            if (!head)
            {
                head = true;
                continue;
            }
            string[] values = line.Split('\t');

            likeabilityInfo[int.Parse(values[0]) - 1].requiredExp = int.Parse(values[1]);
            likeabilityInfo[int.Parse(values[0]) - 1].totalExp = int.Parse(values[2]);
            likeabilityInfo[int.Parse(values[0]) - 1].defaultAnimationIndex = int.Parse(values[3]);
            likeabilityInfo[int.Parse(values[0]) - 1].interactionAnimationIndex = int.Parse(values[4]);

        }
    }
}
