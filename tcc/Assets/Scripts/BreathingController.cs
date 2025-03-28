using UnityEngine;
using System.Collections;

public class BreathingController : MonoBehaviour
{
    public Animator breathingAnimator;

    void Start()
    {
        StartCoroutine(BreathingCycle());
    }

    private IEnumerator BreathingCycle()
    {
        while (true)
        {
            // Reproduz a animação da flor
            breathingAnimator.Play("FlowerAnimation");
            yield return new WaitForSeconds(5f); // Tempo da animação da flor

            // Reproduz a animação da vela
            breathingAnimator.Play("CandleAnimation");
            yield return new WaitForSeconds(5f); // Tempo da animação da vela

        }
    }
}