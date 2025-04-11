using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    public RectTransform characterTransform;
    public List<RectTransform> stairPositions = new List<RectTransform>();
    public float moveSpeed = 2.0f;
    public string[] levelSceneNames;

    private int currentLevel = 0;
    private int unlockedLevel = 0;
    private bool isMoving = false;

    void Start()
    {
        // Carrega o nível desbloqueado
        unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 0);

        // Posiciona o personagem no degrau atual
        if (stairPositions.Count > 0 && characterTransform != null)
        {
            // Define a posição exata do primeiro degrau
            characterTransform.position = stairPositions[currentLevel].position;

            // Opcionalmente, ajuste a posição Y para garantir que fique em cima do degrau
            Vector3 pos = characterTransform.position;
            // Ajuste este valor conforme necessário para alinhar com o degrau
            float yOffset = 20f;
            characterTransform.position = new Vector3(pos.x, pos.y + yOffset, pos.z);
        }
    }

    // Chamado quando o usuário clica em um degrau
    public void OnStairClicked(int stairIndex)
    {
        // Verifica se o nível está desbloqueado
        if (stairIndex <= unlockedLevel && !isMoving)
        {
            StartCoroutine(MoveCharacterToStair(stairIndex));
        }
    }

    // Move o personagem para o degrau selecionado
    private IEnumerator MoveCharacterToStair(int stairIndex)
    {
        isMoving = true;

        // Determina a direção do movimento
        int direction = (stairIndex > currentLevel) ? 1 : -1;

        // Move o personagem degrau por degrau
        for (int i = currentLevel; i != stairIndex; i += direction)
        {
            Vector3 targetPosition = stairPositions[i + direction].position;

            while (Vector3.Distance(characterTransform.position, targetPosition) > 0.1f)
            {
                characterTransform.position = Vector3.MoveTowards(
                    characterTransform.position,
                    targetPosition,
                    moveSpeed * Time.deltaTime);

                yield return null;
            }
        }

        currentLevel = stairIndex;
        isMoving = false;
    }

    // Inicia o nível selecionado
    public void PlaySelectedLevel()
    {
        if (currentLevel < levelSceneNames.Length)
        {
            SceneManager.LoadScene(levelSceneNames[currentLevel]);
        }
    }

    // Desbloqueia o próximo nível (chamado quando um nível é concluído)
    public static void UnlockNextLevel()
    {
        int currentUnlocked = PlayerPrefs.GetInt("UnlockedLevel", 0);
        PlayerPrefs.SetInt("UnlockedLevel", currentUnlocked + 1);
        PlayerPrefs.Save();
    }
}