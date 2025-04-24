using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigator : MonoBehaviour
{
    // Singleton para acesso global
    public static SceneNavigator Instance { get; private set; }

    void Awake()
    {
        // Implementação do padrão Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Navega para uma nova cena, registrando no histórico
    public void NavigateToScene(string sceneName)
    {
        // A cena atual será adicionada ao histórico pelo BackButton.Awake
        SceneManager.LoadScene(sceneName);
    }

    // Limpa o histórico e vai para a cena inicial (útil para reiniciar o jogo)
    public void NavigateToStartAndClearHistory(string startSceneName)
    {
        BackButton.ClearNavigationHistory();
        SceneManager.LoadScene(startSceneName);
    }
}