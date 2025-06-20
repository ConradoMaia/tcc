using UnityEngine;
using UnityEngine.UI;

public class ClosePopupButton : MonoBehaviour
{
    public GameObject popupToClose;
    
    void Start()
    {
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(ClosePopup);
        }
    }
    
    public void ClosePopup()
    {
        if (popupToClose != null)
        {
            popupToClose.SetActive(false);
        }
    }
}