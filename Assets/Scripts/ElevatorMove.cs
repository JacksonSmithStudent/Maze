using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ElevatorMove : MonoBehaviour
{
    public float speed = 2f;
    public float distance = 22.3f;
    public float disableDelay = 2f;
    public List<GameObject> blocksToDisable;

    private Vector3 startPos;
    private bool moving = true;

    void Start()
    {
        startPos = transform.position;
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
