using UnityEngine;




public class TutorialChecker : MonoBehaviour
{
    public static TutorialChecker instance;

    [HideInInspector] public bool flag = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
