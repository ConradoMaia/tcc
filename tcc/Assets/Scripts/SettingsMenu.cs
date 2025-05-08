using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    [Header("Painel")]
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button openSettingsButton;

    [Header("Configurações de Áudio")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;

    [Header("Configurações de Qualidade")]
    [SerializeField] private TMP_Dropdown qualityDropdown;

    [Header("Configurações de Tela")]
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private TMP_Dropdown resolutionDropdown;

    private Resolution[] resolutions;

    private void Start()
    {
        // Configurar botões
        if (closeButton != null)
            closeButton.onClick.AddListener(CloseSettingsPanel);
        
        if (openSettingsButton != null)
            openSettingsButton.onClick.AddListener(OpenSettingsPanel);

        // Inicialmente, o painel está fechado
        if (settingsPanel != null)
            settingsPanel.SetActive(false);

        // Configurar sliders de volume
        SetupVolumeSliders();

        // Configurar dropdown de qualidade
        SetupQualityDropdown();

        // Configurar toggle de tela cheia
        SetupFullscreenToggle();

        // Configurar dropdown de resolução
        SetupResolutionDropdown();

        // Carregar configurações salvas
        LoadSettings();
    }

    private void SetupVolumeSliders()
    {
        if (masterVolumeSlider != null)
        {
            masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
            float masterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
            masterVolumeSlider.value = masterVolume;
            SetMasterVolume(masterVolume);
        }

        if (musicVolumeSlider != null)
        {
            musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
            float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
            musicVolumeSlider.value = musicVolume;
            SetMusicVolume(musicVolume);
        }

        if (sfxVolumeSlider != null)
        {
            sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);
            float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);
            sfxVolumeSlider.value = sfxVolume;
            SetSFXVolume(sfxVolume);
        }
    }

    private void SetupQualityDropdown()
    {
        if (qualityDropdown != null)
        {
            qualityDropdown.ClearOptions();
            qualityDropdown.AddOptions(new System.Collections.Generic.List<string>(QualitySettings.names));
            qualityDropdown.value = QualitySettings.GetQualityLevel();
            qualityDropdown.onValueChanged.AddListener(SetQuality);
        }
    }

    private void SetupFullscreenToggle()
    {
        if (fullscreenToggle != null)
        {
            fullscreenToggle.isOn = Screen.fullScreen;
            fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
        }
    }

    private void SetupResolutionDropdown()
    {
        if (resolutionDropdown != null)
        {
            resolutions = Screen.resolutions;
            resolutionDropdown.ClearOptions();

            System.Collections.Generic.List<string> options = new System.Collections.Generic.List<string>();
            int currentResolutionIndex = 0;

            for (int i = 0; i < resolutions.Length; i++)
            {
                string option = $"{resolutions[i].width}x{resolutions[i].height} @{resolutions[i].refreshRate}Hz";
                options.Add(option);

                if (resolutions[i].width == Screen.currentResolution.width &&
                    resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                }
            }

            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = currentResolutionIndex;
            resolutionDropdown.RefreshShownValue();
            resolutionDropdown.onValueChanged.AddListener(SetResolution);
        }
    }

    public void OpenSettingsPanel()
    {
        settingsPanel.SetActive(true);
        settingsPanel.transform.localScale = Vector3.zero;
        LeanTween.scale(settingsPanel, Vector3.one, 0.3f).setEaseOutBack();
    }

    public void CloseSettingsPanel()
    {
        LeanTween.scale(settingsPanel, Vector3.zero, 0.3f).setEaseInBack().setOnComplete(() => {
            settingsPanel.SetActive(false);
            SaveSettings();
        });
    }

    public void SetMasterVolume(float volume)
    {
        if (audioMixer != null)
        {
            audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
        }
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        if (audioMixer != null)
        {
            audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        }
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        if (audioMixer != null)
        {
            audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
        }
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("QualityLevel", qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("ResolutionIndex", resolutionIndex);
    }

    private void SaveSettings()
    {
        PlayerPrefs.Save();
    }

    private void LoadSettings()
    {
        // Carregar volumes
        if (masterVolumeSlider != null)
            masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1f);
        
        if (musicVolumeSlider != null)
            musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        
        if (sfxVolumeSlider != null)
            sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);

        // Carregar qualidade
        if (qualityDropdown != null)
        {
            int qualityLevel = PlayerPrefs.GetInt("QualityLevel", QualitySettings.GetQualityLevel());
            qualityDropdown.value = qualityLevel;
            QualitySettings.SetQualityLevel(qualityLevel);
        }

        // Carregar tela cheia
        if (fullscreenToggle != null)
        {
            bool isFullscreen = PlayerPrefs.GetInt("Fullscreen", Screen.fullScreen ? 1 : 0) == 1;
            fullscreenToggle.isOn = isFullscreen;
            Screen.fullScreen = isFullscreen;
        }

        // Carregar resolução
        if (resolutionDropdown != null)
        {
            int resolutionIndex = PlayerPrefs.GetInt("ResolutionIndex", resolutionDropdown.value);
            if (resolutionIndex < resolutions.Length)
            {
                resolutionDropdown.value = resolutionIndex;
                Resolution resolution = resolutions[resolutionIndex];
                Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
            }
        }
    }
}