using UnityEngine;
using UnityEngine.SceneManagement;

public class TutoNextScene : MonoBehaviour
{
    [SerializeField] string sceneName;
    public void LoadNextScene()
    {
        TutorialChecker.instance.flag = true;
        TutorialDataManager.instance.JsonSave();
        SceneManager.LoadScene(sceneName);
    }
}
