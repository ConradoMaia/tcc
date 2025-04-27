using UnityEngine;
using UnityEngine.SceneManagement;

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
        
        // Marca o nu00edvel como completado
        string currentSceneName = SceneManager.GetActiveScene().name;
        string completedKey = $"Level_{currentSceneName}_Completed";
        PlayerPrefs.SetInt(completedKey, 1);
        PlayerPrefs.Save();
        
        // Desbloqueia o pru00f3ximo nu00edvel
        LevelManager.UnlockNextLevel();
        
        // Desbloquear a conquista especu00edfica deste nu00edvel, se definida
        if (!string.IsNullOrEmpty(levelAchievementId) && AchievementSystem.Instance != null)
        {
            AchievementSystem.Instance.UnlockAchievement(levelAchievementId);
            Debug.Log($"Conquista desbloqueada: {levelAchievementId}");
            
            // Desbloquear a conquista geral de completar um jogo (apenas na primeira vez)
            AchievementSystem.Instance.UnlockAchievement("first_game_completed");
        }
        
        // Mostrar popup de parabu00e9ns
        if (popupManager != null)
        {
            Debug.Log("LevelCompletionManager: Mostrando popup");
            popupManager.ShowPopup();
        }
        else
        {
            Debug.LogError("LevelCompletionManager: PopupManager não está referenciado!");
        }
        
        levelCompleted = true;
    }
}