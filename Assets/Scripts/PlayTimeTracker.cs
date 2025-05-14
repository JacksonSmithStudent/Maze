using UnityEngine;
using UnityEngine.UI;

public class PlayTimeTracker : MonoBehaviour
{
    public Text timerText;
    private float playTime;

    void Update()
    {
        playTime += Time.deltaTime;

        int hours = (int)(playTime / 3600);
        int minutes = (int)(playTime / 60) % 60;
        int seconds = (int)(playTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }
}
