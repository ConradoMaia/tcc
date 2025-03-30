using UnityEngine;
using UnityEngine.SceneManagement;

public class RespiracaoController : MonoBehaviour
{
    // Referências aos objetos da cena
    public GameObject flor; // Objeto da flor
    public GameObject caule; // Objeto do caule
    public GameObject vela; // Objeto da vela
    public GameObject telaOpcoes; // Tela com os botões de opções

    private Animator florAnimator; // Animator da flor
    private Animator velaAnimator; // Animator da vela

    void Start()
    {
        // Obter os componentes Animator dos objetos
        florAnimator = flor.GetComponent<Animator>();
        velaAnimator = vela.GetComponent<Animator>();

        // Iniciar o ciclo de respiração
        IniciarRespiracao();
    }

    void IniciarRespiracao()
    {
        // Ativar a flor e o caule, e iniciar a animação de inspiração
        flor.SetActive(true);
        caule.SetActive(true);
        vela.SetActive(false);
        telaOpcoes.SetActive(false);

        // Reproduzir a animação da flor
        florAnimator.Play("FlorInchando");

        // Agendar a transição para a vela após a animação da flor (4 segundos)
        Invoke("IniciarExpiracao", 4f);
    }

    void IniciarExpiracao()
    {
        // Desativar a flor e o caule, e ativar a vela
        flor.SetActive(false);
        caule.SetActive(false);
        vela.SetActive(true);

        // Reproduzir a animação da vela
        velaAnimator.Play("VelaApagando");

        // Agendar a exibição da tela de opções após a animação da vela (6 segundos)
        Invoke("MostrarTelaOpcoes", 6f);
    }

    void MostrarTelaOpcoes()
    {
        // Exibir a tela com os botões
        vela.SetActive(false);
        telaOpcoes.SetActive(true);
    }

    // Função chamada pelo botão "Repetir"
    public void RepetirTecnica()
    {
        // Reiniciar o ciclo de respiração
        IniciarRespiracao();
    }

    // Função chamada pelo botão "Continuar"
    public void Continuar()
    {
        // Carregar a próxima cena ou etapa
        SceneManager.LoadScene("FearList");
    }
}