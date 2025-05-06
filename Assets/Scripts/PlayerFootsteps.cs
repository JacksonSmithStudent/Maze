using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    public AudioSource footstepAudio;
    public AudioClip GrassSound;
    public AudioClip ConcreteSound;
    public float inputThreshold = 0.1f;
    public float raycastDistance = 2.0f;

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector2 input = new Vector2(moveX, moveZ);

        if (input.magnitude > inputThreshold)
        {
            UpdateFootstepClipBySurface();

            if (!footstepAudio.isPlaying)
                footstepAudio.Play();
        }
        else
        {
            if (footstepAudio.isPlaying)
                footstepAudio.Stop();
        }
    }

    void UpdateFootstepClipBySurface()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance))
        {
            string tag = hit.collider.tag;

            AudioClip selectedClip = null;

            switch (tag)
            {
                case "Grass":
                    selectedClip = GrassSound;
                    break;
                case "Concrete":
                    selectedClip = ConcreteSound;
                    break;
                default:
                    break;
            }

            if (selectedClip != null && footstepAudio.clip != selectedClip)
            {
                footstepAudio.Stop();
                footstepAudio.clip = selectedClip;
                footstepAudio.Play();
            }
        }
    }
}
