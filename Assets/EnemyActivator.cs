using UnityEngine;

public class EnemyActivator : MonoBehaviour
{
    public GameObject enemy;

    private float timer = 0f;
    private bool waitingToEnable = true;

    void Start()
    {
        if (enemy == null)
        {
            enemy = this.gameObject;
        }
        enemy.SetActive(false);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (waitingToEnable && timer >= 600f)
        {
            enemy.SetActive(true);
            timer = 0f;
            waitingToEnable = false;
        }
        else if (!waitingToEnable && timer >= 900f)
        {
            enemy.SetActive(false);
            timer = 0f;
            waitingToEnable = true;
        }
    }
}
