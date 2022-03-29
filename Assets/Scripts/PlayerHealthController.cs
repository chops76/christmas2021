using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    public int currentHealth;
    public int maxHealth;
    public float damageInvincLength = 1;
    private float invincCount;

    private void Awake()
    {
        instance = this;
    }

    public void SetInvincible(float invTime)
    {
        invincCount = invTime;
        PlayerController.instance.setCharacterAlpha(.3f);
    }

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = CharacterTracker.instance.maxHealth;
        currentHealth = CharacterTracker.instance.currentHealth;

        UpdateHealthUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (invincCount > 0)
        {
            invincCount -= Time.deltaTime;
            if (invincCount <= 0)
            {
                PlayerController.instance.setCharacterAlpha(1f);
            }
        }
    }

    public void DamagePlayer()
    {
        if (invincCount <= 0)
        {
            SetInvincible(damageInvincLength);
            currentHealth--;

            if (currentHealth <= 0)
            {
                AudioManager.instance.PlaySFX(AudioManager.SFX.PlayerDeath);
                PlayerController.instance.gameObject.SetActive(false);
                UIController.instance.deathScreen.SetActive(true);
                AudioManager.instance.PlayGameOver();
            }
            else
            {
                AudioManager.instance.PlaySFX(AudioManager.SFX.PlayerHurt);
            }
            UpdateHealthUI();
        }
    }

    private void UpdateHealthUI()
    {
        UIController.instance.healthSider.maxValue = maxHealth;
        UIController.instance.healthSider.value = currentHealth;
        UIController.instance.healthText.text = currentHealth + " / " + maxHealth;
    }

    public void healPlayer(int amount)
    {
        currentHealth = Math.Min(currentHealth + amount, maxHealth);
        UpdateHealthUI();
    }

    public void increaseMaxHealth(int amount)
    {
        maxHealth += amount;
        currentHealth = maxHealth;
        UpdateHealthUI();
    }
}


