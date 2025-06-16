using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [Header("Configurações")]
    public int totalItemsNeeded = 0;
    public PopupManager popupManager;

    private int correctItemsPlaced = 0;
    private List<string> placedItems = new List<string>();

    public void RegisterItemPlaced(string itemName)
    {
        if (!placedItems.Contains(itemName))
        {
            placedItems.Add(itemName);
            correctItemsPlaced++;
            UnityEngine.Debug.Log($"Item registrado: {itemName}. Total: {correctItemsPlaced}/{totalItemsNeeded}");

            CheckAllItemsPlaced();
        }
    }

    public void UnregisterItem(string itemName)
    {
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

            if (AchievementSystem.Instance != null)
            {
                UnityEngine.Debug.Log("Tentando desbloquear conquista: first_game_completed via GameManager");
                AchievementSystem.Instance.UnlockAchievement("first_game_completed");
                
                string currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
                string levelAchievementId = $"level_{currentSceneName}_completed";
                UnityEngine.Debug.Log($"Tentando desbloquear conquista do nível: {levelAchievementId}");
                AchievementSystem.Instance.UnlockAchievement(levelAchievementId);
            }
            else
            {
                UnityEngine.Debug.LogWarning("AchievementSystem não encontrado! A conquista não será desbloqueada.");
            }

            if (popupManager != null)
            {
                UnityEngine.Debug.Log("Mostrando popup de parabéns via GameManager");
                popupManager.ShowPopup();
            }
            else
            {
                UnityEngine.Debug.LogError("PopupManager não está referenciado no GameManager!");
            }
        }
    }
}