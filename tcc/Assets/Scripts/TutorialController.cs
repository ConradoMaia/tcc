using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public GameObject tutorialPanel;
    public GameObject respiracaoPanel;
    public RespiracaoController respiracaoController;

    void Start()
    {
        // Inicialmente, mostrar apenas o tutorial
        tutorialPanel.SetActive(true);
        respiracaoPanel.SetActive(false);

        // Desativar o controlador de respiração até que o usuário clique em continuar
        if (respiracaoController != null)
        {
            respiracaoController.enabled = false;
        }
    }

    // Função chamada pelo botão "Continuar" no tutorial
    public void ContinuarParaRespiracao()
    {
        // Esconder o tutorial e mostrar a tela de respiração
        tutorialPanel.SetActive(false);
        respiracaoPanel.SetActive(true);

        // Ativar o controlador de respiração para iniciar o ciclo
        if (respiracaoController != null)
        {
            respiracaoController.enabled = true;
            respiracaoController.IniciarRespiracao();
        }
    }
}