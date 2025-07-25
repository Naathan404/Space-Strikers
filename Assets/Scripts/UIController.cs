using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("Energy Bar")]
    [SerializeField] private Slider energyBar;
    [SerializeField] private Image fillArea;
    [SerializeField] private TMP_Text energyText;
    [SerializeField] private Color energyUsableColor;
    [SerializeField] private Color energyUnusableColor;

    [Header("Health Bar")]
    [SerializeField] private Slider healthBar;
    [SerializeField] private TMP_Text healthText;

    [Header("Pause Panel")]
    [SerializeField] private GameObject pauseGamePanel;

    private PlayerController playerController;
    public static UIController instance;
    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;
    }

    private void Start()
    {
        playerController = FindAnyObjectByType<PlayerController>();
    }

    public void DisplayEnergyBar()
    {
        energyText.text = playerController.GetCurrentEnergy().ToString("00") + "/" + playerController.GetMaxEnergy().ToString("00");
        energyBar.value = playerController.GetCurrentEnergy() / playerController.GetMaxEnergy();

        if (playerController.GetCurrentEnergy() < playerController.GetEnergyRequiredForBoost())
            fillArea.color = energyUnusableColor;
        else
            fillArea.color = energyUsableColor;
    }

    public void DisplayHealthBar()
    {
        healthText.text = playerController.GetCurrentHealth().ToString("00") + "/" + playerController.GetMaxHealth().ToString("00");
        healthBar.value = (float)playerController.GetCurrentHealth() / playerController.GetMaxHealth();
    }

    public void ActivatePausePanel(bool active)
    {
        pauseGamePanel.SetActive(active);
    }
}
