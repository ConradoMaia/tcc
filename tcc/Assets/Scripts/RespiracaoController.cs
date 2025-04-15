using UnityEngine;
using UnityEngine.SceneManagement;

public class RespiracaoController : MonoBehaviour
{
    // Referências aos objetos da cena
    public GameObject flor; // Objeto da flor
    public GameObject caule; // Objeto do caule
    public GameObject velaCompleta; // Objeto pai da vela (contém a base e a chama)
    public GameObject telaOpcoes; // Tela com os botões de opções

    private Animator florAnimator; // Animator da flor
    private VelaController velaController; // Script para controlar a vela
    private bool cicloIniciado = false; // Para garantir que o ciclo só inicie após o botão "Continuar"

    void Start()
    {
        // Obter os componentes Animator e o controlador da vela
        florAnimator = flor.GetComponent<Animator>();
        velaController = velaCompleta.GetComponent<VelaController>();

        // Certifique-se de que todos os elementos estão desativados inicialmente
        ResetarElementos();
    }

    private void ResetarElementos()
    {
        // Desativar todos os elementos no início
        flor.SetActive(false);
        caule.SetActive(false);
        velaCompleta.SetActive(false);
        telaOpcoes.SetActive(false);
    }

    public void IniciarRespiracao()
    {
        if (cicloIniciado) return; // Evitar que o ciclo seja iniciado mais de uma vez
        cicloIniciado = true;

        // Ativar a flor e o caule, e iniciar a animação de inspiração
        flor.SetActive(true);
        caule.SetActive(true);
        velaCompleta.SetActive(false);
        telaOpcoes.SetActive(false);

        // Reproduzir a animação da flor
        florAnimator.Play("FlorInchando");

        // Usar uma Coroutine para gerenciar a transição
        StartCoroutine(TransicaoParaVela());
    }

    private System.Collections.IEnumerator TransicaoParaVela()
    {
        // Esperar 4 segundos para a animação da flor
        yield return new WaitForSeconds(4f);

        // Desativar a flor e o caule, e ativar a vela
        flor.SetActive(false);
        caule.SetActive(false);
        velaCompleta.SetActive(true);

        // Iniciar a animação da chama da vela
        if (velaController != null)
        {
            velaController.enabled = true; // Ativar o script da vela
        }

        // Esperar 6 segundos para a animação da vela
        yield return new WaitForSeconds(6f);

        // Mostrar a tela de opções
        MostrarTelaOpcoes();
    }

    private void MostrarTelaOpcoes()
    {
        // Desativar a vela e exibir a tela com os botões
        velaCompleta.SetActive(false);
        telaOpcoes.SetActive(true);
    }

    // Função chamada pelo botão "Repetir"
    public void RepetirTecnica()
    {
        // Reiniciar o ciclo de respiração
        cicloIniciado = false; // Permitir que o ciclo seja reiniciado
        ResetarElementos();
        IniciarRespiracao();
    }

    // Função chamada pelo botão "Continuar"
    public void Continuar()
    {
        // Carregar a próxima cena ou etapa
        SceneManager.LoadScene("FearList");
    }
}