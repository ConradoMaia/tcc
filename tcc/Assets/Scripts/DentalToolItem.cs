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
    
    void Start()
    {
        toolManager = FindObjectOfType<DentalToolManager>();
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (toolManager != null)
        {
            toolManager.OnToolClicked(toolIndex);
        }
    }
}