using UnityEngine;
using System.Collections.Generic;

public class MazeWallController : MonoBehaviour
{
    public GameObject mazeWallsParent;
    private List<GameObject> allWalls = new List<GameObject>();
    private List<GameObject> activeWalls = new List<GameObject>();
    private List<GameObject> inactiveWalls = new List<GameObject>();

    public int wallsToTurnOn = 10;
    public int wallsToTurnOff = 15;
    private float timer = 0f;
    public float nightCycleDuration = 10f * 60f; // 10 minutes cycle
    private bool canChangeWalls = true; // Flag to track if we can change walls

    void Start()
    {
        foreach (Transform child in mazeWallsParent.transform)
        {
            allWalls.Add(child.gameObject);
        }

        UpdateWallLists();
    }

    void Update()
    {
        // Increment the timer with delta time
        timer += Time.deltaTime;

        // Check if 10 minutes have passed and if it's allowed to change walls
        if (timer >= nightCycleDuration && canChangeWalls)
        {
            StartNightCycle();
            canChangeWalls = false; // Prevent changes until the next cycle
            timer = 0f; // Reset the timer for the next cycle
        }
    }

    void ToggleWalls()
    {
        // Turn off a number of active walls
        for (int i = 0; i < wallsToTurnOff; i++)
        {
            if (activeWalls.Count == 0) break;

            int randomIndex = Random.Range(0, activeWalls.Count);
            GameObject wall = activeWalls[randomIndex];

            wall.SetActive(false);

            activeWalls.RemoveAt(randomIndex);
            inactiveWalls.Add(wall);
        }

        // Turn on a number of inactive walls
        for (int i = 0; i < wallsToTurnOn; i++)
        {
            if (inactiveWalls.Count == 0) break;

            int randomIndex = Random.Range(0, inactiveWalls.Count);
            GameObject wall = inactiveWalls[randomIndex];

            wall.SetActive(true);

            inactiveWalls.RemoveAt(randomIndex);
            activeWalls.Add(wall);
        }
    }

    void UpdateWallLists()
    {
        activeWalls.Clear();
        inactiveWalls.Clear();

        foreach (GameObject wall in allWalls)
        {
            if (wall.activeSelf)
            {
                activeWalls.Add(wall);
            }
            else
            {
                inactiveWalls.Add(wall);
            }
        }
    }

    void StartNightCycle()
    {
        UpdateWallLists();
        ToggleWalls();
    }
}
