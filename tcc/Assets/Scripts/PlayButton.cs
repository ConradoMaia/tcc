using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public FearTierList fearTierList;
    public string nextSceneName = "GameScene"; // Nome da cena do jogo

    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnPlayButtonClicked);
    }

    void OnPlayButtonClicked()
    {
        // Salva a ordem dos medos antes de carregar a próxima cena
        SaveFearOrder();

        // Carrega a cena do jogo
        SceneManager.LoadScene(nextSceneName);
    }

    void SaveFearOrder()
    {
        // Salva a ordem dos medos usando PlayerPrefs
        for (int i = 0; i < fearTierList.fears.Count; i++)
        {
            PlayerPrefs.SetString("Fear_" + i, fearTierList.fears[i].fearName);
        }

        PlayerPrefs.SetInt("FearCount", fearTierList.fears.Count);
        PlayerPrefs.Save();
    }
}