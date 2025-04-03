using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PopupManager : MonoBehaviour
{
    public GameObject popupPanel; // Refer�ncia ao painel do popup
    public Button closeButton; // Refer�ncia ao bot�o de fechar
    public AudioClip successSound; // Som de parab�ns
    private AudioSource audioSource; // Componente para tocar o som

    private void Start()
    {
        // Certifique-se de que o popup est� desativado no in�cio
        popupPanel.SetActive(false);

        // Adiciona o evento de clique ao bot�o de fechar
        closeButton.onClick.AddListener(ClosePopup);

        // Configura o AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    public void ShowPopup()
    {
        // Ativa o popup
        popupPanel.SetActive(true);

        // Toca o som de parab�ns
        if (successSound != null)
        {
            audioSource.PlayOneShot(successSound);
        }

        // Anima o popup (aumenta de escala com efeito)
        popupPanel.transform.localScale = Vector3.zero; // Come�a invis�vel
        LeanTween.scale(popupPanel, Vector3.one, 0.5f).setEaseOutBack();
    }

    private void ClosePopup()
    {
        // Anima o fechamento do popup (reduz a escala)
        LeanTween.scale(popupPanel, Vector3.zero, 0.5f).setEaseInBack().setOnComplete(() =>
        {
            popupPanel.SetActive(false);
            SceneManager.LoadScene("MoodThermometer");
        });
    }
}