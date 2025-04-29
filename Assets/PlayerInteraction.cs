using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    public float interactDistance = 3f; // Distance at which the player can interact with objects
    public KeyCode interactKey = KeyCode.E; // Key to interact with objects
    public Text interactText; // UI Text to show interaction prompt (assign in inspector)

    public PlayerStatus playerStatus; // Assign in inspector or found via tag

    private Camera playerCamera;

    void Start()
    {
        playerCamera = Camera.main;

        // If not assigned in inspector, try to find it by tag
        if (playerStatus == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerStatus = player.GetComponent<PlayerStatus>();
            }
            else
            {
                Debug.LogError("Player GameObject with tag 'Player' not found!");
            }
        }

        if (interactText != null)
        {
            interactText.text = ""; // Make sure it's empty at the start
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

                if (Input.GetKeyDown(interactKey) && playerStatus != null)
                {
                    interactableItem.Interact(playerStatus);
                }
            }
        }
        else
        {
            if (interactText != null)
            {
                interactText.text = ""; // Hide prompt
            }
        }
    }
}
