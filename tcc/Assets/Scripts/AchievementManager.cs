using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager Instance { get; private set; }

    [Header("Achievement UI")]
    [SerializeField] private GameObject achievementPanel;
    [SerializeField] private TextMeshProUGUI achievementTitle;
    [SerializeField] private TextMeshProUGUI achievementDescription;
    [SerializeField] private Image achievementIcon;
    [SerializeField] private float displayDuration = 3f;

    private Dictionary<string, bool> unlockedAchievements = new Dictionary<string, bool>();

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

    private void Start()
    {
        if (achievementPanel != null)
        {
            achievementPanel.SetActive(false);
        }
    }

    public void UnlockAchievement(string achievementId, string title, string description, Sprite icon = null)
    {
        if (!unlockedAchievements.ContainsKey(achievementId) || !unlockedAchievements[achievementId])
        {
            unlockedAchievements[achievementId] = true;
            SaveAchievements();
            ShowAchievementPopup(title, description, icon);
        }
    }

    private void ShowAchievementPopup(string title, string description, Sprite icon)
    {
        if (achievementPanel != null)
        {
            achievementTitle.text = title;
            achievementDescription.text = description;
            
            if (icon != null && achievementIcon != null)
            {
                achievementIcon.sprite = icon;
            }

            achievementPanel.SetActive(true);
            achievementPanel.transform.localScale = Vector3.zero;
            
            LeanTween.scale(achievementPanel, Vector3.one, 0.3f).setEaseOutBack();
            
            Invoke("HideAchievementPanel", displayDuration);
        }
    }

    private void HideAchievementPanel()
    {
        if (achievementPanel != null)
        {
            LeanTween.scale(achievementPanel, Vector3.zero, 0.3f).setEaseInBack()
                .setOnComplete(() => achievementPanel.SetActive(false));
        }
    }

    public bool IsAchievementUnlocked(string achievementId)
    {
        return unlockedAchievements.ContainsKey(achievementId) && unlockedAchievements[achievementId];
    }

    private void SaveAchievements()
    {
        foreach (var achievement in unlockedAchievements)
        {
            PlayerPrefs.SetInt("Achievement_" + achievement.Key, achievement.Value ? 1 : 0);
        }
        PlayerPrefs.Save();
    }

    private void LoadAchievements()
    {
        unlockedAchievements.Clear();
        string[] achievementIds = new string[] { "FirstMinigameComplete" };
        
        foreach (string id in achievementIds)
        {
            unlockedAchievements[id] = PlayerPrefs.GetInt("Achievement_" + id, 0) == 1;
        }
    }
}