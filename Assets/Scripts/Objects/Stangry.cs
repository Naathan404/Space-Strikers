using UnityEngine;

public class Stangry : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerController playerController;
    [Header("Stats")]
    [SerializeField] private float speed;
    [SerializeField] private float waitTime;
    private float timer = 0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerController = FindAnyObjectByType<PlayerController>();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = Vector2.Lerp(transform.position, GameManager.instance.GetPlayerPosition(), speed);
    }
}
