using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [Header("Configura��es")]
    public int totalItemsNeeded = 0; // Total de itens que precisam ser colocados
    public PopupManager popupManager; // Refer�ncia ao PopupManager

    private int correctItemsPlaced = 0; // Contador de itens colocados corretamente
    private List<string> placedItems = new List<string>(); // Lista para rastrear itens j� colocados

    public void RegisterItemPlaced(string itemName)
    {
        // Verifica se o item j� foi contabilizado antes
        if (!placedItems.Contains(itemName))
        {
            placedItems.Add(itemName);
            correctItemsPlaced++;
            UnityEngine.Debug.Log($"Item registrado: {itemName}. Total: {correctItemsPlaced}/{totalItemsNeeded}");

            // Verifica se todos os itens foram colocados
            CheckAllItemsPlaced();
        }
    }

    public void UnregisterItem(string itemName)
    {
        // Remove o item da lista quando ele for retirado de uma DropZone
        if (placedItems.Contains(itemName))
        {
            placedItems.Remove(itemName);
            correctItemsPlaced--;
            UnityEngine.Debug.Log($"Item removido: {itemName}. Total: {correctItemsPlaced}/{totalItemsNeeded}");
        }
    }

    private void CheckAllItemsPlaced()
    {
        if (correctItemsPlaced >= totalItemsNeeded)
        {
            UnityEngine.Debug.Log("Todos os itens foram colocados corretamente!");

            // Desbloquear a conquista
            if (AchievementSystem.Instance != null)
            {
                AchievementSystem.Instance.UnlockAchievement("first_game_completed");
                UnityEngine.Debug.Log("Tentando desbloquear conquista: first_game_completed");
            }
            else
            {
                UnityEngine.Debug.LogWarning("AchievementSystem n�o encontrado! A conquista n�o ser� desbloqueada.");
            }

            // Mostrar popup de parab�ns
            if (popupManager != null)
            {
                popupManager.ShowPopup();
            }
            else
            {
                UnityEngine.Debug.LogError("PopupManager n�o est� referenciado no GameManager!");
            }
        }
    }
}