using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutoNextScene : MonoBehaviour
{
    [SerializeField] string sceneName;
    public void LoadNextScene()
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        TutorialChecker.instance.flag = true;
        TutorialDataManager.instance.JsonSave();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
