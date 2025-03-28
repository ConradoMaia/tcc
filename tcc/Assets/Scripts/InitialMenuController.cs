using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void CarregarCenaRespiracao()
    {
        SceneManager.LoadScene("Respiracao");
    }

    public void CarregarSegunda()
    {
        SceneManager.LoadScene("level1");
    }

    public void SairDoJogo()
    {
        Application.Quit();
    }
}