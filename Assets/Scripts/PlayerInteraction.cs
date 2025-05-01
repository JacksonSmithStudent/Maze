using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    public float interactDistance = 3f;
    public KeyCode interactKey = KeyCode.E;
    public Text interactText;

    private Camera playerCamera;

    void Start()
    {
        playerCamera = Camera.main;

        if (interactText != null)
        {
            interactText.text = "";
        }
        else
        {
            Debug.LogWarning("Interact Text UI is not assigned in the inspector.");
        }
    }

    void Update()
    {
        HandleInteraction();
    }

    void HandleInteraction()
    {
        RaycastHit hit;
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            InteractableItem interactableItem = hit.collider.GetComponent<InteractableItem>();
            if (interactableItem != null)
            {
                if (interactText != null)
                {
                    interactText.text = $"Press '{interactKey}' to pick up {interactableItem.itemName}";
                }

                if (Input.GetKeyDown(interactKey))
                {
                    interactableItem.Interact(); // No need for 'playerStatus' argument anymore
                }
            }
        }
        else
        {
            if (interactText != null)
            {
                interactText.text = "";
            }
        }
    }
}
