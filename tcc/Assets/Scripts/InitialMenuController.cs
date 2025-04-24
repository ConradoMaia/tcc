using UnityEngine;
using UnityEngine.SceneManagement;

public class InitialMenuController : MonoBehaviour
{
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

    public void SairDoJogo()
    {
        Application.Quit();
    }
}