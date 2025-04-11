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

        // Desativar o controlador de respira��o at� que o usu�rio clique em continuar
        if (respiracaoController != null)
        {
            respiracaoController.enabled = false;
        }
    }

    // Fun��o chamada pelo bot�o "Continuar" no tutorial
    public void ContinuarParaRespiracao()
    {
        // Esconder o tutorial e mostrar a tela de respira��o
        tutorialPanel.SetActive(false);
        respiracaoPanel.SetActive(true);

        // Ativar o controlador de respira��o para iniciar o ciclo
        if (respiracaoController != null)
        {
            respiracaoController.enabled = true;
            respiracaoController.IniciarRespiracao();
        }
    }
}