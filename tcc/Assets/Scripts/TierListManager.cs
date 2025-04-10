using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TierListManager : MonoBehaviour
{
    public RectTransform[] fearObjects;

    public void SaveOrder()
    {
        if (fearObjects == null || fearObjects.Length == 0)
        {
            UnityEngine.Debug.LogError("Nenhum objeto foi atribuído para salvar a ordem!");
            return;
        }

        // Ordena os objetos com base na posição Y
        List<string> order = new List<string>();
        foreach (var obj in fearObjects)
        {
            order.Add(obj.name);
        }

        // Salva a ordem em um arquivo
        string filePath = Path.Combine(UnityEngine.Application.persistentDataPath, "tier_list_order.txt");
        File.WriteAllLines(filePath, order);

        UnityEngine.Debug.Log("Ordem salva em: " + filePath);
        SceneManager.LoadScene("LevelMenu");
    }
}