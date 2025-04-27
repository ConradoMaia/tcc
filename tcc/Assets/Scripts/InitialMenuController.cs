using UnityEngine;
using UnityEngine.SceneManagement;

public class InitialMenuController : MonoBehaviour
{
    [SerializeField] private AchievmentUI achievementUI;

    private void Start()
    {
        // Limpa o histórico de navegação ao iniciar o jogo
        BackButton.ClearNavigationHistory();
    }

    public void CarregarCena()
    {
        // Usa o SceneNavigator se disponível, caso contrário usa o SceneManager diretamente
        if (SceneNavigator.Instance != null)
        {
            SceneNavigator.Instance.NavigateToScene("MoodThermometer");
        }
        else
        {
            SceneManager.LoadScene("MoodThermometer");
        }
    }

    public void MostrarConquistas()
    {
        if (achievementUI != null)
        {
            achievementUI.OpenAchievementPanel();
        }
        else
        {
            Debug.LogError("AchievmentUI não está referenciado no InitialMenuController!");
        }
    }

    public void SairDoJogo()
    {
        Application.Quit();
    }
}