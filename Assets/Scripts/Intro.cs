using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{
    public List<GameObject> uiPanels; 
    public float displayTime = 5f;

    void Start()
    {
        StartCoroutine(ShowUIPanelsSequentially());
    }

    IEnumerator ShowUIPanelsSequentially()
    {
        foreach (GameObject panel in uiPanels)
        {
            panel.SetActive(true);
            yield return new WaitForSeconds(displayTime);
            panel.SetActive(false);
        }
    }
}
