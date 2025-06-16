using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class AchievementData
{
    public string id;
    public string title;
    public string description;
    public bool unlocked;
    public Sprite icon;
}

[System.Serializable]
public class SerializableStringList
{
    public List<string> items = new List<string>();
}

public class AchievementSystem : MonoBehaviour
{
    public static AchievementSystem Instance { get; private set; }

    [SerializeField] private List<AchievementData> achievements = new List<AchievementData>();
    private const string SAVE_KEY = "PlayerAchievements";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadAchievements();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UnlockAchievement(string achievementId)
    {
        Debug.Log($"Tentando desbloquear conquista: {achievementId}");
        
        AchievementData achievement = achievements.Find(a => a.id == achievementId);
        if (achievement != null)
        {
            if (!achievement.unlocked)
            {
                achievement.unlocked = true;
                SaveAchievements();

                Debug.Log($"Conquista desbloqueada: {achievement.title}");

                if (AchievementNotification.Instance != null)
                {
                    AchievementNotification.Instance.ShowNotification(achievement);
                }
                else
                {
                    Debug.LogWarning("AchievementNotification.Instance u00e9 nulo. A notificau00e7u00e3o visual nu00e3o seru00e1 exibida.");
                }
            }
            else
            {
                Debug.Log($"Conquista '{achievement.title}' ju00e1 estu00e1 desbloqueada.");
            }
        }
        else
        {
            Debug.LogError($"Conquista com ID '{achievementId}' nu00e3o encontrada! Verifique se ela estu00e1 definida no Inspector.");
            
            Debug.Log("Conquistas disponu00edveis:");
            foreach (var a in achievements)
            {
                Debug.Log($"- ID: {a.id}, Tu00edtulo: {a.title}");
            }
        }
    }

    public List<AchievementData> GetAllAchievements()
    {
        return achievements;
    }

    public bool IsAchievementUnlocked(string achievementId)
    {
        AchievementData achievement = achievements.Find(a => a.id == achievementId);
        return achievement != null && achievement.unlocked;
    }

    private void SaveAchievements()
    {
        List<string> unlockedIds = new List<string>();
        foreach (AchievementData achievement in achievements)
        {
            if (achievement.unlocked)
            {
                unlockedIds.Add(achievement.id);
            }
        }

        SerializableStringList serializableList = new SerializableStringList();
        serializableList.items = unlockedIds;
        string json = JsonUtility.ToJson(serializableList);
        PlayerPrefs.SetString(SAVE_KEY, json);
        PlayerPrefs.Save();
    }

    private void LoadAchievements()
    {
        if (PlayerPrefs.HasKey(SAVE_KEY))
        {
            string json = PlayerPrefs.GetString(SAVE_KEY);
            SerializableStringList unlockedIds = JsonUtility.FromJson<SerializableStringList>(json);

            if (unlockedIds != null && unlockedIds.items != null)
            {
                foreach (string id in unlockedIds.items)
                {
                    AchievementData achievement = achievements.Find(a => a.id == id);
                    if (achievement != null)
                    {
                        achievement.unlocked = true;
                    }
                }
            }
        }
    }
}