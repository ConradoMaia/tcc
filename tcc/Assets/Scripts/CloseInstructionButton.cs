using UnityEngine;
using UnityEngine.UI;

public class CloseInstructionButton : MonoBehaviour
{
    private Button button;
    
    private void Awake()
    {
        button = GetComponent<Button>();
        
        if (button == null)
        {
            Debug.LogError("Botao nao encontrado no objeto CloseInstructionButton!");
            return;
        }
        
        button.onClick.AddListener(OnCloseButtonClicked);
    }
    
    private void OnCloseButtonClicked()
    {
        Level3Manager level3Manager = FindObjectOfType<Level3Manager>();
        if (level3Manager != null)
        {
            level3Manager.CloseInstructionPanel();
        }
        else
        {
            Debug.LogError("Level3Manager nao encontrado na cena!");
        }
    }
}