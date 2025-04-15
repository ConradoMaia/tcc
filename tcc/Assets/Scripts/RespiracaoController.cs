using UnityEngine;
using UnityEngine.SceneManagement;

public class RespiracaoController : MonoBehaviour
{
    public GameObject flor;
    public GameObject caule;
    public GameObject velaCompleta;

    private Animator florAnimator;
    private VelaController velaController;
    private bool cicloIniciado = false;
    private int ciclosCompletos = 0;
    private const int TOTAL_CICLOS = 3;

    void Start()
    {
        florAnimator = flor.GetComponent<Animator>();
        velaController = velaCompleta.GetComponent<VelaController>();
        ResetarElementos();
    }

    private void ResetarElementos()
    {
        flor.SetActive(false);
        caule.SetActive(false);
        velaCompleta.SetActive(false);
    }

    public void IniciarRespiracao()
    {
        if (cicloIniciado) return;
        cicloIniciado = true;

        flor.SetActive(true);
        caule.SetActive(true);
        velaCompleta.SetActive(false);

        florAnimator.Play("FlorInchando");
        StartCoroutine(TransicaoParaVela());
    }

    private System.Collections.IEnumerator TransicaoParaVela()
    {
        yield return new WaitForSeconds(4f);

        flor.SetActive(false);
        caule.SetActive(false);
        velaCompleta.SetActive(true);

        if (velaController != null)
        {
            velaController.enabled = true;
        }

        yield return new WaitForSeconds(6f);

        CompletarCiclo();
    }

    private void CompletarCiclo()
    {
        ciclosCompletos++;
        cicloIniciado = false;

        if (ciclosCompletos < TOTAL_CICLOS)
        {
            ResetarElementos();
            IniciarRespiracao();
        }
        else
        {
            SceneManager.LoadScene("FearList");
        }
    }
}