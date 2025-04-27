using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PopupManager : MonoBehaviour
{
    public GameObject popupPanel; // Referência ao painel do popup
    public Button closeButton; // Referência ao botão de fechar
    public AudioClip successSound; // Som de parabéns
    public string nextSceneName = "MoodThermometer"; // Cena para onde voltar após fechar o popup
    private AudioSource audioSource; // Componente para tocar o som

    private void Start()
    {
        // Certifique-se de que o popup está desativado no início
        popupPanel.SetActive(false);

        // Adiciona o evento de clique ao botão de fechar
        closeButton.onClick.AddListener(ClosePopup);

        // Configura o AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    public void ShowPopup()
    {
        Debug.Log("PopupManager: Mostrando popup");
        
        // Verifica se o painel existe
        if (popupPanel == null)
        {
            Debug.LogError("PopupManager: popupPanel nu00e3o estu00e1 referenciado!");
            return;
        }
        
        // Ativa o popup
        popupPanel.SetActive(true);

        // Toca o som de parabéns
        if (successSound != null)
        {
            audioSource.PlayOneShot(successSound);
        }

        // Anima o popup (aumenta de escala com efeito)
        popupPanel.transform.localScale = Vector3.zero; // Começa invisível
        LeanTween.scale(popupPanel, Vector3.one, 0.5f).setEaseOutBack();
    }

    private void ClosePopup()
    {
        // Anima o fechamento do popup (reduz a escala)
        LeanTween.scale(popupPanel, Vector3.zero, 0.5f).setEaseInBack().setOnComplete(() =>
        {
            popupPanel.SetActive(false);
            
            // Usa o SceneNavigator se disponível, caso contrário usa o SceneManager diretamente
            if (SceneNavigator.Instance != null)
            {
                SceneNavigator.Instance.NavigateToScene(nextSceneName);
            }
            else
            {
                SceneManager.LoadScene(nextSceneName);
            }
        });
    }
}