using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StairButton : MonoBehaviour, IPointerClickHandler
{
    public int stairIndex;
    public LevelManager levelManager;
    public UnityEngine.UI.Image stairImage; // Especifica o namespace correto

    public Color unlockedColor = Color.white;
    public Color lockedColor = Color.gray;

    void Start()
    {
        // Verifica se o nível está desbloqueado
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 0);

        // Atualiza a cor do degrau
        if (stairImage != null)
        {
            stairImage.color = (stairIndex <= unlockedLevel) ? unlockedColor : lockedColor;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        levelManager.OnStairClicked(stairIndex);
    }
}