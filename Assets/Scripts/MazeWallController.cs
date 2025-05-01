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
    public float nightCycleDuration = 10f * 60f; 
    private bool canChangeWalls = true; 

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
      
        timer += Time.deltaTime;

        if (timer >= nightCycleDuration && canChangeWalls)
        {
            StartNightCycle();
            canChangeWalls = false; 
            timer = 0f; 
        }
    }

    void ToggleWalls()
    {
        for (int i = 0; i < wallsToTurnOff; i++)
        {
            if (activeWalls.Count == 0) break;

            int randomIndex = Random.Range(0, activeWalls.Count);
            GameObject wall = activeWalls[randomIndex];

            wall.SetActive(false);

            activeWalls.RemoveAt(randomIndex);
            inactiveWalls.Add(wall);
        }

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
