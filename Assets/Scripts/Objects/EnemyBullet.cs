using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float speed;
    private PlayerController playerController;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerController = FindAnyObjectByType<PlayerController>();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(-speed * playerController.GetBoost(), rb.linearVelocity.y);

        if (transform.position.x < -10f)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AudioManager.instance.PlayAdjustedSound(AudioManager.instance._playerHit);
            playerController.TakeDamage(1);
            gameObject.SetActive(false);
        }
    }
}
