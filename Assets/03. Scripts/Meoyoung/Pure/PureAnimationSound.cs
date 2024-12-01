using UnityEngine;

public class PureAnimationSound : MonoBehaviour
{
    [SerializeField] SFX.PureSound pureAnimationType;

    private void OnEnable()
    {
        SoundManager.instance.PlaySFX(pureAnimationType);
    }
}
