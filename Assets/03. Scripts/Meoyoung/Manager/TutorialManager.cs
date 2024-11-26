using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [Header("# Hightlight Transform List")]
    [SerializeField] GameObject hightlight;
    [Space(5)]
    [SerializeField] Transform pure;
    [SerializeField] RectTransform wateringUI;

    [Header("# Dialogue Info")]
    [SerializeField] string[] dialogues;

    private void Start()
    {
        Instantiate(hightlight, pure.position, Quaternion.identity);
        
        Instantiate(hightlight, wateringUI.position , Quaternion.identity);
    }
}
