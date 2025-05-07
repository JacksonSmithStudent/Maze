using UnityEngine;

public class ProximityAudio : MonoBehaviour
{
    public Transform player;
    public AudioSource audioSource;
    public float detectionRadius = 50f;

    void Start()
    {
        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
        }

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        if (audioSource == null || audioSource.clip == null)
        {
            return;
        }

        audioSource.volume = 0f;
        audioSource.loop = true;
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f;
    }

    void Update()
    {
        if (player == null || audioSource == null || audioSource.clip == null)
            return;

        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= detectionRadius)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }

            float volume = 1f - (distance / detectionRadius);
            audioSource.volume = Mathf.Clamp01(volume);
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}
