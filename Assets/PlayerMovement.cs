using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementWithStaminaUI : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float sprintSpeed = 9f;
    public float maxStamina = 100f;
    public float staminaDrain = 20f;
    public float staminaRegen = 15f;
    public float sprintFov = 90f;
    public float walkFov = 60f;
    public float crouchHeight = 1f;
    public float standingHeight = 2f;
    public float crouchSpeed = 2.5f;
    public float zoomFov = 30f;
    public float zoomSpeed = 8f;
    public KeyCode zoomKey = KeyCode.Mouse1;
    private bool isZooming = false;

    private bool isCrouching = false;
    private CapsuleCollider playerCollider;

    public Slider staminaSlider;
    [HideInInspector]
    public bool isSprinting = false;

    private float stamina;
    private float currentSpeed;
    private Rigidbody rb;
    private Camera playerCamera;
    private Vector3 movement;

    public float walkBobAmount = 0.05f;
    public float sprintBobAmount = 0.1f;
    public float bobSpeed = 14f;

    private float defaultYPosition;
    private float timer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCamera = Camera.main;

        if (playerCamera == null)
        {
            Debug.LogError("Main Camera not found! Make sure there is a Camera with the 'MainCamera' tag.");
        }

        stamina = maxStamina;
        currentSpeed = walkSpeed;

        if (staminaSlider != null)
        {
            staminaSlider.maxValue = maxStamina;
        }
        else
        {
            Debug.LogError("Stamina Slider is not assigned! Please assign it in the Inspector.");
        }

        playerCollider = GetComponent<CapsuleCollider>();
        if (playerCollider == null)
        {
            Debug.LogError("No CapsuleCollider found on the player!");
        }

        defaultYPosition = playerCamera.transform.localPosition.y;
    }

    void Update()
    {
        HandleMovementInput();
        HandleSprint();
        UpdateStaminaUI();
        AdjustCameraFov();
        ApplyCameraBobbing();
        HandleCrouch();
        HandleZoom();
    }

    void HandleMovementInput()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 forward = playerCamera.transform.forward;
        Vector3 right = playerCamera.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        movement = forward * moveZ + right * moveX;
    }

    void HandleSprint()
    {
        if (isCrouching)
        {
            currentSpeed = crouchSpeed;
            return;
        }

        bool wantsToSprint = Input.GetKey(KeyCode.LeftShift);
        bool isMoving = movement.magnitude > 0;

        if (wantsToSprint && stamina > 0 && isMoving)
        {
            currentSpeed = sprintSpeed;
            stamina -= staminaDrain * Time.deltaTime;
            stamina = Mathf.Clamp(stamina, 0, maxStamina);
            isSprinting = true;
        }
        else
        {
            currentSpeed = walkSpeed;
            stamina += staminaRegen * Time.deltaTime;
            stamina = Mathf.Clamp(stamina, 0, maxStamina);
            isSprinting = false;
        }

        if (stamina <= 0)
        {
            isSprinting = false;
            currentSpeed = walkSpeed;
        }
    }

    void HandleCrouch()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (!isCrouching)
            {
                isCrouching = true;
                playerCollider.height = crouchHeight;
                currentSpeed = crouchSpeed;
            }
        }
        else
        {
            if (isCrouching)
            {
                isCrouching = false;
                playerCollider.height = standingHeight;
                currentSpeed = walkSpeed;
            }
        }
    }

    void FixedUpdate()
    {
        if (movement.magnitude > 0)
        {
            Vector3 velocity = movement.normalized * currentSpeed;
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
    }

    void UpdateStaminaUI()
    {
        if (staminaSlider != null)
            staminaSlider.value = stamina;
    }

    void AdjustCameraFov()
    {
        if (playerCamera != null)
        {
            float targetFov = isSprinting ? sprintFov : walkFov;
            playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, targetFov, Time.deltaTime * 5f);
        }
    }

    void HandleZoom()
    {
        if (Input.GetKey(zoomKey))
        {
            isZooming = true;
        }
        else
        {
            isZooming = false;
        }

        float targetFov = isZooming ? zoomFov : (isSprinting ? sprintFov : walkFov);
        playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, targetFov, Time.deltaTime * zoomSpeed);
    }

    void ApplyCameraBobbing()
    {
        if (movement.magnitude > 0)
        {
            float bobAmount = isSprinting ? sprintBobAmount : walkBobAmount;
            float waveFactor = Mathf.Sin(timer * bobSpeed);
            playerCamera.transform.localPosition = new Vector3(
                playerCamera.transform.localPosition.x,
                defaultYPosition + waveFactor * bobAmount,
                playerCamera.transform.localPosition.z
            );

            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
            playerCamera.transform.localPosition = new Vector3(
                playerCamera.transform.localPosition.x,
                defaultYPosition,
                playerCamera.transform.localPosition.z
            );
        }
    }
}
