using UnityEngine;

public class ShowVFXOnDisable : MonoBehaviour
{
    public enum VFXType
    {
        BigDust,
        MiniDust
    }

    [SerializeField] VFXType type;
    private void OnDisable()
    {
        switch(type)
        {
            case VFXType.BigDust:
                Instantiate(VFXManager.instance.bigDustVFX, this.transform.position, Quaternion.identity);
                break;
            case VFXType.MiniDust:
                Instantiate(VFXManager.instance.miniDustVFX, this.transform.position, Quaternion.identity);
                break;
        }
    }
}
