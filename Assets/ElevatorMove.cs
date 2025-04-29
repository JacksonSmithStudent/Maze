using UnityEngine;

public class ElevatorMove : MonoBehaviour
{
    public float speed = 2f;
    public float distance = 10f;

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
            }
        }
    }
}
