using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    [Header("UI Elements")]
    public Slider volumeSlider;
    public Toggle fullscreenToggle;
    public Toggle vibrationToggle;
    public Button backButton;

    private void Start()
    {
        // Carregar configurações salvas
        LoadSettings();

        // Adicionar listeners aos elementos da UI
        volumeSlider.onValueChanged.AddListener(SetVolume);
        fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
        vibrationToggle.onValueChanged.AddListener(SetVibration);
        backButton.onClick.AddListener(BackToMainMenu);
    }

    private void LoadSettings()
    {
        // Carregar volume salvo
        float savedVolume = PlayerPrefs.GetFloat("Volume", 1f);
        volumeSlider.value = savedVolume;
        AudioListener.volume = savedVolume;

        // Carregar estado de tela cheia salvo
        bool isFullscreen = PlayerPrefs.GetInt("Fullscreen", 1) == 1;
        fullscreenToggle.isOn = isFullscreen;
        Screen.fullScreen = isFullscreen;

        // Carregar estado de vibração salvo
        bool isVibrationEnabled = PlayerPrefs.GetInt("Vibration", 1) == 1;
        vibrationToggle.isOn = isVibrationEnabled;
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("Volume", volume);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
    }

    public void SetVibration(bool isVibrationEnabled)
    {
        PlayerPrefs.SetInt("Vibration", isVibrationEnabled ? 1 : 0);

        // Testar vibração ao ativar
        if (isVibrationEnabled)
        {
            Handheld.Vibrate();
        }
    }

    public void BackToMainMenu()
    {
        // Carregar a cena do menu principal
        SceneManager.LoadScene("StartMenu");
    }
}