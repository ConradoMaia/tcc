using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class MoodThermometer : MonoBehaviour
{
    // Refer�ncias aos bot�es
    public Button sadButton;
    public Button neutralButton;
    public Button happyButton;

    // Caminho para salvar os dados
    private string savePath;

    void Start()
    {
        // Inicializa o caminho para salvar os dados
        savePath = UnityEngine.Application.persistentDataPath + "/moodData.txt";

        // Adiciona os listeners aos bot�es
        sadButton.onClick.AddListener(() => SaveMoodAndProceed("Sad"));
        neutralButton.onClick.AddListener(() => SaveMoodAndProceed("Neutral"));
        happyButton.onClick.AddListener(() => SaveMoodAndProceed("Happy"));
    }

    // Salva o humor e vai para a pr�xima tela
    void SaveMoodAndProceed(string mood)
    {
        // Salva o humor no arquivo
        SaveMoodToFile(mood);

        // Carrega a pr�xima cena (tela de respira��o)
        SceneManager.LoadScene("BreathingScreen");
    }

    // Fun��o para salvar o humor em um arquivo
    void SaveMoodToFile(string mood)
    {
        string data = $"Mood: {mood}, Time: {System.DateTime.Now}\n";
        File.AppendAllText(savePath, data);
        UnityEngine.Debug.Log("Mood saved: " + data);
    }
}