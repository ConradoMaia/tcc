using UnityEngine;
using UnityEngine.SceneManagement;

public class RespiracaoController : MonoBehaviour
{
    // Refer�ncias aos objetos da cena
    public GameObject flor; // Objeto da flor
    public GameObject caule; // Objeto do caule
    public GameObject velaCompleta; // Objeto pai da vela (cont�m a base e a chama)
    public GameObject telaOpcoes; // Tela com os bot�es de op��es

    private Animator florAnimator; // Animator da flor
    private VelaController velaController; // Script para controlar a vela

    void Start()
    {
        // Obter os componentes Animator e o controlador da vela
        florAnimator = flor.GetComponent<Animator>();
        velaController = velaCompleta.GetComponent<VelaController>();

        // Iniciar o ciclo de respira��o
        IniciarRespiracao();
    }

    void IniciarRespiracao()
    {
        // Ativar a flor e o caule, e iniciar a anima��o de inspira��o
        flor.SetActive(true);
        caule.SetActive(true);
        velaCompleta.SetActive(false);
        telaOpcoes.SetActive(false);

        // Reproduzir a anima��o da flor
        florAnimator.Play("FlorInchando");

        // Agendar a transi��o para a vela ap�s a anima��o da flor (4 segundos)
        Invoke("IniciarExpiracao", 4f);
    }

    void IniciarExpiracao()
    {
        // Desativar a flor e o caule, e ativar a vela
        flor.SetActive(false);
        caule.SetActive(false);
        velaCompleta.SetActive(true);

        // Iniciar a anima��o da chama da vela
        if (velaController != null)
        {
            velaController.enabled = true; // Ativar o script da vela
        }

        // Agendar a exibi��o da tela de op��es ap�s a anima��o da vela (6 segundos)
        Invoke("MostrarTelaOpcoes", 6f);
    }

    void MostrarTelaOpcoes()
    {
        // Desativar a vela e exibir a tela com os bot�es
        velaCompleta.SetActive(false);
        telaOpcoes.SetActive(true);
    }

    // Fun��o chamada pelo bot�o "Repetir"
    public void RepetirTecnica()
    {
        // Reiniciar o ciclo de respira��o
        IniciarRespiracao();
    }

    // Fun��o chamada pelo bot�o "Continuar"
    public void Continuar()
    {
        // Carregar a pr�xima cena ou etapa
        SceneManager.LoadScene("FearList");
    }
}