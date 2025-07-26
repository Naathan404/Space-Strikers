using NUnit.Framework;
using UnityEngine;

public class Blueprint : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    public void Clicked()
    {
        Debug.Log("choose blueprint successfully");
        HasDone();
    }

    public void RestoreFullHealth()
    {
        playerController.SetCurrentHealth(playerController.GetMaxHealth());
        HasDone();
    }

    public void AddBullets(int amount)
    {
        BasicGun.instance.SetAmountOfBullets(BasicGun.instance.GetAmountOfBullets() + amount);
        HasDone();
    }

    public void IncreaseBulletSize()
    {
        BasicGun.instance.SetBulletSize(BasicGun.instance.GetBulletSize() * 1.5f);
        HasDone();
    }

    public void AttackFaster()
    {
        BasicGun.instance.SetCoolDown(BasicGun.instance.GetCoolDown() * 0.8f);
        HasDone();
    }

    public void IncreaseMaxEnergy(int amount)
    {
        playerController.SetMaxEnergy(playerController.GetMaxEnergy() + amount);
        HasDone();
    }

    public void IncreaseEnergyRegeneration()
    {
        playerController.SetEnergyRegeneration(playerController.GetEnergyRegeneration() * 1.5f);
        HasDone();
    }

    private void HasDone()
    {
        UIController.instance.DeactivatePowerUpPanel();
    }
}
