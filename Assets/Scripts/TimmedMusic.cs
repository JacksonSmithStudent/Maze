using UnityEngine;

public class TimedAudioPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    public float playDuration = 600f;
    public float pauseDuration = 900f;

    private bool isPlaying = false;
    private float timer = 0f;

    void Start()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.Stop();
        isPlaying = false;
        timer = 0f;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (isPlaying && timer >= playDuration)
        {
            audioSource.Pause();
            isPlaying = false;
            timer = 0f;
        }
        else if (!isPlaying && timer >= pauseDuration)
        {
            audioSource.Play();
            isPlaying = true;
            timer = 0f;
        }
    }
}
