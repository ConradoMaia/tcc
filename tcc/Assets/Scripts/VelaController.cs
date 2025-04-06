using UnityEngine;
using static System.Net.Mime.MediaTypeNames;

public class VelaController : MonoBehaviour
{
    public UnityEngine.UI.Image velaImage;
    public UnityEngine.UI.Image chamaImage;
    public Sprite[] chamaSprites;
    private int currentChamaIndex = 0;

    public float tempoEntreFrames = 0.1f;
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= tempoEntreFrames)
        {
            NextChamaSprite();
            timer = 0f;
        }
    }

    void NextChamaSprite()
    {
        if (chamaSprites.Length == 0) return;

        currentChamaIndex = (currentChamaIndex + 1) % chamaSprites.Length;
        chamaImage.sprite = chamaSprites[currentChamaIndex];

        if (currentChamaIndex == chamaSprites.Length - 1)
        {
            chamaImage.enabled = false;
        }
    }
}