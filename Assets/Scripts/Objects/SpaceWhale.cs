using UnityEngine;

public class SpaceWhale : MonoBehaviour
{
    private PlayerController playerController;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerController = FindAnyObjectByType<PlayerController>();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(-0.5f * GameManager.instance.GetWorldSpeed() * playerController.GetBoost(), rb.linearVelocity.y);
        if (Mathf.Abs(transform.position.x) > 16f || Mathf.Abs(transform.position.y) > 7f)
            gameObject.SetActive(false);
    }
}
