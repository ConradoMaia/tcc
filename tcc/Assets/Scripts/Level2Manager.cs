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
    
    [Header("Level Settings")]
    public int requiredToolInteractions = 5;
    private int toolInteractionsCount = 0;
    
    private DentalToolManager toolManager;
    private GameManager gameManager;
    
    void Awake()
    {
        toolManager = FindObjectOfType<DentalToolManager>();
        gameManager = FindObjectOfType<GameManager>();
    }
    
    void Start()
    {
        if (gameManager != null)
        {
            gameManager.totalItemsNeeded = requiredToolInteractions;
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
            
            if (toolManager.toolInfoPopup != null)
            {
                toolManager.toolInfoPopup.SetActive(false);
            }
        }
    }
    
    void OnDestroy()
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
            SceneManager.LoadScene("LevelMenu");
        }
    }
    
    private void IncrementToolInteraction()
    {
        toolInteractionsCount++;
        
        if (gameManager != null)
        {
            gameManager.RegisterItemPlaced("Tool_" + toolInteractionsCount);
        }
    }
}