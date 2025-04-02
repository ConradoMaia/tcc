using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    private UnityEngine.UI.Image dropZoneImage; // Especifica explicitamente o namespace correto

    public string correctItemName; // Nome do item correto para esta sombra

    private void Awake()
    {
        dropZoneImage = GetComponent<UnityEngine.UI.Image>(); // Especifica explicitamente o namespace correto
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedItem = eventData.pointerDrag;

        if (droppedItem != null)
        {
            // Verifica se o item solto é o correto
            if (droppedItem.name == correctItemName)
            {
                // Posiciona o item na sombra
                droppedItem.transform.SetParent(transform);
                droppedItem.transform.position = transform.position;

                UnityEngine.Debug.Log("Item colocado corretamente: " + droppedItem.name); // Especifica UnityEngine.Debug
            }
            else
            {
                // Retorna o item à posição original
                DraggableItem draggable = droppedItem.GetComponent<DraggableItem>();
                if (draggable != null)
                {
                    draggable.ResetPosition();
                }

                UnityEngine.Debug.Log("Item incorreto: " + droppedItem.name); // Especifica UnityEngine.Debug
            }
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