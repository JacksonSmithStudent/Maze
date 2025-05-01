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

    // Time thresholds in seconds
    private float openTime = 10f * 60f;      // 10 minutes
    private float closeTime = 25f * 60f;     // 25 minutes
    private float cycleTime = 25f * 60f;     // Full cycle (25 minutes)

    void Start()
    {
        timer = 0f;
        isOpen = true;

        if (!useAnimation && door != null)
        {
            door.position = openPosition;
        }
        else if (useAnimation && doorAnimator != null)
        {
            doorAnimator.SetBool("isOpen", true);
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer < openTime)
        {
            OpenDoor(); // From 0 to 10 minutes
        }
        else if (timer >= openTime && timer < closeTime)
        {
            CloseDoor(); // From 10 to 25 minutes
        }

        if (timer >= cycleTime)
        {
            timer = 0f;
            OpenDoor(); // Reopen at the start of the next cycle
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
            {
                doorAnimator.SetBool("isOpen", true);
            }
        }
    }

    void CloseDoor()
    {
        if (isOpen)
        {
            isOpen = false;
            if (useAnimation && doorAnimator != null)
            {
                doorAnimator.SetBool("isOpen", false);
            }
        }
    }
}
