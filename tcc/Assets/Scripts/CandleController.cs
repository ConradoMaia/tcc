using UnityEngine;
using System.Collections;

public class CandleController : MonoBehaviour
{
    [Header("Configura��o do Animator")]
    public Animator candleAnimator; // Arraste o objeto CandleImage no Inspector

    [Header("Tempos (em segundos)")]
    public float exhaleTime = 5f; // Tempo para a anima��o de apagar a vela

    [Header("N�mero de ciclos")]
    public int totalCycles = 3; // Quantas vezes a anima��o ser� repetida

    private void Start()
    {
        // Inicia o processo de apagar a vela assim que a cena carrega
        StartCoroutine(CandleProcess());
    }

    private IEnumerator CandleProcess()
    {
        for (int cycle = 0; cycle < totalCycles; cycle++)
        {
            // Reproduz a anima��o de apagar a vela
            yield return StartCoroutine(ShowCandle());
        }
    }

    private IEnumerator ShowCandle()
    {
        // Reproduz a anima��o de apagar a vela
        candleAnimator.Play("CandleFadeOut");

        // Aguarda o tempo da anima��o
        yield return new WaitForSeconds(exhaleTime);
    }
}