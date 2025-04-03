using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PopupManager popupManager;
    private int totalItems = 5;
    private int correctItems = 0;

    public void ItemPlacedCorrectly()
    {
        correctItems++;
        UnityEngine.Debug.Log("Itens: " + correctItems);

        if (correctItems >= totalItems)
        {
            popupManager.ShowPopup();
        }
    }
}