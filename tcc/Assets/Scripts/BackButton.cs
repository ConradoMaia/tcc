using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class BackButton : MonoBehaviour
{
    // Chave para armazenar o histórico de navegação
    private const string NAVIGATION_HISTORY_KEY = "NavigationHistory";
    // Separador para a string do histórico
    private const string SEPARATOR = "|";
    // Nome da cena do menu inicial
    private const string MENU_SCENE = "StartMenu"; // Ajuste para o nome correto da sua cena inicial
    
    // Lista para armazenar o histórico de navegação
    private static List<string> navigationHistory = new List<string>();
    
    void Awake()
    {
        // Carrega o histórico de navegação se ainda não foi carregado
        if (navigationHistory.Count == 0 && PlayerPrefs.HasKey(NAVIGATION_HISTORY_KEY))
        {
            string historyString = PlayerPrefs.GetString(NAVIGATION_HISTORY_KEY);
            string[] scenes = historyString.Split(new[] { SEPARATOR }, System.StringSplitOptions.RemoveEmptyEntries);
            navigationHistory = new List<string>(scenes);
        }
        
        // Adiciona a cena atual ao histórico se for diferente da última
        string currentScene = SceneManager.GetActiveScene().name;
        if (navigationHistory.Count == 0 || navigationHistory[navigationHistory.Count - 1] != currentScene)
        {
            navigationHistory.Add(currentScene);
            SaveNavigationHistory();
        }
        
        // Limita o histórico a 10 cenas para evitar crescimento excessivo
        if (navigationHistory.Count > 10)
        {
            navigationHistory.RemoveAt(0);
            SaveNavigationHistory();
        }
    }
    
    public void GoBack()
    {
        // Remove a cena atual do histórico
        if (navigationHistory.Count > 0)
        {
            navigationHistory.RemoveAt(navigationHistory.Count - 1);
        }
        
        // Verifica se há uma cena anterior para voltar
        if (navigationHistory.Count > 0)
        {
            // Obtém a última cena do histórico
            string previousScene = navigationHistory[navigationHistory.Count - 1];
            // Remove a cena anterior do histórico (será adicionada novamente no Awake)
            navigationHistory.RemoveAt(navigationHistory.Count - 1);
            SaveNavigationHistory();
            // Carrega a cena anterior
            SceneManager.LoadScene(previousScene);
        }
        else
        {
            // Se não houver histórico, volta para o menu principal
            navigationHistory.Clear();
            SaveNavigationHistory();
            SceneManager.LoadScene(MENU_SCENE);
        }
    }
    
    // Salva o histórico de navegação nos PlayerPrefs
    private void SaveNavigationHistory()
    {
        string historyString = string.Join(SEPARATOR, navigationHistory.ToArray());
        PlayerPrefs.SetString(NAVIGATION_HISTORY_KEY, historyString);
        PlayerPrefs.Save();
    }
    
    // Método estático para limpar o histórico de navegação (útil ao iniciar um novo jogo)
    public static void ClearNavigationHistory()
    {
        navigationHistory.Clear();
        PlayerPrefs.DeleteKey(NAVIGATION_HISTORY_KEY);
        PlayerPrefs.Save();
    }
}