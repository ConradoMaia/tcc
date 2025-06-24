using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableFearItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public int itemIndex;
    public FearTierList tierList;

    private Vector3 originalScale;
    public float dragScale = 1.1f;

    void Start()
    {
        originalScale = transform.localScale;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Efeito visual opcional
        transform.localScale = originalScale * dragScale;

        tierList.BeginDrag(itemIndex);
    }

    public void OnDrag(PointerEventData eventData)
    {
        tierList.OnDrag(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Restaura a escala original
        transform.localScale = originalScale;

        tierList.EndDrag();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Ignora cliques que foram parte de um arrasto
        if (Vector2.Distance(eventData.pressPosition, eventData.position) < 5)
            tierList.OnFearClicked(itemIndex);
    }
}