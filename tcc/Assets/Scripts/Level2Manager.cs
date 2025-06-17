using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level2Manager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject instructionsPanel;
    public Button startButton;
    public Button backButton;
    public Level2Tutorial tutorial;
    
    [Header("Level Completion")]
    public int requiredToolInteractions = 5;
    private int toolInteractionsCount = 0;
    
    private DentalToolManager toolManager;
    private LevelCompletionManager completionManager;
    
    private void Start()
    {
        toolManager = FindObjectOfType<DentalToolManager>();
        completionManager = FindObjectOfType<LevelCompletionManager>();
        
        if (toolManager == null)
        {
            Debug.LogError("DentalToolManager not found in the scene!");
        }
        
        if (instructionsPanel != null)
        {
            instructionsPanel.SetActive(true);
        }
        
        if (startButton != null)
        {
            startButton.onClick.AddListener(StartLevel);
        }
        
        if (backButton != null)
        {
            backButton.onClick.AddListener(GoBack);
        }
        
        if (toolManager != null)
        {
            toolManager.OnToolInteraction += IncrementToolInteraction;
        }
    }
    
    private void OnDestroy()
    {
        if (toolManager != null)
        {
            toolManager.OnToolInteraction -= IncrementToolInteraction;
        }
    }
    
    public void StartLevel()
    {
        if (instructionsPanel != null)
        {
            instructionsPanel.SetActive(false);
        }
    }
    
    private void GoBack()
    {
        if (SceneNavigator.Instance != null)
        {
            SceneNavigator.Instance.NavigateToScene("LevelMenu");
        }
        else
        {
            Debug.LogError("SceneNavigator not found in the scene!");
            SceneManager.LoadScene("LevelMenu");
        }
    }
    
    private void IncrementToolInteraction()
    {
        toolInteractionsCount++;
        
        if (toolInteractionsCount >= requiredToolInteractions)
        {
            CompleteLevel();
        }
    }
    
    private void CompleteLevel()
    {
        if (completionManager != null)
        {
            completionManager.CompleteLevel();
        }
        else
        {
            Debug.LogWarning("LevelCompletionManager not found. Cannot mark level as complete.");
        }
    }
}