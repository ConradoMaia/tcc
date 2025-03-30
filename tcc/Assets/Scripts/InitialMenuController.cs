using UnityEngine;
using UnityEngine.SceneManagement;

public class InitialMenuController : MonoBehaviour
{
    public void CarregarCena()
    {
        SceneManager.LoadScene("MoodThermometer");
    }

    public void SairDoJogo()
    {
        Application.Quit();
    }
}