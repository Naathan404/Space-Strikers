using UnityEngine;

public class SmallShip : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerController playerController;
    [Header("Stats")]
    [SerializeField] private float speed;
    [SerializeField] private float targetPosX;
    [SerializeField] private float fireCoolDown;
    private float timer = 0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerController = FindAnyObjectByType<PlayerController>();
    }

    private void FixedUpdate()
    {
        if (transform.position.x > targetPosX)
            rb.linearVelocity = new Vector2(-speed * playerController.GetBoost(), rb.linearVelocity.y);
        else
        {
            rb.linearVelocity = Vector2.zero;
            timer += Time.fixedDeltaTime;
            if (timer > fireCoolDown)
            {
                Fire();
                timer = 0f;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            AudioManager.instance.PlaySound(AudioManager.instance._explode1);
            EffectManager.instance.RunExplosion03Effect(transform.position);
            playerController.AddExp(1);
            gameObject.SetActive(false);
            collision.gameObject.SetActive(false);
        }
    }

    private void Fire()
    {
        GameObject bullet = GameObject.Find("EnemyBulletPooler").GetComponent<ObjectPooler>().GetPooledObject();
        bullet.transform.position = transform.position;
        bullet.SetActive(true);
    }
}