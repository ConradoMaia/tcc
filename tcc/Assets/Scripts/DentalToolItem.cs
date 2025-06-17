using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DentalToolItem : MonoBehaviour, IPointerClickHandler
{
    [Tooltip("Index of this tool in the DentalToolManager's list")]
    public int toolIndex;
    
    private DentalToolManager toolManager;
    private Button button;
    private Image image;
    
    private Vector3 originalScale;
    private float hoverScaleFactor = 1.1f;
    private float clickScaleFactor = 0.9f;
    
    private void Start()
    {
        toolManager = FindObjectOfType<DentalToolManager>();
        if (toolManager == null)
        {
            Debug.LogError("DentalToolManager not found in the scene!");
        }
        
        button = GetComponent<Button>();
        image = GetComponent<Image>();
        
        originalScale = transform.localScale;
        
        if (button != null)
        {
            EventTrigger trigger = GetComponent<EventTrigger>();
            if (trigger == null)
            {
                trigger = gameObject.AddComponent<EventTrigger>();
            }
            
            EventTrigger.Entry enterEntry = new EventTrigger.Entry();
            enterEntry.eventID = EventTriggerType.PointerEnter;
            enterEntry.callback.AddListener((data) => { OnPointerEnter(); });
            trigger.triggers.Add(enterEntry);
            
            EventTrigger.Entry exitEntry = new EventTrigger.Entry();
            exitEntry.eventID = EventTriggerType.PointerExit;
            exitEntry.callback.AddListener((data) => { OnPointerExit(); });
            trigger.triggers.Add(exitEntry);
        }
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (toolManager != null)
        {
            AnimateClick();
            
            toolManager.OnToolClicked(toolIndex);
        }
    }
    
    private void OnPointerEnter()
    {
        LeanTween.scale(gameObject, originalScale * hoverScaleFactor, 0.2f).setEaseOutQuad();
    }
    
    private void OnPointerExit()
    {
        LeanTween.scale(gameObject, originalScale, 0.2f).setEaseOutQuad();
    }
    
    private void AnimateClick()
    {
        LeanTween.cancel(gameObject);
        LeanTween.scale(gameObject, originalScale * clickScaleFactor, 0.1f).setEaseInQuad().setOnComplete(() => {
            LeanTween.scale(gameObject, originalScale * hoverScaleFactor, 0.2f).setEaseOutElastic();
        });
    }
}