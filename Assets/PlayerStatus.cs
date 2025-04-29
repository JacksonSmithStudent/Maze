using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    public float maxHunger = 100f;
    public float maxThirst = 100f;
    public float hunger = 100f;
    public float thirst = 100f;

    public Slider hungerSlider; // Assign in Inspector
    public Slider thirstSlider; // Assign in Inspector
    public Slider exhaustionSlider; // New Slider for Exhaustion - Assign in Inspector

    private PlayerMovementWithStaminaUI movementScript; // Reference to PlayerMovement script
    public float normalThirstDepletionRate = 0.03f; // Normal rate at which thirst depletes
    public float sprintThirstDepletionRate = 0.06f; // Faster depletion when sprinting

    // Exhaustion-related variables
    public float exhaustion = 0f; // Current exhaustion level (0 to 1)
    public float exhaustionIncreaseRate = 0.1f; // Rate at which exhaustion increases when hunger is low
    public float maxExhaustion = 1f; // Max exhaustion value (1 represents 100% exhaustion)

    // Stamina and vision effects related to exhaustion
    public float maxStamina = 100f;
    private float stamina;
    public float staminaDrain = 20f;
    public float staminaRegen = 15f;
    private Camera playerCamera;
    public float blurAmount = 0.5f; // Vision blur intensity

    void Start()
    {
        // Get the PlayerMovementWithStaminaUI script from the player
        movementScript = GetComponent<PlayerMovementWithStaminaUI>();

        if (hungerSlider != null) hungerSlider.maxValue = maxHunger;
        if (thirstSlider != null) thirstSlider.maxValue = maxThirst;
        if (exhaustionSlider != null) exhaustionSlider.maxValue = maxExhaustion; // Set max value for exhaustion slider

        playerCamera = Camera.main; // Reference to the main camera

        stamina = maxStamina;
        UpdateUI();
    }

    void Update()
    {
        // Handle thirst depletion
        float thirstDepletionRate = movementScript != null && movementScript.isSprinting ? sprintThirstDepletionRate : normalThirstDepletionRate;
        thirst -= Time.deltaTime * thirstDepletionRate;

        // Gradually decrease hunger over time
        hunger -= Time.deltaTime * 0.01f;

        // Check for exhaustion based on hunger level
        if (hunger < 45f)
        {
            // Increase exhaustion when hunger is low
            exhaustion += Time.deltaTime * exhaustionIncreaseRate;
            exhaustion = Mathf.Clamp(exhaustion, 0f, maxExhaustion);

            // Modify stamina drain and regeneration based on exhaustion
            staminaDrain = 20f + exhaustion * 30f; // More stamina drain with higher exhaustion
            staminaRegen = 15f - exhaustion * 10f; // Slower stamina regen with higher exhaustion
        }
        else
        {
            // No exhaustion when hunger is above 45
            exhaustion = 0f;
            staminaDrain = 20f;
            staminaRegen = 15f;
        }

        // Apply vision blur if exhaustion is high
        ApplyVisionBlur();

        // Ensure values stay within bounds
        hunger = Mathf.Clamp(hunger, 0f, maxHunger);
        thirst = Mathf.Clamp(thirst, 0f, maxThirst);
        exhaustion = Mathf.Clamp(exhaustion, 0f, maxExhaustion);

        UpdateUI();
    }

    public void ReplenishHunger(float amount)
    {
        hunger = Mathf.Clamp(hunger + amount, 0f, maxHunger);
        UpdateUI();
    }

    public void ReplenishThirst(float amount)
    {
        thirst = Mathf.Clamp(thirst + amount, 0f, maxThirst);
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (hungerSlider != null) hungerSlider.value = hunger;
        if (thirstSlider != null) thirstSlider.value = thirst;
        if (exhaustionSlider != null) exhaustionSlider.value = exhaustion; // Update exhaustion slider
    }

    // Apply vision blur based on exhaustion
    void ApplyVisionBlur()
    {
        if (playerCamera != null)
        {
            if (exhaustion > 0.5f)
            {
                // Reduce the camera FOV to simulate blurred vision
                playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, 50f, Time.deltaTime * 2f);
            }
            else
            {
                playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, 60f, Time.deltaTime * 2f);
            }
        }
    }
}
