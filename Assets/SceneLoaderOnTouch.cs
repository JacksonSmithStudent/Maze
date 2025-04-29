using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderOnTouch : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // Check if the object colliding with the trigger has the "Player" tag
        if (other.CompareTag("Player"))
        {
            // Load the "Win" scene
            SceneManager.LoadScene("Win");
        }
    }
}
