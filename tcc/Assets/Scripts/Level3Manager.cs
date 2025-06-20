using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Level3Manager : MonoBehaviour
{
    [Header("Configuraçoes do Nível")]
    public GameObject instructionPanel;
    public Button continueButton;
    public Text instructionText;
    
    [Header("Ferramentas")]
    public List<GameObject> tools = new List<GameObject>();
    
    private HashSet<string> interactedTools = new HashSet<string>();
    
    private void Start()
    {
        if (instructionPanel != null)
        {
            instructionPanel.SetActive(true);
            
            if (instructionText != null)
            {
                instructionText.text = "Toque nas ferramentas para ouvir seus sons e sentir a vibraçao. " +
                                      "Quando terminar, clique no botao Continuar.";
            }
        }
        
        if (continueButton != null)
        {
            continueButton.onClick.AddListener(CompleteLevel);
        }
        
        SetToolsInteractable(false);
    }
    
    public void CloseInstructionPanel()
    {
        if (instructionPanel != null)
        {
            instructionPanel.SetActive(false);
            SetToolsInteractable(true);
        }
    }
    
    private void SetToolsInteractable(bool interactable)
    {
        foreach (GameObject tool in tools)
        {
            if (tool != null)
            {
                Collider2D collider = tool.GetComponent<Collider2D>();
                if (collider != null)
                {
                    collider.enabled = interactable;
                }
                
                Collider collider3D = tool.GetComponent<Collider>();
                if (collider3D != null)
                {
                    collider3D.enabled = interactable;
                }
            }
        }
    }
    
    public void RegisterToolInteraction(string toolName)
    {
        interactedTools.Add(toolName);
        Debug.Log($"Ferramenta interagida: {toolName}. Total: {interactedTools.Count}/{tools.Count}");
    }
    
    public void CompleteLevel()
    {        
        if (AchievementSystem.Instance != null)
        {
            Debug.Log("Tentando desbloquear conquista: level_3_completed");
            AchievementSystem.Instance.UnlockAchievement("level_3_completed");
        }
        
        SceneManager.LoadScene("StartMenu");
    }
}