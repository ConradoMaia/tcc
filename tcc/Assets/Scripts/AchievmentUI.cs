using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

public class AchievmentUI : MonoBehaviour
{
    [SerializeField] private GameObject achievementPanel;
    [SerializeField] private GameObject achievementItemPrefab;
    [SerializeField] private Transform achievementContainer;

    [SerializeField] private Button closeButton;
    [SerializeField] private Button openAchievementsButton;

    private void Start()
    {
        closeButton.onClick.AddListener(CloseAchievementPanel);
        openAchievementsButton.onClick.AddListener(OpenAchievementPanel);

        achievementPanel.SetActive(false);
    }

    public void OpenAchievementPanel()
    {
        foreach (Transform child in achievementContainer)
        {
            Destroy(child.gameObject);
        }

        if (AchievementSystem.Instance != null)
        {
            List<AchievementData> achievements = AchievementSystem.Instance.GetAllAchievements();
            foreach (AchievementData achievement in achievements)
            {
                GameObject item = Instantiate(achievementItemPrefab, achievementContainer);
                SetupAchievementItem(item, achievement);
            }
        }
        else
        {
            UnityEngine.Debug.LogWarning("AchievementSystem não encontrado!");
        }

        achievementPanel.SetActive(true);
        achievementPanel.transform.localScale = Vector3.zero;
        LeanTween.scale(achievementPanel, Vector3.one, 0.3f).setEaseOutBack();
    }

    private void CloseAchievementPanel()
    {
        LeanTween.scale(achievementPanel, Vector3.zero, 0.3f).setEaseInBack().setOnComplete(() => {
            achievementPanel.SetActive(false);
        });
    }

    private void SetupAchievementItem(GameObject item, AchievementData achievement)
    {
        AchievementItem achievementItemComponent = item.GetComponent<AchievementItem>();
        if (achievementItemComponent != null)
        {
            achievementItemComponent.Setup(achievement);
            return;
        }

        try
        {
            Transform iconTransform = item.transform.Find("Icon");
            Transform titleTransform = item.transform.Find("Title");
            Transform descriptionTransform = item.transform.Find("Description");
            Transform lockOverlayTransform = item.transform.Find("LockOverlay");

            if (iconTransform == null || titleTransform == null || 
                descriptionTransform == null || lockOverlayTransform == null)
            {
                Debug.LogError("Prefab de conquista não tem a estrutura esperada. Verifique se ele tem os filhos: Icon, Title, Description e LockOverlay");
                return;
            }

            UnityEngine.UI.Image icon = iconTransform.GetComponent<UnityEngine.UI.Image>();
            UnityEngine.UI.Text title = titleTransform.GetComponent<UnityEngine.UI.Text>();
            UnityEngine.UI.Text description = descriptionTransform.GetComponent<UnityEngine.UI.Text>();
            UnityEngine.UI.Image lockOverlay = lockOverlayTransform.GetComponent<UnityEngine.UI.Image>();

            if (icon != null) icon.sprite = achievement.icon;
            if (title != null) title.text = achievement.title;
            if (description != null) description.text = achievement.description;

            if (achievement.unlocked)
            {
                if (lockOverlay != null) lockOverlay.gameObject.SetActive(false);
                if (icon != null) icon.color = Color.white;
                if (title != null) title.color = Color.white;
                if (description != null) description.color = Color.white;
            }
            else
            {
                if (lockOverlay != null) lockOverlay.gameObject.SetActive(true);
                if (icon != null) icon.color = new Color(0.5f, 0.5f, 0.5f);
                if (title != null) title.color = new Color(0.5f, 0.5f, 0.5f);
                if (description != null) description.color = new Color(0.5f, 0.5f, 0.5f);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Erro ao configurar item de conquista: {e.Message}");
        }
    }
}