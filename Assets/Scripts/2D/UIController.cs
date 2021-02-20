using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject uiPrefab;
    public Image healthImage;
    public Image staminaImage;
    public FloatVariable maxStamina;
    public FloatVariable stamina;
    public FloatVariable maxHealth;
    public FloatVariable health;
    public float lerpSpd = 7f;

    public void SwitchUI(PlayerController2D player)
    {
        maxStamina = player.maxStamina;
        stamina = player.GetStamina();
        maxHealth = player.maxHealth;
        health = player.GetHealth();
    }

    void Update()
    {
        staminaImage.fillAmount = Mathf.Lerp(staminaImage.fillAmount, (stamina.Value / maxStamina.Value), Time.deltaTime * lerpSpd);
        healthImage.fillAmount = Mathf.Lerp(healthImage.fillAmount, (health.Value / maxHealth.Value), Time.deltaTime* lerpSpd);
    }
}