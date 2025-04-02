using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenuManager : MonoBehaviour
{
    public Transform[] steps; // Refer�ncia aos degraus da escada
    public RectTransform character; // Refer�ncia ao boneco (pessoinha)
    public Button playButton; // Bot�o de jogar
    private int currentLevel = 0; // N�vel atual da crian�a

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
        // Aqui voc� pode carregar a cena correspondente ao n�vel atual
        UnityEngine.Debug.Log("Jogando o n�vel: " + (currentLevel + 1));
        SceneManager.LoadScene("Level" + (currentLevel + 1));
    }
}