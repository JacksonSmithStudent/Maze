using UnityEngine;

public class InteractableItem : MonoBehaviour
{
    public string itemName = "Item";
    public bool isPickable = true;
    public enum ItemType { Food, Drink }
    public ItemType itemType;

    public float restoreAmount = 20f;

    private PlayerStatus playerStatus;

    private bool isAvailable = true;
    private float respawnTime = 5f;
    private float timeSinceLastInteraction = 0f;

    private Renderer[] renderers;
    private Collider[] colliders;

    void Start()
    {
        playerStatus = FindObjectOfType<PlayerStatus>();

        
        renderers = GetComponentsInChildren<Renderer>();
        colliders = GetComponentsInChildren<Collider>();

        if (playerStatus == null)
        {
            Debug.LogError("PlayerStatus not found in the scene.");
        }
    }

    void Update()
    {
        if (!isAvailable)
        {
            timeSinceLastInteraction += Time.deltaTime;
            if (timeSinceLastInteraction >= respawnTime)
            {
                isAvailable = true;
                timeSinceLastInteraction = 0f;
                SetItemVisible(true);
                Debug.Log($"{itemName} has respawned.");
            }
        }
    }

    public void Interact()
    {
        if (isAvailable)
        {
            if (itemType == ItemType.Food)
            {
                playerStatus.ReplenishHunger(restoreAmount);
            }
            else if (itemType == ItemType.Drink)
            {
                playerStatus.ReplenishThirst(restoreAmount);
            }

            isAvailable = false;
            timeSinceLastInteraction = 0f;
            SetItemVisible(false);

            Debug.Log($"{itemName} picked up. Replenishing {itemType}");
        }
    }

    private void SetItemVisible(bool visible)
    {
        foreach (Renderer r in renderers)
            r.enabled = visible;

        foreach (Collider c in colliders)
            c.enabled = visible;
    }
}
