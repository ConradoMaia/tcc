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
    }

    [Header("Dental Tools")]
    public List<DentalTool> dentalTools = new List<DentalTool>();

    [Header("UI References")]
    public GameObject toolInfoPopup;
    public TMP_Text toolNameText;
    public TMP_Text toolDescriptionText;
    public Image toolImage;
    public Button closePopupButton;

    void Awake()
    {
        if (toolInfoPopup != null)
        {
            toolInfoPopup.SetActive(false);
        }
    }

    void Start()
    {
        if (closePopupButton != null)
        {
            closePopupButton.onClick.RemoveAllListeners();
            closePopupButton.onClick.AddListener(OnCloseButtonClick);
        }
    }

    public void OnToolClicked(int toolIndex)
    {
        if (toolIndex >= 0 && toolIndex < dentalTools.Count)
        {
            DentalTool tool = dentalTools[toolIndex];
            toolNameText.text = tool.toolName;
            toolDescriptionText.text = tool.description;
            toolImage.sprite = tool.toolSprite;

            toolInfoPopup.SetActive(true);

            OnToolInteraction?.Invoke();
        }
    }

    public void OnCloseButtonClick()
    {
        toolInfoPopup.SetActive(false);
    }
}