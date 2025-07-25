using UnityEngine;

public class BaseGun : MonoBehaviour
{
    [Header("Gun Variables")]
    [SerializeField] protected int amountOfBullet;
    [SerializeField] protected float range;
    [SerializeField] protected float bulletSize;
    [SerializeField] protected float coolDown;
}
