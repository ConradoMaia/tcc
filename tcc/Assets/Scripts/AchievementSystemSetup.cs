using UnityEngine;

// Este script deve ser adicionado à cena inicial do jogo
public class AchievementSystemSetup : MonoBehaviour
{
    [SerializeField] private GameObject achievementSystemPrefab;

    void Awake()
    {
        // Verifica se o AchievementSystem já existe
        if (AchievementSystem.Instance == null && achievementSystemPrefab != null)
        {
            Debug.Log("Instanciando AchievementSystem");
            // Instancia o AchievementSystem
            Instantiate(achievementSystemPrefab);
        }
        else if (AchievementSystem.Instance != null)
        {
            Debug.Log("AchievementSystem já existe");
        }
        else if (achievementSystemPrefab == null)
        {
            Debug.LogError("achievementSystemPrefab não está configurado no AchievementSystemSetup!");
        }
    }
}