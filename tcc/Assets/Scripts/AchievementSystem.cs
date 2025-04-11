using UnityEngine;
using System.Collections.Generic;

// Classe de dados para armazenar informações sobre uma conquista
[System.Serializable]
public class AchievementData
{
    public string id;
    public string title;
    public string description;
    public bool unlocked;
    public Sprite icon;
}

// Classe auxiliar para serialização de listas
[System.Serializable]
public class SerializableStringList
{
    public List<string> items = new List<string>();
}

// Componente principal que gerencia as conquistas
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
        AchievementData achievement = achievements.Find(a => a.id == achievementId);
        if (achievement != null && !achievement.unlocked)
        {
            achievement.unlocked = true;
            SaveAchievements();

            // Mostrar notificação de conquista desbloqueada
            UnityEngine.Debug.Log($"Conquista desbloqueada: {achievement.title}");

            // Aqui você pode adicionar código para mostrar uma notificação visual
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
        // Cria uma lista simples de IDs de conquistas desbloqueadas
        List<string> unlockedIds = new List<string>();
        foreach (AchievementData achievement in achievements)
        {
            if (achievement.unlocked)
            {
                unlockedIds.Add(achievement.id);
            }
        }

        // Converte para JSON e salva
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

            // Atualiza o estado das conquistas
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