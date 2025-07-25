using UnityEngine;

public class BasicBullet : BaseBullet
{
    protected override void Move()
    {
        base.Move();
        rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
    }
}
