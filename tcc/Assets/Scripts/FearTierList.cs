using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class FearTierList : MonoBehaviour
{
    [System.Serializable]
    public class FearItem
    {
        public string fearName;
        public Sprite iconImage;
        public Sprite detailImage;
        public RectTransform itemTransform;
        public UnityEngine.UI.Image imageComponent;
        public bool showingDetail;
    }

    public List<FearItem> fears = new List<FearItem>();
    public float spacingBetweenItems = 120f;
    public float dragThreshold = 50f; // Distância mínima para considerar troca de posição

    private FearItem draggedItem;
    private Vector2 dragStartPosition;
    private int draggedIndex;
    private Vector2[] originalPositions;

    void Start()
    {
        // Armazena as posições originais
        originalPositions = new Vector2[fears.Count];
        ArrangeFears();
    }

    // Organiza os medos verticalmente com espaçamento adequado
    private void ArrangeFears()
    {
        float startY = (fears.Count - 1) * spacingBetweenItems / 2; // Centraliza verticalmente

        for (int i = 0; i < fears.Count; i++)
        {
            Vector2 position = new Vector2(0, startY - i * spacingBetweenItems);
            fears[i].itemTransform.anchoredPosition = position;

            // Armazena a posição original para referência
            if (originalPositions != null && i < originalPositions.Length)
                originalPositions[i] = position;
        }
    }

    public void OnFearClicked(int index)
    {
        FearItem fear = fears[index];
        fear.showingDetail = !fear.showingDetail;
        fear.imageComponent.sprite = fear.showingDetail ? fear.detailImage : fear.iconImage;
    }

    public void BeginDrag(int index)
    {
        draggedItem = fears[index];
        draggedIndex = index;
        dragStartPosition = draggedItem.itemTransform.anchoredPosition;

        // Opcional: Efeito visual para o item sendo arrastado
        draggedItem.itemTransform.SetAsLastSibling(); // Traz para frente
    }

    public void OnDrag(BaseEventData eventData)
    {
        if (draggedItem == null) return;

        PointerEventData pointerData = eventData as PointerEventData;

        // Atualiza apenas a posição Y do item arrastado
        draggedItem.itemTransform.position = new Vector3(
            draggedItem.itemTransform.position.x,
            pointerData.position.y,
            draggedItem.itemTransform.position.z);
    }

    // Modifique o método EndDrag() no script FearTierList.cs
    public void EndDrag()
    {
        if (draggedItem == null) return;

        // Encontra a posição mais próxima para soltar
        int closestIndex = FindClosestPositionIndex(draggedItem.itemTransform.anchoredPosition);

        if (closestIndex != draggedIndex)
        {
            // Reordena a lista
            FearItem temp = fears[draggedIndex];
            fears.RemoveAt(draggedIndex);
            fears.Insert(closestIndex, temp);

            // Atualiza os índices em todos os DraggableFearItem
            UpdateAllItemIndices();
        }

        // Reorganiza todos os itens
        ArrangeFears();

        draggedItem = null;
    }

    // Adicione este novo método para atualizar os índices
    private void UpdateAllItemIndices()
    {
        for (int i = 0; i < fears.Count; i++)
        {
            DraggableFearItem draggableItem = fears[i].itemTransform.GetComponent<DraggableFearItem>();
            if (draggableItem != null)
            {
                draggableItem.itemIndex = i;
            }
        }
    }

    // Encontra o índice da posição mais próxima
    private int FindClosestPositionIndex(Vector2 position)
    {
        float minDistance = float.MaxValue;
        int closestIndex = draggedIndex;

        for (int i = 0; i < fears.Count; i++)
        {
            if (i == draggedIndex) continue;

            float distance = Mathf.Abs(position.y - originalPositions[i].y);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestIndex = i;
            }
        }

        // Se a distância for muito grande, mantém na posição original
        if (minDistance > dragThreshold)
            return draggedIndex;

        return closestIndex;
    }
}