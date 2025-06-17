using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigator : MonoBehaviour
{
    public static SceneNavigator Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void NavigateToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void NavigateToStartAndClearHistory(string startSceneName)
    {
        BackButton.ClearNavigationHistory();
        SceneManager.LoadScene(startSceneName);
    }
}