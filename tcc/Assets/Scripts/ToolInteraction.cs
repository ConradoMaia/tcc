using UnityEngine;

public class ToolInteraction : MonoBehaviour
{
    [Header("Configurações de Áudio")]
    public AudioClip toolSound;
    public float volume = 1.0f;
    
    [Header("Configurações de Vibração")]
    public long vibrationDuration = 100;
    public int vibrationAmplitude = 100;
    
    private AudioSource audioSource;
    
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    
    private void OnMouseDown()
    {
        PlayToolFeedback();
    }
    
    public void PlayToolFeedback()
    {
        if (toolSound != null)
        {
            audioSource.PlayOneShot(toolSound, volume);
        }
        
        Vibrate();
        
        Level3Manager level3Manager = FindObjectOfType<Level3Manager>();
        if (level3Manager != null)
        {
            level3Manager.RegisterToolInteraction(gameObject.name);
        }
    }
    
    private void Vibrate()
    {
        #if UNITY_ANDROID && !UNITY_EDITOR
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
            
            if (vibrator.Call<bool>("hasVibrator"))
            {
                vibrator.Call("vibrate", vibrationDuration);
            }
        #elif UNITY_IOS && !UNITY_EDITOR
            Handheld.Vibrate();
        #endif
    }
}