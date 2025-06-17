using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DentalToolManager : MonoBehaviour
{
    public event Action OnToolInteraction;
    [System.Serializable]
    public class DentalTool
    {
        public string toolName;
        public string description;
        public Sprite toolSprite;
        public AudioClip toolSound;
        [Range(0f, 1f)]
        public float vibrationIntensity = 0.5f;
        public float vibrationDuration = 0.5f;
    }

    [Header("Dental Tools")]
    public List<DentalTool> dentalTools = new List<DentalTool>();

    [Header("UI References")]
    public GameObject toolInfoPopup;
    public TextMeshProUGUI toolNameText;
    public TextMeshProUGUI toolDescriptionText;
    public Image toolImage;
    public Button closePopupButton;

    [Header("Audio")]
    public AudioSource audioSource;

    private void Start()
    {
        if (toolInfoPopup != null)
        {
            toolInfoPopup.SetActive(false);
        }

        if (closePopupButton != null)
        {
            closePopupButton.onClick.AddListener(ClosePopup);
        }

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void OnToolClicked(int toolIndex)
    {
        if (toolIndex >= 0 && toolIndex < dentalTools.Count)
        {
            ShowToolInfo(dentalTools[toolIndex]);
            PlayToolSound(dentalTools[toolIndex]);
            VibrateDevice(dentalTools[toolIndex]);
            
            OnToolInteraction?.Invoke();
        }
        else
        {
            Debug.LogError("Tool index out of range: " + toolIndex);
        }
    }

    private void ShowToolInfo(DentalTool tool)
    {
        if (toolInfoPopup != null)
        {
            toolNameText.text = tool.toolName;
            toolDescriptionText.text = tool.description;
            toolImage.sprite = tool.toolSprite;

            toolInfoPopup.SetActive(true);
            
            if (toolInfoPopup.transform.localScale == Vector3.one)
            {
                toolInfoPopup.transform.localScale = Vector3.zero;
                LeanTween.scale(toolInfoPopup, Vector3.one, 0.3f).setEaseOutBack();
            }
        }
    }

    private void PlayToolSound(DentalTool tool)
    {
        if (audioSource != null && tool.toolSound != null)
        {
            audioSource.clip = tool.toolSound;
            audioSource.Play();
        }
    }

    private void VibrateDevice(DentalTool tool)
    {
        VibrationManager.Instance.VibrateWithIntensity(tool.vibrationIntensity, tool.vibrationDuration);
    }

    public void ClosePopup()
    {
        if (toolInfoPopup != null)
        {
            LeanTween.scale(toolInfoPopup, Vector3.zero, 0.2f).setEaseInBack().setOnComplete(() => {
                toolInfoPopup.SetActive(false);
                toolInfoPopup.transform.localScale = Vector3.one;
            });
        }
    }
}