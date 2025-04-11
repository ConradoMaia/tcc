using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

public class AchievementUI : MonoBehaviour
{
    [SerializeField] private GameObject achievementPanel;
    [SerializeField] private GameObject achievementItemPrefab;
    [SerializeField] private Transform achievementContainer;

    [SerializeField] private Button closeButton;
    [SerializeField] private Button openAchievementsButton; // Botão no menu principal

    private void Start()
    {
        // Configurar botões
        closeButton.onClick.AddListener(CloseAchievementPanel);
        openAchievementsButton.onClick.AddListener(OpenAchievementPanel);

        // Inicialmente, o painel está fechado
        achievementPanel.SetActive(false);
    }

    public void OpenAchievementPanel()
    {
        // Limpar itens existentes
        foreach (Transform child in achievementContainer)
        {
            Destroy(child.gameObject);
        }

        // Preencher com conquistas
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

        // Mostrar o painel com animação
        achievementPanel.SetActive(true);
        achievementPanel.transform.localScale = Vector3.zero;
        LeanTween.scale(achievementPanel, Vector3.one, 0.3f).setEaseOutBack();
    }

    private void CloseAchievementPanel()
    {
        // Fechar o painel com animação
        LeanTween.scale(achievementPanel, Vector3.zero, 0.3f).setEaseInBack().setOnComplete(() => {
            achievementPanel.SetActive(false);
        });
    }

    private void SetupAchievementItem(GameObject item, AchievementData achievement)
    {
        // Configurar o visual do item de conquista
        UnityEngine.UI.Image icon = item.transform.Find("Icon").GetComponent<UnityEngine.UI.Image>();
        UnityEngine.UI.Text title = item.transform.Find("Title").GetComponent<UnityEngine.UI.Text>();
        UnityEngine.UI.Text description = item.transform.Find("Description").GetComponent<UnityEngine.UI.Text>();
        UnityEngine.UI.Image lockOverlay = item.transform.Find("LockOverlay").GetComponent<UnityEngine.UI.Image>();

        // Definir dados
        icon.sprite = achievement.icon;
        title.text = achievement.title;
        description.text = achievement.description;

        // Definir estado (bloqueado/desbloqueado)
        if (achievement.unlocked)
        {
            lockOverlay.gameObject.SetActive(false);
            icon.color = Color.white;
            title.color = Color.white;
            description.color = Color.white;
        }
        else
        {
            lockOverlay.gameObject.SetActive(true);
            icon.color = new Color(0.5f, 0.5f, 0.5f);
            title.color = new Color(0.5f, 0.5f, 0.5f);
            description.color = new Color(0.5f, 0.5f, 0.5f);
        }
    }
}