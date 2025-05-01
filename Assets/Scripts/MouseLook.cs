using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;

    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");

        if (mouseX != 0f || mouseY != 0f)
        {
            float finalMouseX = mouseX * mouseSensitivity * Time.deltaTime;
            float finalMouseY = mouseY * mouseSensitivity * Time.deltaTime;

            xRotation -= finalMouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * finalMouseX);
        }
    }
}
