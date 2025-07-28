using UnityEngine;

public class BaseGun : MonoBehaviour
{
    [Header("Gun Variables")]
    [SerializeField] protected int amountOfBullet;
    protected float baseRange = 0.2f;
    [SerializeField] protected float bulletSize = 0.5f;
    [SerializeField] protected float coolDown;

    public int GetAmountOfBullets() => amountOfBullet;
    public void SetAmountOfBullets(int value) { amountOfBullet = value; }
    public float GetBulletSize() => bulletSize;
    public void SetBulletSize(float value) { bulletSize = value; }
    public float GetCoolDown() => coolDown;
    public void SetCoolDown(float value) { coolDown = Mathf.Clamp(value, 0.15f, 0.5f); }
}
