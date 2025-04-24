using UnityEngine;

// Este script deve ser adicionado u00e0 cena inicial do jogo
public class SceneNavigatorSetup : MonoBehaviour
{
    [SerializeField] private GameObject sceneNavigatorPrefab;

    void Awake()
    {
        // Verifica se o SceneNavigator ju00e1 existe
        if (SceneNavigator.Instance == null && sceneNavigatorPrefab != null)
        {
            // Instancia o SceneNavigator
            Instantiate(sceneNavigatorPrefab);
        }
    }
}