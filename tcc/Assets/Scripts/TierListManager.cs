using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TierListManager : MonoBehaviour
{
    public Transform tierListContainer;

    public void SaveOrder()
    {
        if (tierListContainer == null)
        {
            UnityEngine.Debug.LogError("Tier List Container não foi atribuído no Inspector!");
            return;
        }

        List<string> order = new List<string>();
        foreach (Transform child in tierListContainer)
        {
            order.Add(child.name); // Salva o nome de cada item na ordem atual
        }

        // Salva a ordem em um arquivo
        string filePath = Path.Combine(UnityEngine.Application.persistentDataPath, "tier_list_order.txt");
        File.WriteAllLines(filePath, order);

        UnityEngine.Debug.Log("Ordem salva em: " + filePath);
        SceneManager.LoadScene("LevelMenu");
    }
}