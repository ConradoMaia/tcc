using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Image dropZoneImage;

    private void Awake()
    {
        dropZoneImage = GetComponent<Image>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedItem = eventData.pointerDrag;
        if (droppedItem != null)
        {
            droppedItem.transform.SetParent(transform); // Define o novo pai do item arrastado
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Destaca a área de soltura ao passar o cursor
        dropZoneImage.color = Color.green;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Restaura a cor original ao sair da área
        dropZoneImage.color = Color.white;
    }
}