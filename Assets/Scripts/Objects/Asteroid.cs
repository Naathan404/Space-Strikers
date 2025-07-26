using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [Header("Asteroid State")]
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private int maxHP;
    private int currentHP;

    [Header("Sprite & Material")]
    [SerializeField] List<Sprite> asteroidSprites = new List<Sprite>();
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb2D;
    private PlayerController playerController;
    private float rotateDirection;
    private Vector2 dir;
    float scaleFactor;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerController = FindAnyObjectByType<PlayerController>();
    }

    private void Start()
    {
        spriteRenderer.sprite = asteroidSprites[Random.Range(0, asteroidSprites.Count)];
    }

    private void OnEnable()
    {
        currentHP = maxHP;
        rotateDirection = Random.Range(-rotationSpeed, rotationSpeed);
        float dirX = Random.Range(-1, 0f);
        float dirY = Random.Range(-0.5f, 0.5f);
        dir = new Vector2(dirX, dirY);
        rb2D.linearVelocity = dir;

        spriteRenderer.color = Color.white;
        scaleFactor = Random.Range(0.85f, 1.25f);
        transform.localScale = new Vector2(scaleFactor, scaleFactor);
    }

    private void FixedUpdate()
    {
        rb2D.linearVelocity = new Vector2(-1f * GameManager.instance.GetWorldSpeed(), rb2D.linearVelocity.y);
        rb2D.MoveRotation(rb2D.rotation + rotateDirection * Time.fixedDeltaTime);
        if (Mathf.Abs(transform.position.x) > 12f || Mathf.Abs(transform.position.y) > 6f)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            AudioManager.instance.PlayAdjustedSound(AudioManager.instance._playerHit);
            FlashEffect.instance.CallFlashEffect(spriteRenderer);

            currentHP--;
            if (currentHP <= 0)
            {
                Destroyed();
            }

            other.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AudioManager.instance.PlayAdjustedSound(AudioManager.instance._playerHit);

            FlashEffect.instance.CallFlashEffect(spriteRenderer);
            FlashEffect.instance.CallFlashEffect(collision.gameObject.GetComponent<SpriteRenderer>());

            collision.gameObject.GetComponent<PlayerController>().TakeDamage(1);
            UIController.instance.DisplayHealthBar();

            ContactPoint2D contact = collision.GetContact(0);
            Vector2 bounceDir = (rb2D.position - contact.point).normalized;
            rb2D.linearVelocity = bounceDir * 5f;
            collision.gameObject.GetComponent<Rigidbody2D>().linearVelocity = -bounceDir * 3f;

            currentHP -= 3;
            if (currentHP <= 0)
                Destroyed();
        }
    }

    private void Destroyed()
    {
        AudioManager.instance.PlaySound(AudioManager.instance._explode1);
        EffectManager.instance.RunExplosion02Effect(transform.position);
        playerController.AddExp(2);
        gameObject.SetActive(false);
    }
}