using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    private UnityEngine.UI.Image dropZoneImage;

    [Header("Configurações")]
    public bool validateCorrectItem = true; // Define se a validação de item correto será usada
    public string correctItemName; // Nome do item correto (usado apenas se validateCorrectItem for true)
    public GameManager gameManager; // Referência opcional ao GameManager

    private GameObject currentItem; // Referência ao item atualmente na DropZone

    private void Awake()
    {
        dropZoneImage = GetComponent<UnityEngine.UI.Image>(); // Obtém o componente Image da área de soltura
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedItem = eventData.pointerDrag;

        if (droppedItem != null)
        {
            if (validateCorrectItem)
            {
                // Valida se o item é o correto
                if (droppedItem.name == correctItemName)
                {
                    HandleItemPlacement(droppedItem);
                }
                else
                {
                    // Retorna o item à posição original
                    DraggableItem draggable = droppedItem.GetComponent<DraggableItem>();
                    if (draggable != null)
                    {
                        draggable.ResetPosition();
                    }

                    UnityEngine.Debug.Log("Item incorreto: " + droppedItem.name);
                }
            }
            else
            {
                // Sem validação: permite organizar livremente
                HandleItemPlacement(droppedItem);
            }
        }
    }

    private void HandleItemPlacement(GameObject newItem)
    {
        // Verifica se o item já estava em outra DropZone
        DropZone previousDropZone = newItem.transform.parent?.GetComponent<DropZone>();
        if (previousDropZone != null && previousDropZone != this)
        {
            UnityEngine.Debug.Log($"Item '{newItem.name}' foi movido de '{previousDropZone.name}' para '{name}'");
            previousDropZone.ClearCurrentItem(); // Limpa o item da DropZone anterior
        }

        // Se já houver um item na DropZone atual, retorna o item anterior à posição original
        if (currentItem != null)
        {
            DraggableItem draggable = currentItem.GetComponent<DraggableItem>();
            if (draggable != null)
            {
                draggable.ResetPosition();
            }
        }

        // Posiciona o novo item na DropZone
        currentItem = newItem;
        currentItem.transform.SetParent(transform);
        currentItem.transform.position = transform.position;

        UnityEngine.Debug.Log("Item colocado na área: " + currentItem.name);
    }

    public void ClearCurrentItem()
    {
        currentItem = null; // Limpa a referência ao item atual
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Destaca a área de soltura ao passar o cursor
        if (dropZoneImage != null)
        {
            dropZoneImage.color = Color.green;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Restaura a cor original ao sair da área
        if (dropZoneImage != null)
        {
            dropZoneImage.color = Color.white;
        }
    }
}