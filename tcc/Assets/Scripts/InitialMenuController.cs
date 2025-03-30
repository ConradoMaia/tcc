using UnityEngine;
using UnityEngine.SceneManagement;

public class InitialMenuController : MonoBehaviour
{
    public void CarregarCena()
    {
        SceneManager.LoadScene("MoodThermometer");
    }

    public void CarregarCena2()
    {
        SceneManager.LoadScene("MoodThermometer");
    }

    public void SairDoJogo()
    {
        Application.Quit();
    }
}