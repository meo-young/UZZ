using UnityEngine;

public class NextHandler : MonoBehaviour
{
    [SerializeField] GameObject nextObject;
    [SerializeField] GameObject nextObject2;

    public void OnNextHandler()
    {
        if (!nextObject.activeSelf)
            nextObject.SetActive(true);

        if(nextObject2 != null)
            if (!nextObject2.activeSelf)
                nextObject2.SetActive(true);

        if (this.gameObject.activeSelf)
            this.gameObject.SetActive(false);
    }
}
