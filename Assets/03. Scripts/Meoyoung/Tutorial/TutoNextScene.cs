using UnityEngine;
using UnityEngine.SceneManagement;

public class TutoNextScene : MonoBehaviour
{
    public void LoadNextScene()
    {
        SceneManager.LoadScene("Main");
    }
}
