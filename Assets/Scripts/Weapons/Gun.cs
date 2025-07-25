using UnityEngine;

public class Gun : MonoBehaviour
{
    public static Gun instance;
    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;
    }

    public void Fire(Vector2 pos)
    {
        AudioManager.instance.PlayAdjustedSound(AudioManager.instance._playerAttack);
        GameObject bullet = GameObject.Find("BasicGun").GetComponent<ObjectPooler>().GetPooledObject();
        bullet.transform.position = pos;
        bullet.SetActive(true);
    }
}
