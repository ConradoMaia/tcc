using UnityEngine;
using UnityEngine.UI;

public class InstructionPanel : MonoBehaviour
{
    [Header("Componentes")]
    public Text instructionText;
    public Button closeButton;
    
    [Header("Configuraçoes")]
    [TextArea(3, 10)]
    public string instructions = "Toque nas ferramentas para ouvir seus sons e sentir a vibração. Quando terminar, clique no botão Continuar.";
    
    private void Start()
    {
        if (instructionText != null)
        {
            instructionText.text = instructions;
        }
        
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(ClosePanel);
        }
    }
    
    private void ClosePanel()
    {
        Level3Manager level3Manager = FindObjectOfType<Level3Manager>();
        if (level3Manager != null)
        {
            level3Manager.CloseInstructionPanel();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}