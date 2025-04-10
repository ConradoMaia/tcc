using UnityEngine;
using UnityEngine.UI;

public class ThermometerManager : MonoBehaviour
{
    public RectTransform thermometerFill;
    public RectTransform[] fearObjects;

    private void Update()
    {
        UpdateThermometer();
    }

    private void UpdateThermometer()
    {
        if (fearObjects.Length == 0 || thermometerFill == null) return;

        // Calcula a posi��o m�dia dos objetos
        float totalHeight = 0f;
        foreach (var obj in fearObjects)
        {
            totalHeight += obj.anchoredPosition.y;
        }

        float averageHeight = totalHeight / fearObjects.Length;

        // Atualiza o preenchimento do term�metro
        float normalizedHeight = Mathf.InverseLerp(-300f, 300f, averageHeight); // Ajuste os valores conforme necess�rio
        thermometerFill.localScale = new Vector3(1f, normalizedHeight, 1f);
    }
}