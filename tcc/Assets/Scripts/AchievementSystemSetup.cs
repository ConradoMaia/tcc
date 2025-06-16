using UnityEngine;

public class AchievementSystemSetup : MonoBehaviour
{
    [SerializeField] private GameObject achievementSystemPrefab;

    void Awake()
    {
        if (AchievementSystem.Instance == null && achievementSystemPrefab != null)
        {
            Debug.Log("Instanciando AchievementSystem");
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