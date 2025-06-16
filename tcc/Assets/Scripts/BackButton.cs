using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class BackButton : MonoBehaviour
{
    private const string NAVIGATION_HISTORY_KEY = "NavigationHistory";
    private const string SEPARATOR = "|";
    private const string MENU_SCENE = "StartMenu";
    
    private static List<string> navigationHistory = new List<string>();
    
    void Awake()
    {
        if (navigationHistory.Count == 0 && PlayerPrefs.HasKey(NAVIGATION_HISTORY_KEY))
        {
            string historyString = PlayerPrefs.GetString(NAVIGATION_HISTORY_KEY);
            string[] scenes = historyString.Split(new[] { SEPARATOR }, System.StringSplitOptions.RemoveEmptyEntries);
            navigationHistory = new List<string>(scenes);
        }
        
        string currentScene = SceneManager.GetActiveScene().name;
        if (navigationHistory.Count == 0 || navigationHistory[navigationHistory.Count - 1] != currentScene)
        {
            navigationHistory.Add(currentScene);
            SaveNavigationHistory();
        }
        
        if (navigationHistory.Count > 10)
        {
            navigationHistory.RemoveAt(0);
            SaveNavigationHistory();
        }
    }
    
    public void GoBack()
    {
        if (navigationHistory.Count > 0)
        {
            navigationHistory.RemoveAt(navigationHistory.Count - 1);
        }
        
        if (navigationHistory.Count > 0)
        {
            string previousScene = navigationHistory[navigationHistory.Count - 1];
            navigationHistory.RemoveAt(navigationHistory.Count - 1);
            SaveNavigationHistory();
            SceneManager.LoadScene(previousScene);
        }
        else
        {
            navigationHistory.Clear();
            SaveNavigationHistory();
            SceneManager.LoadScene(MENU_SCENE);
        }
    }
    
    private void SaveNavigationHistory()
    {
        string historyString = string.Join(SEPARATOR, navigationHistory.ToArray());
        PlayerPrefs.SetString(NAVIGATION_HISTORY_KEY, historyString);
        PlayerPrefs.Save();
    }
    
    public static void ClearNavigationHistory()
    {
        navigationHistory.Clear();
        PlayerPrefs.DeleteKey(NAVIGATION_HISTORY_KEY);
        PlayerPrefs.Save();
    }
}