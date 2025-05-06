using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject inventoryItemPrefab;
    public List<GameObject> inventory = new List<GameObject>();
    private bool inventoryOpen = false;

    public Camera playerCamera;
    private bool isMouseUnlocked = false;

    void Start()
    {
        inventoryPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory();
        }

        if (isMouseUnlocked)
        {
            return;
        }
    }

    public void AddItem(GameObject itemPrefab)
    {
        GameObject itemInstance = Instantiate(itemPrefab, inventoryPanel.transform);
        inventory.Add(itemInstance);

        if (inventoryOpen)
        {
            UpdateInventoryUI();
        }
    }

    void ToggleInventory()
    {
        inventoryOpen = !inventoryOpen;
        inventoryPanel.SetActive(inventoryOpen);

        if (inventoryOpen)
        {
            UpdateInventoryUI();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            isMouseUnlocked = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            isMouseUnlocked = false;
        }
    }

    void UpdateInventoryUI()
    {
        foreach (Transform child in inventoryPanel.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (var item in inventory)
        {
            GameObject newItem = Instantiate(item, inventoryPanel.transform);
            newItem.transform.SetParent(inventoryPanel.transform);
        }
    }
}
