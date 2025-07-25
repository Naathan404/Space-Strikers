using System;
using System.Collections;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float activeBoostPower;
    [Header("Health Settings")]
    [SerializeField] private int currentHealth;
    [SerializeField] private int maxHealth;
    [Header("Energy Settings")]
    [SerializeField] private float currentEnergy;
    [SerializeField] private float maxEnergy;
    [SerializeField] private float energyRegeneration;
    [SerializeField] private float energyUsage;
    [SerializeField] private float energyRequiredForBoost = 10f;
    [Header("Attack Variables")]
    [SerializeField] private float fireCooldown;
    private float fireTimer;

    private bool isBoosting = false;
    private float baseBoostPower = 1f;
    private float boost;
    private float dirX, dirY;

    // Define components
    private Rigidbody2D playerRigidbody2D;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        // Get components
        playerRigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Set start value
        currentHealth = maxHealth;
        currentEnergy = maxEnergy;
        boost = baseBoostPower;
        fireTimer = fireCooldown;

        // Display UI
        UIController.instance.DisplayEnergyBar();
        UIController.instance.DisplayHealthBar();
    }

    private void Update()
    {
        if (GameManager.instance.IsPaused()) return;
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

        // Attack
        fireTimer -= Time.deltaTime;
        if (fireTimer <= 0f)
        {
            Gun.instance.Fire(transform.position);
            fireTimer = fireCooldown;
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
    }

    private void HandleAnimation()
    {
        animator.SetFloat("dirX", dirX);
        animator.SetFloat("dirY", dirY);
        if(dirX != 0 || dirY != 0)
            animator.SetBool("isMoving", true);
        else
            animator.SetBool("isMoving", false);
    }

    private void ActivateBoost()
    {
        animator.SetBool("isBoosting", true);
        boost = activeBoostPower;
        isBoosting = true;

        AudioManager.instance.PlaySound(AudioManager.instance.boostEngine);

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
    }

    public void TakeDamage(int amount)
    {
        currentHealth = currentHealth - amount >= 0 ? currentHealth - amount : 0;
        FlashEffect.instance.CallFlashEffect(spriteRenderer);

        AudioManager.instance.PlayAdjustedSound(AudioManager.instance._playerHit);
    }

    private void Die()
    {
        EffectManager.instance.RunExplosion01Effect(transform.position);
        AudioManager.instance.PlaySound(AudioManager.instance._playerExplode);
        DeactivateBoost();
        gameObject.SetActive(false);
    }

    public bool IsBoosting() => isBoosting;
    public float GetBoost() => boost;
    public float GetCurrentEnergy() => currentEnergy;
    public float GetMaxEnergy() => maxEnergy;
    public float GetEnergyRequiredForBoost() => energyRequiredForBoost;

    public int GetCurrentHealth() => currentHealth;
    public int GetMaxHealth() => maxHealth;
}