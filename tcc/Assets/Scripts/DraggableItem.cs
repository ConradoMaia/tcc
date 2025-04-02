using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform originalParent;
    private Vector3 originalPosition;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private RectTransform parentRectTransform; // Referência ao painel pai

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        originalPosition = transform.position;
        parentRectTransform = originalParent.GetComponent<RectTransform>(); // Obtém o RectTransform do painel pai

        // Reduz a opacidade para dar feedback visual
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;

        // Aumenta o tamanho do item ao começar a arrastar
        LeanTween.scale(gameObject, Vector3.one * 1.1f, 0.2f).setEaseOutBack();
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Atualiza a posição do item com o cursor
        rectTransform.position = eventData.position;

        // Limita o movimento do item para dentro do painel
        Vector3[] worldCorners = new Vector3[4];
        parentRectTransform.GetWorldCorners(worldCorners);

        Vector3 clampedPosition = rectTransform.position;
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, worldCorners[0].y, worldCorners[1].y); // Limita no eixo Y
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, worldCorners[0].x, worldCorners[2].x); // Limita no eixo X
        rectTransform.position = clampedPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Restaura a opacidade
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        // Restaura o tamanho original do item
        LeanTween.scale(gameObject, Vector3.one, 0.2f).setEaseOutBack();
    }

    public void ResetPosition()
    {
        // Retorna o item à posição original
        transform.position = originalPosition;
        transform.SetParent(originalParent);
    }
}