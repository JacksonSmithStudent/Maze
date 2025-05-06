using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ElevatorMove : MonoBehaviour
{
    public float speed = 2f;
    public float distance = 64.5f;
    public float disableDelay = 2f;
    public List<GameObject> blocksToDisable;
    public AudioSource elevatorAudio;
    public AudioClip elevatorSound;
    public AudioClip topReachedSound;

    private Vector3 startPos;
    private bool moving = true;
    private bool topSoundPlayed = false;

    void Start()
    {
        startPos = transform.position;

        if (elevatorAudio != null && elevatorSound != null)
        {
            elevatorAudio.clip = elevatorSound;
            elevatorAudio.loop = true;
            elevatorAudio.Play();
        }
    }

    void Update()
    {
        if (moving)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);

            if (Vector3.Distance(startPos, transform.position) >= distance)
            {
                moving = false;
                StartCoroutine(DisableBlocksAfterDelay());

                if (elevatorAudio != null)
                {
                    elevatorAudio.Stop();
                }

                if (!topSoundPlayed && topReachedSound != null)
                {
                    elevatorAudio.clip = topReachedSound;
                    elevatorAudio.loop = false;
                    elevatorAudio.Play();
                    topSoundPlayed = true;
                }
                else
                {
                    elevatorAudio.Stop();
                }
            }
        }
    }

    IEnumerator DisableBlocksAfterDelay()
    {
        yield return new WaitForSeconds(disableDelay);

        foreach (GameObject obj in blocksToDisable)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }
    }
}
