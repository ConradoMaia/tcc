using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PopupManager : MonoBehaviour
{
    public GameObject congratulationsPopup;
    public Button nextLevelButton;
    public Button menuButton;
    
    public string nextSceneName = "LevelMenu";
    
    void Start()
    {
        if (congratulationsPopup != null)
        {
            congratulationsPopup.SetActive(false);
        }
        
        if (nextLevelButton != null)
        {
            nextLevelButton.onClick.AddListener(GoToNextLevel);
        }
        
        if (menuButton != null)
        {
            menuButton.onClick.AddListener(GoToMenu);
        }
    }
    
    public void ShowPopup()
    {
        if (congratulationsPopup != null)
        {
            congratulationsPopup.SetActive(true);
        }
    }
    
    private void GoToNextLevel()
    {
        string nextLevelName = nextSceneName;
        
        if (string.IsNullOrEmpty(nextSceneName) || nextSceneName == "LevelMenu")
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            
            if (currentSceneName == "Level1")
            {
                nextLevelName = "Level2";
            }
            else if (currentSceneName == "Level2")
            {
                nextLevelName = "Level3";
            }
            else if (currentSceneName == "Level3")
            {
                nextLevelName = "LevelMenu";
            }
        }
        
        if (SceneNavigator.Instance != null)
        {
            SceneNavigator.Instance.NavigateToScene(nextLevelName);
        }
        else
        {
            SceneManager.LoadScene(nextLevelName);
        }
    }
    
    private void GoToMenu()
    {
        if (SceneNavigator.Instance != null)
        {
            SceneNavigator.Instance.NavigateToScene("LevelMenu");
        }
        else
        {
            SceneManager.LoadScene("LevelMenu");
        }
    }
}