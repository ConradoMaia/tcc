using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AchievementNotification : MonoBehaviour
{
    public static AchievementNotification Instance { get; private set; }

    [SerializeField] private GameObject notificationPanel;
    [SerializeField] private Image achievementIcon;
    [SerializeField] private Text achievementTitle;
    [SerializeField] private float displayTime = 3f;
    [SerializeField] private AudioClip unlockSound;

    private Queue achievementQueue = new Queue();
    private bool isDisplaying = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            notificationPanel.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowNotification(AchievementData achievement)
    {
        achievementQueue.Enqueue(achievement);
        
        if (!isDisplaying)
        {
            StartCoroutine(DisplayNextNotification());
        }
    }

    private IEnumerator DisplayNextNotification()
    {
        isDisplaying = true;

        while (achievementQueue.Count > 0)
        {
            AchievementData achievement = (AchievementData)achievementQueue.Dequeue();

            achievementIcon.sprite = achievement.icon;
            achievementTitle.text = $"Conquista Desbloqueada!\n{achievement.title}";

            if (unlockSound != null)
            {
                AudioSource.PlayClipAtPoint(unlockSound, Camera.main.transform.position);
            }

            notificationPanel.SetActive(true);
            notificationPanel.transform.localScale = Vector3.zero;
            LeanTween.scale(notificationPanel, Vector3.one, 0.3f).setEaseOutBack();

            yield return new WaitForSeconds(displayTime);

            LeanTween.scale(notificationPanel, Vector3.zero, 0.3f).setEaseInBack();
            yield return new WaitForSeconds(0.3f);
            notificationPanel.SetActive(false);

            yield return new WaitForSeconds(0.5f);
        }

        isDisplaying = false;
    }
}