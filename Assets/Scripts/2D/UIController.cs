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
    public Text ammoText;
    GunController gun;

    TrainController2D train;
    public Image trainHealth;
    public Text trainText;

    private void Awake()
    {
        train = FindObjectOfType<TrainController2D>();
    }

    public void SwitchUI(PlayerController2D player)
    {
        maxStamina = player.maxStamina;
        stamina = player.GetStamina();
        maxHealth = player.maxHealth;
        health = player.GetHealth();
        gun = player.GetComponentInChildren<GunController>();
    }

    void Update()
    {
        staminaImage.fillAmount = Mathf.Lerp(staminaImage.fillAmount, (stamina.Value / maxStamina.Value), Time.deltaTime * lerpSpd);
        healthImage.fillAmount = Mathf.Lerp(healthImage.fillAmount, (health.Value / maxHealth.Value), Time.deltaTime* lerpSpd);
        if (gun != null) ammoText.text = "Ammo: " + gun.GetCurrentClipSize() + "/" + gun.clipSize;

        if (trainHealth != null) trainHealth.fillAmount = Mathf.Lerp(trainHealth.fillAmount, (train.health / train.maxHealth), Time.deltaTime * lerpSpd);
        if (trainText != null) trainText.text = "Cargo: " + train.health + "/" + train.maxHealth;
    }
}