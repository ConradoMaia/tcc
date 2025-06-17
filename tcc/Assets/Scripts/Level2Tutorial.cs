using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Level2Tutorial : MonoBehaviour
{
    [Header("UI References")]
    public GameObject tutorialPanel;
    public Button nextButton;
    public Button skipButton;
    public TextMeshProUGUI tutorialText;
    public Image tutorialImage;
    
    [Header("Tutorial Content")]
    [TextArea(3, 5)]
    public string[] tutorialSteps;
    public Sprite[] tutorialImages;
    
    private int currentStep = 0;
    private Level2Manager levelManager;
    
    private void Start()
    {
        levelManager = FindObjectOfType<Level2Manager>();
        
        if (nextButton != null)
        {
            nextButton.onClick.AddListener(NextStep);
        }
        
        if (skipButton != null)
        {
            skipButton.onClick.AddListener(SkipTutorial);
        }
        
        // Inicia o tutorial
        ShowTutorial();
    }
    
    private void ShowTutorial()
    {
        if (tutorialPanel != null)
        {
            tutorialPanel.SetActive(true);
            DisplayCurrentStep();
        }
    }
    
    private void DisplayCurrentStep()
    {
        if (currentStep < tutorialSteps.Length)
        {
            // Atualiza o texto
            if (tutorialText != null)
            {
                tutorialText.text = tutorialSteps[currentStep];
            }
            
            // Atualiza a imagem, se disponu00edvel
            if (tutorialImage != null && tutorialImages.Length > currentStep)
            {
                tutorialImage.sprite = tutorialImages[currentStep];
                tutorialImage.gameObject.SetActive(tutorialImages[currentStep] != null);
            }
            
            // Atualiza o texto do botu00e3o para o u00faltimo passo
            if (nextButton != null && currentStep == tutorialSteps.Length - 1)
            {
                TextMeshProUGUI buttonText = nextButton.GetComponentInChildren<TextMeshProUGUI>();
                if (buttonText != null)
                {
                    buttonText.text = "Começar";
                }
            }
        }
        else
        {
            // Finaliza o tutorial
            CloseTutorial();
        }
    }
    
    private void NextStep()
    {
        currentStep++;
        
        if (currentStep < tutorialSteps.Length)
        {
            DisplayCurrentStep();
        }
        else
        {
            CloseTutorial();
        }
    }
    
    private void SkipTutorial()
    {
        CloseTutorial();
    }
    
    private void CloseTutorial()
    {
        if (tutorialPanel != null)
        {
            tutorialPanel.SetActive(false);
        }
        
        // Inicia o nu00edvel
        if (levelManager != null)
        {
            levelManager.StartLevel();
        }
    }
}