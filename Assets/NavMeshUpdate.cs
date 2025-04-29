using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshRebaker : MonoBehaviour
{
    public NavMeshSurface navMeshSurface; 
    public float rebakeInterval = 600f; 

    private float timer;

    void Start()
    {
        if (navMeshSurface == null)
        {
            navMeshSurface = FindObjectOfType<NavMeshSurface>();
        }

        timer = rebakeInterval; 
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            navMeshSurface.BuildNavMesh(); 
            timer = rebakeInterval; 
        }
    }
}
