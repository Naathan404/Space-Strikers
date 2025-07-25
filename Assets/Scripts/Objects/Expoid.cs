using System.Collections.Generic;
using UnityEngine;

public class Expoid : MonoBehaviour
{
    [SerializeField] private float speed;
    private float timer = 0f;
    private Vector2 dir;
    private Rigidbody2D rb;
    private PlayerController playerController;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerController = FindAnyObjectByType<PlayerController>();
    }

    private void OnEnable()
    {
        float scaleFactor = Random.Range(1f, 1.2f);
        transform.localScale = new Vector2(scaleFactor, scaleFactor);
    }

    private void Update()
    {
        if (transform.position.x <= -10f || Mathf.Abs(transform.position.y) >= 6f)
            gameObject.SetActive(false);
        timer += Time.deltaTime;
        if (timer > Random.Range(1.5f, 3f))
        {
            dir = new Vector2(Random.Range(-2f, 0f), Random.Range(-0.6f, 0.6f));
            timer = 0f;
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(dir.x * playerController.GetBoost(), dir.y) * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            AudioManager.instance.PlaySound(AudioManager.instance._hit4);    /// temporary for testing
            EffectManager.instance.RunExplosion02Effect(transform.position);
            gameObject.SetActive(false);
            collision.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            AudioManager.instance.PlaySound(AudioManager.instance._hit3);
            EffectManager.instance.RunExplosion02Effect(transform.position);
            gameObject.SetActive(false);
        }
   }
}
