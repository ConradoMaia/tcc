using UnityEngine;
using UnityEngine.UI;

public class AchievementItem : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Text title;
    [SerializeField] private Text description;
    [SerializeField] private Image lockOverlay;

    public void Setup(AchievementData achievement)
    {
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