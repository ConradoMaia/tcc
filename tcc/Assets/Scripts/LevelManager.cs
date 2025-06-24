using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    [Header("Character Settings")]
    public RectTransform characterTransform;
    public float characterYOffset = 50f;
    public float characterXOffset = 0f;
    public float moveSpeed = 300f;

    [Header("Level Settings")]
    public List<RectTransform> stairPositions = new List<RectTransform>();
    public string[] levelSceneNames;

    [Header("Optional Settings")]
    public bool smoothMovement = true;
    public float movementCurveStrength = 0.5f;

    private int currentLevel = 0;
    private int unlockedLevel = 0;
    private bool isMoving = false;
    private Canvas mainCanvas;
    private RectTransform canvasRectTransform;

    void Awake()
    {
        FindCanvas();
    }

    void Start()
    {
        unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 0);

        currentLevel = unlockedLevel;

        if (characterTransform == null)
        {
            UnityEngine.Debug.LogError("LevelManager: Character Transform não está configurado!");
            return;
        }

        SetupCharacterTransform();

        if (stairPositions.Count > 0)
        {
            PositionCharacterOnStair(currentLevel);
        }
        else
        {
            UnityEngine.Debug.LogError("LevelManager: Nenhuma posição de degrau configurada!");
        }
    }

    private void FindCanvas()
    {
        mainCanvas = GetComponentInParent<Canvas>();

        if (mainCanvas == null)
        {
            mainCanvas = FindObjectOfType<Canvas>();
        }

        if (mainCanvas != null)
        {
            canvasRectTransform = mainCanvas.GetComponent<RectTransform>();
            UnityEngine.Debug.Log("Canvas encontrado: " + mainCanvas.name);
        }
        else
        {
            UnityEngine.Debug.LogError("LevelManager: Nenhum Canvas encontrado na cena! Certifique-se de que existe um Canvas.");
        }
    }

    private void SetupCharacterTransform()
    {
        if (mainCanvas == null)
        {
            FindCanvas();
            if (mainCanvas == null) return;
        }

        if (characterTransform.parent != mainCanvas.transform)
        {
            characterTransform.SetParent(mainCanvas.transform, true);
        }

        characterTransform.anchorMin = new Vector2(1, 0);
        characterTransform.anchorMax = new Vector2(1, 0);
        characterTransform.pivot = new Vector2(0.5f, 0.5f);
    }

    private void PositionCharacterOnStair(int stairIndex)
    {
        if (stairIndex >= 0 && stairIndex < stairPositions.Count)
        {
            if (mainCanvas == null)
            {
                FindCanvas();
                if (mainCanvas == null) return;
            }

            RectTransform stairTransform = stairPositions[stairIndex];

            Vector2 stairPos = stairTransform.anchoredPosition;

            float rightEdgeOffset = canvasRectTransform.rect.width - stairPos.x;

            characterTransform.anchoredPosition = new Vector2(-rightEdgeOffset + characterXOffset, stairPos.y + characterYOffset);
        }
    }

    public void OnStairClicked(int stairIndex)
    {
        if (stairIndex <= unlockedLevel && !isMoving && stairIndex < stairPositions.Count)
        {
            StartCoroutine(MoveCharacterToStair(stairIndex));
        }
    }

    private IEnumerator MoveCharacterToStair(int stairIndex)
    {
        if (mainCanvas == null)
        {
            FindCanvas();
            if (mainCanvas == null) yield break;
        }

        isMoving = true;

        Vector2 startPos = characterTransform.anchoredPosition;
        RectTransform targetStair = stairPositions[stairIndex];

        float rightEdgeOffset = canvasRectTransform.rect.width - targetStair.anchoredPosition.x;
        Vector2 targetPos = new Vector2(-rightEdgeOffset + characterXOffset, targetStair.anchoredPosition.y + characterYOffset);

        float distance = Vector2.Distance(startPos, targetPos);
        float journeyTime = distance / moveSpeed;
        float elapsedTime = 0f;

        while (elapsedTime < journeyTime)
        {
            elapsedTime += Time.deltaTime;
            float percent = elapsedTime / journeyTime;

            if (smoothMovement)
            {
                percent = Mathf.SmoothStep(0, 1, percent);
            }

            characterTransform.anchoredPosition = Vector2.Lerp(startPos, targetPos, percent);
            yield return null;
        }

        characterTransform.anchoredPosition = targetPos;
        currentLevel = stairIndex;
        isMoving = false;
    }

    public void PlaySelectedLevel()
    {
        if (!isMoving && currentLevel < levelSceneNames.Length)
        {
            string sceneName = levelSceneNames[currentLevel];

            if (string.IsNullOrEmpty(sceneName))
            {
                UnityEngine.Debug.LogError($"LevelManager: Nome da cena não configurado para o nível {currentLevel}");
                return;
            }

            if (SceneNavigator.Instance != null)
            {
                SceneNavigator.Instance.NavigateToScene(sceneName);
            }
            else
            {
                SceneManager.LoadScene(sceneName);
            }
        }
    }

    public static void UnlockNextLevel()
    {
        int currentUnlocked = PlayerPrefs.GetInt("UnlockedLevel", 0);
        PlayerPrefs.SetInt("UnlockedLevel", currentUnlocked + 1);
        PlayerPrefs.Save();
    }

    public void ResetProgress()
    {
        PlayerPrefs.SetInt("UnlockedLevel", 0);
        PlayerPrefs.Save();
        unlockedLevel = 0;
        currentLevel = 0;
        PositionCharacterOnStair(currentLevel);
    }
}
