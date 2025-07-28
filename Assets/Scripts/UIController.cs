using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject playingPanel;
    [Header("Score Variables")]
    [SerializeField] private TMP_Text scoreText;
    [Header("Energy Bar")]
    [SerializeField] private Slider energyBar;
    [SerializeField] private Image energyFillArea;
    [SerializeField] private TMP_Text energyText;
    [SerializeField] private Color energyUsableColor;
    [SerializeField] private Color energyUnusableColor;

    [Header("Health Bar")]
    [SerializeField] private Slider healthBar;
    [SerializeField] private TMP_Text healthText;
    [Header("Exp Bar")]
    [SerializeField] private Slider expBar;
    [SerializeField] private TMP_Text expText;
    [SerializeField] private Image expFillArea;
    [SerializeField] private Color lowExpColor;
    [SerializeField] private Color medExpColor;
    [SerializeField] private Color highExpColor;
    [SerializeField] private Color veryHighExpColor;

    [Header("Pause Panel")]
    [SerializeField] private GameObject pauseGamePanel;
    [Header("Game Over Panel")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text yourScore;
    [SerializeField] private TMP_Text highScore;
    [Header("LevelUpPanel")]
    [SerializeField] private GameObject levelUpPanel;
    [Header("Tutorial Panel ")]
    [SerializeField] private GameObject tutoPanel;

    private PlayerController playerController;
    public static UIController instance;
    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;
        if (PlayerPrefs.GetInt("FirstTimeToPlay", 0) == 0)
        {
            PlayerPrefs.SetInt("FirstTimeToPlay", 1);
            tutoPanel.SetActive(true);
            StartCoroutine(DeactivateTutoPanel());
        }
        else
        {
            tutoPanel.SetActive(false);
        }
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
            energyFillArea.color = energyUnusableColor;
        else
            energyFillArea.color = energyUsableColor;
    }

    public void DisplayHealthBar()
    {
        healthText.text = playerController.GetCurrentHealth().ToString("00") + "/" + playerController.GetMaxHealth().ToString("00");
        healthBar.value = (float)playerController.GetCurrentHealth() / playerController.GetMaxHealth();
    }

    public void DisplayEXPBar()
    {
        float curExpVal = (float)playerController.GetCurrentExp() / playerController.GetMaxExp();
        expText.text = "Level " + playerController.GetCurrentLevel().ToString("00");
        if (0f <= curExpVal && curExpVal < 0.25f)
            expFillArea.color = lowExpColor;
        if (0.25f <= curExpVal && curExpVal < 0.5f)
            expFillArea.color = medExpColor;
        if (0.5f <= curExpVal && curExpVal < 0.75f)
            expFillArea.color = highExpColor;
        if (0.75f <= curExpVal && curExpVal <= 1f)
            expFillArea.color = veryHighExpColor;
        expBar.value = curExpVal;
    }

    public void DisplayScore(float score)
    {
        scoreText.text = GameManager.instance.GetScore().ToString("0.0");
    }

    public void ActivatePausePanel(bool active)
    {
        pauseGamePanel.SetActive(active);
    }

    public void ActivateGameOverPanel(bool active)
    {
        gameOverPanel.SetActive(active);
        yourScore.text = "You've traveled: " + GameManager.instance.GetScore().ToString("0.0") + " lys";
        highScore.text = "Best: " + PlayerPrefs.GetFloat("HighScore", 0f).ToString("0.0");
        playingPanel.SetActive(!active);
    }

    public void ActivatePowerUpPanel()
    {
        levelUpPanel.SetActive(true);
        Time.timeScale = 0f;
    }
    public void DeactivatePowerUpPanel()
    {
        levelUpPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    IEnumerator DeactivateTutoPanel()
    {
        yield return new WaitForSeconds(2.5f);
        tutoPanel.SetActive(false);
    }
}
