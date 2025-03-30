using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform originalParent;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private RectTransform parentRectTransform; // Referência ao painel pai
    private int originalSiblingIndex;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        parentRectTransform = originalParent.GetComponent<RectTransform>(); // Obtém o RectTransform do painel pai
        originalSiblingIndex = transform.GetSiblingIndex();

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

        // Detecta a posição do item em relação aos outros no contêiner
        for (int i = 0; i < originalParent.childCount; i++)
        {
            Transform sibling = originalParent.GetChild(i);

            if (sibling == transform) continue;

            // Verifica se o item arrastado está acima ou abaixo de outro item
            if (rectTransform.position.y > sibling.position.y)
            {
                transform.SetSiblingIndex(i);
                break;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Restaura a opacidade
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        // Restaura o tamanho original do item
        LeanTween.scale(gameObject, Vector3.one, 0.2f).setEaseOutBack();
    }
}