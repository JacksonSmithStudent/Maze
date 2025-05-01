using UnityEngine;

public class SkyboxCycle : MonoBehaviour
{
    public Material daySkybox;
    public Material sunsetSkybox;
    public Material nightSkybox;
    public Material sunriseSkybox;

    private float timer = 0f;

    private float dayLength = 7f * 60f;    
    private float sunsetLength = 3f * 60f; 
    private float nightLength = 12f * 60f; 
    private float sunriseLength = 3f * 60f; 

    private float fullDayLength;

    void Start()
    {
        fullDayLength = dayLength + sunsetLength + nightLength + sunriseLength;
    }

    void Update()
    {
        timer += Time.deltaTime;
        float timeInCycle = timer % fullDayLength;

        if (timeInCycle < dayLength)
        {
            RenderSettings.skybox = daySkybox;
        }
        else if (timeInCycle < dayLength + sunsetLength)
        {
            RenderSettings.skybox = sunsetSkybox;
        }
        else if (timeInCycle < dayLength + sunsetLength + nightLength)
        {
            RenderSettings.skybox = nightSkybox;
        }
        else
        {
            RenderSettings.skybox = sunriseSkybox;
        }
    }
}
