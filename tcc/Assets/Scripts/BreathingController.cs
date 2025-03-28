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
            // Reproduz a anima��o da flor
            breathingAnimator.Play("FlowerAnimation");
            yield return new WaitForSeconds(5f); // Tempo da anima��o da flor

            // Reproduz a anima��o da vela
            breathingAnimator.Play("CandleAnimation");
            yield return new WaitForSeconds(5f); // Tempo da anima��o da vela

        }
    }
}