using UnityEngine;

public class DrawUI : MonoBehaviour
{
    [SerializeField] DrawManager drawManager;
    public void OnDrawBtnHandler()
    {
        if(!this.gameObject.activeSelf)
            this.gameObject.SetActive(true);
    }
    public void OnPrevBtnHandler()
    {
        if(this.gameObject.activeSelf)
            this.gameObject.SetActive(false);
    }

    public void OnContentBtnHandler(int _index)
    {

    }
}
