using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public GameObject tutorialPanel;
    public GameObject respiracaoPanel;
    public RespiracaoController respiracaoController;

    void Start()
    {
        tutorialPanel.SetActive(true);
        respiracaoPanel.SetActive(false);

        if (respiracaoController != null)
        {
            respiracaoController.enabled = false;
        }
    }

    public void ContinuarParaRespiracao()
    {
        tutorialPanel.SetActive(false);
        respiracaoPanel.SetActive(true);

        if (respiracaoController != null)
        {
            respiracaoController.enabled = true;
            respiracaoController.IniciarRespiracao();
        }
    }
}