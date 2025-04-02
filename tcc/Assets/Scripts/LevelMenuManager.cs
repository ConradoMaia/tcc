using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenuManager : MonoBehaviour
{
    public Transform[] steps; // Referência aos degraus da escada
    public RectTransform character; // Referência ao boneco (pessoinha)
    public Button playButton; // Botão de jogar
    private int currentLevel = 0; // Nível atual da criança

    private void Start()
    {
        UpdateCharacterPosition();
        playButton.onClick.AddListener(PlayLevel);
    }

    public void CompleteLevel()
    {
        if (currentLevel < steps.Length - 1)
        {
            currentLevel++;
            UpdateCharacterPosition();
        }
    }

    private void UpdateCharacterPosition()
    {
        // Move o boneco para o degrau correspondente
        character.position = steps[currentLevel].position;
    }

    private void PlayLevel()
    {
        // Aqui você pode carregar a cena correspondente ao nível atual
        UnityEngine.Debug.Log("Jogando o nível: " + (currentLevel + 1));
        SceneManager.LoadScene("Level" + (currentLevel + 1));
    }
}