using UnityEngine;
using UnityEngine.UI;

public class ContinueButton : MonoBehaviour
{
    private Button button;
    
    private void Awake()
    {
        button = GetComponent<Button>();
        
        if (button == null)
        {
            Debug.LogError("Botao nao encontrado no objeto ContinueButton!");
            return;
        }
        
        button.onClick.AddListener(OnContinueButtonClicked);
    }
    
    private void OnContinueButtonClicked()
    {
        Level3Manager level3Manager = FindObjectOfType<Level3Manager>();
        if (level3Manager != null)
        {
            level3Manager.CompleteLevel();
        }
        else
        {
            Debug.LogError("Level3Manager nao encontrado na cena!");
        }
    }
}