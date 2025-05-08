using UnityEngine;

public class MicrophoneZone : MonoBehaviour
{
    public GameObject uiContainer;

    private AudioSource audioSource;
    private bool isInsideZone = false;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.mute = true;

        if (uiContainer != null)
            uiContainer.SetActive(false); 
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isInsideZone)
        {
            StartMicrophone();

            if (uiContainer != null)
                uiContainer.SetActive(true); 

            isInsideZone = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && isInsideZone)
        {
            StopMicrophone();

            if (uiContainer != null)
                uiContainer.SetActive(false); 

            isInsideZone = false;
        }
    }

    void StartMicrophone()
    {
        if (Microphone.devices.Length > 0)
        {
            audioSource.clip = Microphone.Start(null, true, 10, 44100);
            while (!(Microphone.GetPosition(null) > 0)) { } 
            audioSource.Play();
            Debug.Log("Microphone started");
        }
        else
        {
            Debug.LogWarning("No microphone detected.");
        }
    }

    void StopMicrophone()
    {
        if (Microphone.IsRecording(null))
        {
            Microphone.End(null);
            audioSource.Stop();
            Debug.Log("Microphone stopped");
        }
    }
}
