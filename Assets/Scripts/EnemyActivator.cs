using UnityEngine;

public class EnemyActivator : MonoBehaviour
{
    public GameObject[] enemies;

    private float timer = 0f;
    private bool waitingToEnable = true;

    void Start()
    {
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
                enemy.SetActive(false);
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (waitingToEnable && timer >= 600f)
        {
            foreach (GameObject enemy in enemies)
            {
                if (enemy != null)
                    enemy.SetActive(true);
            }
            timer = 0f;
            waitingToEnable = false;
        }
        else if (!waitingToEnable && timer >= 900f)
        {
            foreach (GameObject enemy in enemies)
            {
                if (enemy != null)
                    enemy.SetActive(false);
            }
            timer = 0f;
            waitingToEnable = true;
        }
    }
}
