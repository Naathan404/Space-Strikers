using System.Globalization;
using UnityEngine;

public class BasicGun : BaseGun
{
    public static BasicGun instance;
    private float timer;
    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;

        timer = 0f;
    }

    private void Update()
    {
        if (GameManager.instance.IsGameOVer())
        {
            Debug.Log("Game over");
            return;
        }
        timer += Time.deltaTime;
        if (timer > coolDown)
        {
            Fire(GameManager.instance.GetPlayerPosition());
            timer = 0;
        }
    }

    public void Fire(Vector2 pos)
    {
        AudioManager.instance.PlayAdjustedSound(AudioManager.instance._playerAttack);
        for (int i = 0; i < amountOfBullet; i++)
        {
            GameObject bullet = GameObject.Find("BasicBullet").GetComponent<ObjectPooler>().GetPooledObject();
            float spacing, offsetY;
            if (amountOfBullet > 1)
            {
                spacing = (float)range / (amountOfBullet - 1);
                offsetY = range / 2f;
            }
            else
            {
                spacing = 0f;
                offsetY = 0f;
            }
            bullet.transform.position = pos + new Vector2(0, spacing * i - offsetY);
            bullet.SetActive(true);
        }
    }
}
