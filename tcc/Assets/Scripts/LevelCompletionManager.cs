using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelCompletionManager : MonoBehaviour
{
    [SerializeField] private PopupManager popupManager;
    [SerializeField] private string levelAchievementId; // ID da conquista para este nu00edvel
    [SerializeField] private string nextLevelScene; // Cena do pru00f3ximo nu00edvel (opcional)
    
    private bool levelCompleted = false;
    
    private void Start()
    {
        // Verifica se o nu00edvel ju00e1 foi completado antes
        string currentSceneName = SceneManager.GetActiveScene().name;
        string completedKey = $"Level_{currentSceneName}_Completed";
        levelCompleted = PlayerPrefs.GetInt(completedKey, 0) == 1;
        
        // Configura a cena para onde o popup deve voltar
        if (popupManager != null && !string.IsNullOrEmpty(nextLevelScene))
        {
            popupManager.nextSceneName = nextLevelScene;
        }
    }
    
    public void CompleteLevel()
    {
        if (levelCompleted) return;
        
        Debug.Log("LevelCompletionManager: Completando nível");
        
        // Marca o nível como completado
        string currentSceneName = SceneManager.GetActiveScene().name;
        string completedKey = $"Level_{currentSceneName}_Completed";
        PlayerPrefs.SetInt(completedKey, 1);
        PlayerPrefs.Save();
        
        // Desbloqueia o próximo nível
        LevelManager.UnlockNextLevel();
        
        // Mostrar popup de parabéns primeiro
        if (popupManager != null)
        {
            Debug.Log("LevelCompletionManager: Mostrando popup");
            popupManager.ShowPopup();
        }
        else
        {
            Debug.LogError("LevelCompletionManager: PopupManager não está referenciado!");
        }

        // Desbloquear conquistas após um pequeno delay para não sobrepor as animações
        StartCoroutine(UnlockAchievementsWithDelay());
        
        levelCompleted = true;
    }

    private System.Collections.IEnumerator UnlockAchievementsWithDelay()
    {
        // Aguarda um pouco para dar tempo do popup aparecer
        yield return new WaitForSeconds(1.5f);

        if (AchievementSystem.Instance == null)
        {
            Debug.LogError("AchievementSystem não encontrado! Verifique se ele existe na cena.");
            yield break;
        }

        // Desbloquear a conquista específica deste nível, se definida
        if (!string.IsNullOrEmpty(levelAchievementId))
        {
            AchievementSystem.Instance.UnlockAchievement(levelAchievementId);
            Debug.Log($"Conquista desbloqueada: {levelAchievementId}");
            
            // Aguarda um pouco entre as conquistas
            yield return new WaitForSeconds(2f);
            
            // Desbloquear a conquista geral de completar um jogo (apenas na primeira vez)
            AchievementSystem.Instance.UnlockAchievement("first_game_completed");
        }
    }
}