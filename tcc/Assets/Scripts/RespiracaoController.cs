using UnityEngine;
using UnityEngine.SceneManagement;

public class RespiracaoController : MonoBehaviour
{
    // Refer�ncias aos objetos da cena
    public GameObject flor; // Objeto da flor
    public GameObject caule; // Objeto do caule
    public GameObject vela; // Objeto da vela
    public GameObject telaOpcoes; // Tela com os bot�es de op��es

    private Animator florAnimator; // Animator da flor
    private Animator velaAnimator; // Animator da vela

    void Start()
    {
        // Obter os componentes Animator dos objetos
        florAnimator = flor.GetComponent<Animator>();
        velaAnimator = vela.GetComponent<Animator>();

        // Iniciar o ciclo de respira��o
        IniciarRespiracao();
    }

    void IniciarRespiracao()
    {
        // Ativar a flor e o caule, e iniciar a anima��o de inspira��o
        flor.SetActive(true);
        caule.SetActive(true);
        vela.SetActive(false);
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
        vela.SetActive(true);

        // Reproduzir a anima��o da vela
        velaAnimator.Play("VelaApagando");

        // Agendar a exibi��o da tela de op��es ap�s a anima��o da vela (6 segundos)
        Invoke("MostrarTelaOpcoes", 6f);
    }

    void MostrarTelaOpcoes()
    {
        // Exibir a tela com os bot�es
        vela.SetActive(false);
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