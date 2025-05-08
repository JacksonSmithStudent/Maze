using UnityEngine;
using UnityEngine.UI;

public class MicLoudnessUI : MonoBehaviour
{
    public Slider volumeSlider;          // Assign in Inspector
    private string micDevice;
    private AudioClip micClip;
    private const int sampleWindow = 128;

    void Start()
    {
        // Check if mic is available
        if (Microphone.devices.Length > 0)
        {
            micDevice = Microphone.devices[0];
            micClip = Microphone.Start(micDevice, true, 10, 44100); // 10 sec looping buffer
        }
        else
        {
            Debug.LogWarning("No microphone detected.");
        }

        // Optional: set slider range
        if (volumeSlider != null)
        {
            volumeSlider.minValue = 0f;
            volumeSlider.maxValue = 1f;
        }
    }

    void Update()
    {
        if (micClip == null || volumeSlider == null) return;

        float volume = GetMicVolume();
        volumeSlider.value = Mathf.Clamp01(volume * 10f); // Scale for visibility

        // Optional Debug
        Debug.Log("Mic Volume: " + volume);
    }

    float GetMicVolume()
    {
        int micPosition = Microphone.GetPosition(micDevice);
        if (micPosition < sampleWindow) return 0f;

        float[] samples = new float[sampleWindow];
        micClip.GetData(samples, micPosition - sampleWindow);

        float sum = 0;
        foreach (var sample in samples)
        {
            sum += sample * sample;
        }

        return Mathf.Sqrt(sum / sampleWindow); // RMS (Root Mean Square) volume
    }
}
