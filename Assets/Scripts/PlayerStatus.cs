using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour
{
    public float maxHunger = 100f;
    public float maxThirst = 100f;
    public float hunger = 100f;
    public float thirst = 100f;

    public Slider hungerSlider; 
    public Slider thirstSlider;
    public Slider exhaustionSlider; 

    private PlayerMovementWithStaminaUI movementScript; 
    public float normalThirstDepletionRate = 0.03f; 
    public float sprintThirstDepletionRate = 0.06f; 

   
    public float exhaustion = 0f; 
    public float exhaustionIncreaseRate = 0.1f;
    public float maxExhaustion = 1f; 

   
    public float maxStamina = 100f;
    private float stamina;
    public float staminaDrain = 20f;
    public float staminaRegen = 15f;
    private Camera playerCamera;
    public float blurAmount = 0.5f; 

    void Start()
    {
        
        movementScript = GetComponent<PlayerMovementWithStaminaUI>();

        if (hungerSlider != null) hungerSlider.maxValue = maxHunger;
        if (thirstSlider != null) thirstSlider.maxValue = maxThirst;
        if (exhaustionSlider != null) exhaustionSlider.maxValue = maxExhaustion; 

        playerCamera = Camera.main; 

        stamina = maxStamina;
        UpdateUI();
    }

    void Update()
    {
        float thirstDepletionRate = movementScript != null && movementScript.isSprinting ? sprintThirstDepletionRate : normalThirstDepletionRate;
        thirst -= Time.deltaTime * thirstDepletionRate;

        hunger -= Time.deltaTime * 0.01f;


        if (hunger <= 0f || thirst <= 0f)
        {
            SceneManager.LoadScene("Lose");
            return;
        }

        if (hunger < 45f)
        {
            exhaustion += Time.deltaTime * exhaustionIncreaseRate;
            exhaustion = Mathf.Clamp(exhaustion, 0f, maxExhaustion);

            staminaDrain = 20f + exhaustion * 30f;
            staminaRegen = 15f - exhaustion * 10f;
        }
        else
        {
            exhaustion = 0f;
            staminaDrain = 20f;
            staminaRegen = 15f;
        }

        ApplyVisionBlur();

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
        if (exhaustionSlider != null) exhaustionSlider.value = exhaustion; 
    }

    
    void ApplyVisionBlur()
    {
        if (playerCamera != null)
        {
            if (exhaustion > 0.5f)
            {
                
                playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, 50f, Time.deltaTime * 2f);
            }
            else
            {
                playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, 60f, Time.deltaTime * 2f);
            }
        }
    }
}
