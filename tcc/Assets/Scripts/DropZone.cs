using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    private UnityEngine.UI.Image dropZoneImage;
    public string correctItemName;
    public GameManager gameManager;

    private void Awake()
    {
        dropZoneImage = GetComponent<UnityEngine.UI.Image>(); // Especifica explicitamente o namespace correto
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedItem = eventData.pointerDrag;

        if (droppedItem != null)
        {
            // Verifica se o item solto � o correto
            if (droppedItem.name == correctItemName)
            {
                // Posiciona o item na sombra
                droppedItem.transform.SetParent(transform);
                droppedItem.transform.position = transform.position;

                // Notifica o GameManager que um item foi colocado corretamente
                if (gameManager != null)
                {
                    gameManager.ItemPlacedCorrectly();
                }

                UnityEngine.Debug.Log("Item colocado corretamente: " + droppedItem.name);
            }
            else
            {
                // Retorna o item � posi��o original
                DraggableItem draggable = droppedItem.GetComponent<DraggableItem>();
                if (draggable != null)
                {
                    draggable.ResetPosition();
                }

                UnityEngine.Debug.Log("Item incorreto: " + droppedItem.name);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Destaca a �rea de soltura ao passar o cursor
        dropZoneImage.color = Color.green;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Restaura a cor original ao sair da �rea
        dropZoneImage.color = Color.white;
    }
}