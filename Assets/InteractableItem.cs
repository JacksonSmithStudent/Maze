using UnityEngine;

public class InteractableItem : MonoBehaviour
{
    public string itemName = "Item"; // Name of the item
    public bool isPickable = true;  // Whether the item can be picked up
    public enum ItemType { Food, Drink }  // Type of item
    public ItemType itemType;

    public float restoreAmount = 20f; // Amount to restore (hunger or thirst)

    // This method will handle what happens when the item is interacted with
    public void Interact(PlayerStatus playerStatus)
    {
        // Depending on the item type, replenish hunger or thirst
        if (itemType == ItemType.Food)
        {
            playerStatus.ReplenishHunger(restoreAmount); // Increase hunger
        }
        else if (itemType == ItemType.Drink)
        {
            playerStatus.ReplenishThirst(restoreAmount); // Increase thirst
        }

        // Destroy the item after interaction
        Debug.Log($"Picked up: {itemName}");
        Destroy(gameObject);
    }
}
