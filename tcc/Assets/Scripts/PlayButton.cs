using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    public LevelManager levelManager;
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnPlayButtonClicked);
        }
        else
        {
            Debug.LogError("PlayButton: Componente Button não encontrado!");
        }

        if (levelManager == null)
        {
            levelManager = FindObjectOfType<LevelManager>();
            if (levelManager == null)
            {
                Debug.LogError("PlayButton: LevelManager não encontrado na cena!");
            }
        }
    }

    void OnPlayButtonClicked()
    {
        if (levelManager != null)
        {
            levelManager.PlaySelectedLevel();
        }
    }

    void OnDestroy()
    {
        if (button != null)
        {
            button.onClick.RemoveListener(OnPlayButtonClicked);
        }
    }
}
