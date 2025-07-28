using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float activeBoostPower;
    [Header("Level Manager")]
    [SerializeField] private int currentLevel = 0;
    [SerializeField] private int maxLevel;
    [SerializeField] private int currentExp;
    [SerializeField] private List<int> expToNextLevels;
    [Header("Health Settings")]
    [SerializeField] private int currentHealth;
    [SerializeField] private int maxHealth;
    [Header("Energy Settings")]
    [SerializeField] private float currentEnergy;
    [SerializeField] private float maxEnergy;
    [SerializeField] private float energyRegeneration;
    [SerializeField] private float energyUsage;
    [SerializeField] private float energyRequiredForBoost = 10f;

    private bool isBoosting = false;
    private float baseBoostPower = 1f;
    private float boost;
    private float dirX, dirY;
    public bool isLevelUp = false;

    // Define components
    private Rigidbody2D playerRigidbody2D;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        // Get components
        playerRigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Generate level system
        for (int i = expToNextLevels.Count; i < maxLevel; i++)
        {
            expToNextLevels.Add(Mathf.CeilToInt(expToNextLevels[expToNextLevels.Count - 1] * 1.5f));
        }
    }

    private void Start()
    {
        // Set start value
        currentHealth = maxHealth;
        currentEnergy = maxEnergy;
        boost = baseBoostPower;

        // Display UI
        UIController.instance.DisplayEnergyBar();
        UIController.instance.DisplayHealthBar();
        UIController.instance.DisplayEXPBar();
    }

    private void Update()
    {
        if (GameManager.instance.IsPaused() || GameManager.instance.IsGameOVer()) return;
        dirX = Input.GetAxisRaw("Horizontal");
        dirY = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.Space) && currentEnergy > energyRequiredForBoost)
        {
            ActivateBoost();
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            DeactivateBoost();
        }

        // temp check lose condition
        if (currentHealth <= 0)
        {
            Die();
        }

        // Animation
        HandleAnimation();
    }

    private void FixedUpdate()
    {
        playerRigidbody2D.linearVelocity = new Vector2(dirX, dirY).normalized * moveSpeed;
        HandleEnergy();
        UIController.instance.DisplayEnergyBar();
        UIController.instance.DisplayHealthBar();
        UIController.instance.DisplayEXPBar();
    }

    private void HandleAnimation()
    {
        animator.SetFloat("dirX", dirX);
        animator.SetFloat("dirY", dirY);
        if (dirX != 0 || dirY != 0)
            animator.SetBool("isMoving", true);
        else
            animator.SetBool("isMoving", false);
    }

    private void ActivateBoost()
    {
        animator.SetBool("isBoosting", true);
        boost = activeBoostPower;
        isBoosting = true;

        AudioManager.instance.PlaySound(AudioManager.instance._boostEngine);

        // Play engine boosting particles
        ParticleManager.instance.PlayParticle(ParticleManager.instance.mainEngineBoostParticle);
        ParticleManager.instance.PlayParticle(ParticleManager.instance.upEngineBoostParticle);
        ParticleManager.instance.PlayParticle(ParticleManager.instance.downEngineBoostParticle);
    }

    public void DeactivateBoost()
    {
        animator.SetBool("isBoosting", false);
        boost = baseBoostPower;
        isBoosting = false;

        AudioManager.instance.StopSound();

        // Stop all engine particles
        ParticleManager.instance.StopParticle(ParticleManager.instance.mainEngineBoostParticle);
        ParticleManager.instance.StopParticle(ParticleManager.instance.upEngineBoostParticle);
        ParticleManager.instance.StopParticle(ParticleManager.instance.downEngineBoostParticle);
    }

    private void HandleEnergy()
    {
        if (isBoosting)
        {
            if (currentEnergy > energyUsage)
                currentEnergy -= energyUsage;
            else
                DeactivateBoost();
        }
        else
        {
            if (currentEnergy < maxEnergy)
                currentEnergy += energyRegeneration;
        }
        currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);
    }

    public void TakeDamage(int amount)
    {
        currentHealth = currentHealth - amount >= 0 ? currentHealth - amount : 0;
        FlashEffect.instance.CallFlashEffect(spriteRenderer);
        UIController.instance.DisplayHealthBar();
        AudioManager.instance.PlayAdjustedSound(AudioManager.instance._playerHit);
    }

    private void Die()
    {
        EffectManager.instance.RunExplosion01Effect(transform.position);
        AudioManager.instance.PlayAdjustedSound(AudioManager.instance._playerExplode);
        DeactivateBoost();
        GameManager.instance.SetGameOver();
        gameObject.SetActive(false);
    }

    public void AddExp(int amount)
    {
        currentExp += amount;
        if (currentExp >= expToNextLevels[currentLevel])
        {
            LevelUp();
        }
        UIController.instance.DisplayEXPBar();
    }

    private void LevelUp()
    {
        isLevelUp = true;
        currentExp -= expToNextLevels[currentLevel];
        currentLevel++;
        AudioManager.instance.PlaySound(AudioManager.instance._levelUp);
        if (!GameManager.instance.IsGameOVer())
            UIController.instance.ActivatePowerUpPanel();
    }

    public bool IsBoosting() => isBoosting;
    public float GetBoost() => boost;
    public float GetCurrentEnergy() => currentEnergy;
    public float GetMaxEnergy() => maxEnergy;
    public void SetMaxEnergy(float val) { maxEnergy = val; }
    public float GetEnergyRequiredForBoost() => energyRequiredForBoost;
    public float GetEnergyRegeneration() => energyRegeneration;
    public void SetEnergyRegeneration(float val) { energyRegeneration = val; }

    public int GetCurrentHealth() => currentHealth;
    public int GetMaxHealth() => maxHealth;
    public void SetCurrentHealth(int value) { currentHealth = value; }

    public int GetCurrentExp() => currentExp;
    public int GetMaxExp() => expToNextLevels[currentLevel];
    public int GetCurrentLevel() => currentLevel + 1;
    public float GetSpeed() => moveSpeed;
    public void SetSpeed(float val) { moveSpeed = val; }
}