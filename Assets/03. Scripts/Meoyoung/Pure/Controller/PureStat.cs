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
    [HideInInspector] public LikeabilityInfo[] likeabilityInfo;
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


    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        UpdateLikeabilityInfo();

        if (PlayerPrefs.GetInt("Story") == 1)
        {

            Debug.Log(PlayerPrefs.GetInt("Plus"));
            GetLikeability(PlayerPrefs.GetInt("Plus"));
            PlayerPrefs.SetInt("Plus", 0);
            PlayerPrefs.SetInt("Story", 0);
        }
    }
    public void GetLikeability(int _acquiredLike)
    {
        SoundManager.instance.PlaySFX(SFX.Ambience.LIKE);

        Debug.Log(_acquiredLike + " 호감도 획득");
        ShowLikeabilityUI(_acquiredLike);
        pureInfo.likeability += _acquiredLike;
        if (pureInfo.likeability >= likeabilityInfo[pureInfo.level].requiredExp)
        {
            pureInfo.level++;
            PureInteractionText.instance.UpdatePureInteractionDialogue();
            MainManager.instance.pureController.isLevelUp = true;
            pureInfo.likeability = 0;
        }
    }

    void ShowLikeabilityUI(int _acquiredLike)
    {
        // 푸르가 작업중이면 작업에 따른 위치를 반환
        Transform targetPos;
        if (MainManager.instance.pureController.fieldWorkState.type != FieldWorkType.None)
            targetPos = MainManager.instance.pureController.ReturnWorkPurePosition();
        else
            targetPos = target;


        float likeabilityValue = (float)pureInfo.likeability / likeabilityInfo[pureInfo.level].requiredExp;
        float targetLikeValue = ((float)pureInfo.likeability + _acquiredLike) / likeabilityInfo[pureInfo.level].requiredExp;
        if(targetLikeValue > 1)
            targetLikeValue = 1;

        likeabilityUI.transform.position = targetPos.position + likeabilityPos;
        likeabilityUI.GetComponent<LikeabilityUI>().ReflectionValue(likeabilityValue, targetLikeValue, pureInfo.level, targetPos);
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
