using UnityEngine;
using System.Collections.Generic;

public class AchievementChecker : MonoBehaviour
{
    [SerializeField] private string[] levelSceneNames;
    [SerializeField] private string allLevelsCompletedAchievementId = "all_levels_completed";

    private void Start()
    {
        // Verifica se todos os nu00edveis foram completados
        CheckAllLevelsCompleted();
    }

    public void CheckAllLevelsCompleted()
    {
        bool allCompleted = true;

        // Verifica cada nu00edvel
        foreach (string levelName in levelSceneNames)
        {
            string completedKey = $"Level_{levelName}_Completed";
            if (PlayerPrefs.GetInt(completedKey, 0) != 1)
            {
                allCompleted = false;
                break;
            }
        }

        // Se todos os nu00edveis foram completados, desbloqueia a conquista
        if (allCompleted && AchievementSystem.Instance != null)
        {
            AchievementSystem.Instance.UnlockAchievement(allLevelsCompletedAchievementId);
        }
    }
}