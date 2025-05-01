using UnityEngine;

public class RenderDistanceTrigger : MonoBehaviour
{
    public float fogEndDistance = 30f;
    private bool originalFog;
    private Color originalFogColor;
    private float originalFogStart;
    private float originalFogEnd;

    void Start()
    {
        originalFog = RenderSettings.fog;
        originalFogColor = RenderSettings.fogColor;
        originalFogStart = RenderSettings.fogStartDistance;
        originalFogEnd = RenderSettings.fogEndDistance;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RenderSettings.fog = true;
            RenderSettings.fogColor = Color.black;
            RenderSettings.fogMode = FogMode.Linear;
            RenderSettings.fogStartDistance = fogEndDistance * 0.75f;
            RenderSettings.fogEndDistance = fogEndDistance;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RenderSettings.fog = originalFog;
            RenderSettings.fogColor = originalFogColor;
            RenderSettings.fogStartDistance = originalFogStart;
            RenderSettings.fogEndDistance = originalFogEnd;
        }
    }
}
