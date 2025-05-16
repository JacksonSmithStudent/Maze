using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public int Index;

    public void LoadScene()
    {
        SceneManager.LoadScene(Index);
    }
}
