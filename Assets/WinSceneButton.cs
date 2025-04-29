using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinSceneButton : MonoBehaviour
{
    public Button yourButton;  // Reference to the button in the Win scene

    void Start()
    {
        if (yourButton != null)
        {
            yourButton.onClick.AddListener(LoadScene0);
        }
    }

    void LoadScene0()
    {
        // Load Scene 0
        SceneManager.LoadScene(0);
    }
}
