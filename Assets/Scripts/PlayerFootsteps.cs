using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    public AudioSource footstepAudio;

    public AudioClip GrassWalkSound;
    public AudioClip GrassRunSound;
    public AudioClip ConcreteWalkSound;
    public AudioClip ConcreteRunSound;

    public float inputThreshold = 0.1f;
    public float raycastDistance = 2.0f;

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector2 input = new Vector2(moveX, moveZ);

        if (input.magnitude > inputThreshold)
        {
            bool isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
            UpdateFootstepClipBySurface(isRunning);

            if (!footstepAudio.isPlaying)
                footstepAudio.Play();
        }
        else
        {
            if (footstepAudio.isPlaying)
                footstepAudio.Stop();
        }
    }

    void UpdateFootstepClipBySurface(bool isRunning)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance))
        {
            string tag = hit.collider.tag;

            AudioClip selectedClip = null;

            switch (tag)
            {
                case "Grass":
                    selectedClip = isRunning ? GrassRunSound : GrassWalkSound;
                    break;
                case "Concrete":
                    selectedClip = isRunning ? ConcreteRunSound : ConcreteWalkSound;
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
