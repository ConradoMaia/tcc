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
    private bool cicloIniciado = false; // Para garantir que o ciclo s� inicie ap�s o bot�o "Continuar"

    void Start()
    {
        // Obter os componentes Animator e o controlador da vela
        florAnimator = flor.GetComponent<Animator>();
        velaController = velaCompleta.GetComponent<VelaController>();

        // Certifique-se de que todos os elementos est�o desativados inicialmente
        ResetarElementos();
    }

    private void ResetarElementos()
    {
        // Desativar todos os elementos no in�cio
        flor.SetActive(false);
        caule.SetActive(false);
        velaCompleta.SetActive(false);
        telaOpcoes.SetActive(false);
    }

    public void IniciarRespiracao()
    {
        if (cicloIniciado) return; // Evitar que o ciclo seja iniciado mais de uma vez
        cicloIniciado = true;

        // Ativar a flor e o caule, e iniciar a anima��o de inspira��o
        flor.SetActive(true);
        caule.SetActive(true);
        velaCompleta.SetActive(false);
        telaOpcoes.SetActive(false);

        // Reproduzir a anima��o da flor
        florAnimator.Play("FlorInchando");

        // Usar uma Coroutine para gerenciar a transi��o
        StartCoroutine(TransicaoParaVela());
    }

    private System.Collections.IEnumerator TransicaoParaVela()
    {
        // Esperar 4 segundos para a anima��o da flor
        yield return new WaitForSeconds(4f);

        // Desativar a flor e o caule, e ativar a vela
        flor.SetActive(false);
        caule.SetActive(false);
        velaCompleta.SetActive(true);

        // Iniciar a anima��o da chama da vela
        if (velaController != null)
        {
            velaController.enabled = true; // Ativar o script da vela
        }

        // Esperar 6 segundos para a anima��o da vela
        yield return new WaitForSeconds(6f);

        // Mostrar a tela de op��es
        MostrarTelaOpcoes();
    }

    private void MostrarTelaOpcoes()
    {
        // Desativar a vela e exibir a tela com os bot�es
        velaCompleta.SetActive(false);
        telaOpcoes.SetActive(true);
    }

    // Fun��o chamada pelo bot�o "Repetir"
    public void RepetirTecnica()
    {
        // Reiniciar o ciclo de respira��o
        cicloIniciado = false; // Permitir que o ciclo seja reiniciado
        ResetarElementos();
        IniciarRespiracao();
    }

    // Fun��o chamada pelo bot�o "Continuar"
    public void Continuar()
    {
        // Carregar a pr�xima cena ou etapa
        SceneManager.LoadScene("FearList");
    }
}