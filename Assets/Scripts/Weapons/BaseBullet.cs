using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected int damage;
    protected Rigidbody2D rb;

    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected void FixedUpdate()
    {
        Move();
    }

    protected void Update()
    {
        if (Mathf.Abs(transform.position.x) >= 10f || Mathf.Abs(transform.position.y) >= 6f)
            gameObject.SetActive(false);
    }

    protected virtual void Move()
    {
    }
}
