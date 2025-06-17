using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationManager : MonoBehaviour
{
    private static VibrationManager _instance;
    
    public static VibrationManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("VibrationManager");
                _instance = go.AddComponent<VibrationManager>();
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
    }
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    // Vibração simples usando a API nativa
    public void Vibrate()
    {
        #if UNITY_ANDROID && !UNITY_EDITOR
            Handheld.Vibrate();
        #elif UNITY_IOS && !UNITY_EDITOR
            Handheld.Vibrate();
        #else
            Debug.Log("Vibração simulada (não disponível no editor)");
        #endif
    }
    
    // Vibração com duração personalizada (Android)
    // Nota: No iOS, a duração é ignorada, pois a API nativa não suporta duração personalizada
    public void Vibrate(long milliseconds)
    {
        #if UNITY_ANDROID && !UNITY_EDITOR
            AndroidVibrate(milliseconds);
        #elif UNITY_IOS && !UNITY_EDITOR
            Handheld.Vibrate();
        #else
            Debug.Log($"Vibração simulada por {milliseconds}ms (não disponível no editor)");
        #endif
    }
    
    // Vibração com padrão e amplitudes (Android)
    public void VibratePattern(long[] pattern, int[] amplitudes, int repeat)
    {
        #if UNITY_ANDROID && !UNITY_EDITOR
            AndroidVibratePattern(pattern, amplitudes, repeat);
        #else
            Debug.Log("Vibração com padrão simulada (não disponível no editor ou iOS)");
        #endif
    }
    
    // Vibração com intensidade personalizada
    // Converte a intensidade (0-1) em um padrão de vibração apropriado
    public void VibrateWithIntensity(float intensity, float duration)
    {
        // Limita a intensidade entre 0 e 1
        intensity = Mathf.Clamp01(intensity);
        
        #if UNITY_ANDROID && !UNITY_EDITOR
            // Para Android, podemos usar padrões de vibração com amplitudes
            if (intensity <= 0.01f) return; // Muito baixo para vibrar
            
            // Converte duração para milissegundos
            long durationMs = (long)(duration * 1000);
            
            if (intensity < 0.3f)
            {
                // Vibração suave
                AndroidVibrate(durationMs);
            }
            else if (intensity < 0.7f)
            {
                // Vibração média - padrão pulsante
                long[] pattern = new long[] { 0, durationMs/3, durationMs/6, durationMs/3, durationMs/6, durationMs/3 };
                int[] amplitudes = new int[] { 0, (int)(255 * intensity), 0, (int)(255 * intensity), 0, (int)(255 * intensity) };
                AndroidVibratePattern(pattern, amplitudes, -1);
            }
            else
            {
                // Vibração forte - padrão contínuo com alta amplitude
                AndroidVibrate(durationMs, (int)(255 * intensity));
            }
        #elif UNITY_IOS && !UNITY_EDITOR
            // Para iOS, só podemos usar a vibração padrão
            if (intensity > 0.3f) // Só vibra se a intensidade for significativa
            {
                Handheld.Vibrate();
            }
        #else
            Debug.Log($"Vibração simulada com intensidade {intensity} por {duration}s (não disponível no editor)");
        #endif
    }
    
    #if UNITY_ANDROID && !UNITY_EDITOR
    private AndroidJavaClass unityPlayer;
    private AndroidJavaObject currentActivity;
    private AndroidJavaObject vibrator;
    private AndroidJavaObject context;
    
    private void AndroidVibrate(long milliseconds)
    {
        if (vibrator == null)
        {
            SetupVibrator();
        }
        
        vibrator.Call("vibrate", milliseconds);
    }
    
    private void AndroidVibrate(long milliseconds, int amplitude)
    {
        if (vibrator == null)
        {
            SetupVibrator();
        }
        
        // Verifica se o dispositivo suporta amplitude de vibração
        if (vibrator.Call<bool>("hasVibrator") && IsAndroidVersionAtLeast(26))
        {
            vibrator.Call("vibrate", VibrationEffect(milliseconds, amplitude));
        }
        else
        {
            vibrator.Call("vibrate", milliseconds);
        }
    }
    
    private void AndroidVibratePattern(long[] pattern, int[] amplitudes, int repeat)
    {
        if (vibrator == null)
        {
            SetupVibrator();
        }
        
        // Verifica se o dispositivo suporta amplitude de vibração
        if (vibrator.Call<bool>("hasVibrator") && IsAndroidVersionAtLeast(26) && amplitudes != null && pattern.Length == amplitudes.Length)
        {
            vibrator.Call("vibrate", VibrationEffectPattern(pattern, amplitudes, repeat));
        }
        else
        {
            vibrator.Call("vibrate", pattern, repeat);
        }
    }
    
    private void SetupVibrator()
    {
        unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        context = currentActivity.Call<AndroidJavaObject>("getApplicationContext");
        
        if (IsAndroidVersionAtLeast(26))
        {
            vibrator = context.Call<AndroidJavaObject>("getSystemService", "vibrator");
        }
        else
        {
            vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
        }
    }
    
    private AndroidJavaObject VibrationEffect(long milliseconds, int amplitude)
    {
        AndroidJavaClass vibrationEffectClass = new AndroidJavaClass("android.os.VibrationEffect");
        if (amplitude > 0)
        {
            return vibrationEffectClass.CallStatic<AndroidJavaObject>("createOneShot", milliseconds, amplitude);
        }
        else
        {
            return vibrationEffectClass.CallStatic<AndroidJavaObject>("createOneShot", milliseconds, 255); // Amplitude máxima
        }
    }
    
    private AndroidJavaObject VibrationEffectPattern(long[] pattern, int[] amplitudes, int repeat)
    {
        AndroidJavaClass vibrationEffectClass = new AndroidJavaClass("android.os.VibrationEffect");
        int[] amplitudesArray = amplitudes;
        
        if (amplitudes != null && pattern.Length == amplitudes.Length)
        {
            return vibrationEffectClass.CallStatic<AndroidJavaObject>("createWaveform", pattern, amplitudesArray, repeat);
        }
        else
        {
            return vibrationEffectClass.CallStatic<AndroidJavaObject>("createWaveform", pattern, repeat);
        }
    }
    
    private bool IsAndroidVersionAtLeast(int apiLevel)
    {
        AndroidJavaClass buildVersionClass = new AndroidJavaClass("android.os.Build$VERSION");
        int sdkInt = buildVersionClass.GetStatic<int>("SDK_INT");
        return sdkInt >= apiLevel;
    }
    #endif
}