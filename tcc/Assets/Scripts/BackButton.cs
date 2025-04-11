using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    private string previousSceneName;

    void Start()
    {
        // Guarda o nome da cena anterior
        if (PlayerPrefs.HasKey("PreviousScene"))
        {
            previousSceneName = PlayerPrefs.GetString("PreviousScene");
        }
    }

    public void GoBack()
    {
        if (!string.IsNullOrEmpty(previousSceneName))
        {
            // Salva a cena atual antes de voltar
            PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
            SceneManager.LoadScene(previousSceneName);
        }
        else
        {
            // Se não houver cena anterior, volta para o menu principal
            SceneManager.LoadScene("StartMenu"); // Substitua pelo nome da sua cena de menu
        }
    }

    void OnDestroy()
    {
        // Salva a cena atual antes de destruir o objeto
        PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
    }
}