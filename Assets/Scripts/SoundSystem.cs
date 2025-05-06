using UnityEngine;

public class TriggerSound : MonoBehaviour
{
    public AudioSource soundToPlay;
    public GameObject playerObject;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == playerObject)
        {
            Debug.Log("Player entered trigger area");
            if (!soundToPlay.isPlaying)
            {
                soundToPlay.Play();
                Debug.Log("Sound started");
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == playerObject)
        {
            Debug.Log("Player exited trigger area");
            if (soundToPlay.isPlaying)
            {
                soundToPlay.Stop();
                Debug.Log("Sound stopped");
            }
        }
    }
}
