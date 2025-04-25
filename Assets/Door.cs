using UnityEngine;

public class DoorController : MonoBehaviour
{
    public bool useAnimation = true;
    public Animator doorAnimator;
    public Transform door;
    public Vector3 closedPosition;
    public Vector3 openPosition;
    public float moveSpeed = 2f;

    private bool isOpen = false;
    private float timer = 0f;
    private float cycleTime = 25f * 60f;
    private float closeTime = 10f * 60f;
    private float openTime = 15f * 60f;

    void Start()
    {
        timer = 0f;
        if (!useAnimation && door != null)
            door.position = openPosition;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (!isOpen && timer >= 0f && timer < openTime)
        {
            OpenDoor();
        }
        else if (isOpen && timer >= openTime && timer < cycleTime)
        {
            CloseDoor();
        }

        if (timer >= cycleTime)
        {
            timer = 0f;
        }

        if (!useAnimation && door != null)
        {
            Vector3 targetPos = isOpen ? openPosition : closedPosition;
            door.position = Vector3.Lerp(door.position, targetPos, Time.deltaTime * moveSpeed);
        }
    }

    void OpenDoor()
    {
        if (!isOpen)
        {
            isOpen = true;
            if (useAnimation && doorAnimator != null)
                doorAnimator.SetBool("isOpen", true);
        }
    }

    void CloseDoor()
    {
        if (isOpen)
        {
            isOpen = false;
            if (useAnimation && doorAnimator != null)
                doorAnimator.SetBool("isOpen", false);
        }
    }
}
