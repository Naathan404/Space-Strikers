using UnityEngine;
using UnityEngine.InputSystem.iOS;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private PlayerController playerController;
    private bool isGamePaused = false;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;

        playerController = FindAnyObjectByType<PlayerController>();
    }

    [SerializeField] private float worldSpeed;

    private void Update()
    {
        // Pause and unpause
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Tab))
            GameManager.instance.PauseGame(true);
    }

    public void PauseGame(bool isPaused)
    {
        if (isPaused)
        {
            AudioManager.instance.PlaySound(AudioManager.instance._uiPause);
            Time.timeScale = 0f;
            isGamePaused = true;
        }
        else
        {
            AudioManager.instance.PlaySound(AudioManager.instance._uiUnpause);
            Time.timeScale = 1f;
            isGamePaused = false;
            playerController.DeactivateBoost();
        }
        UIController.instance.ActivatePausePanel(isPaused);
    }

    public float GetWorldSpeed() => worldSpeed * playerController.GetBoost();
    public bool IsPaused() => isGamePaused;
    public Vector3 GetPlayerPosition() => playerController.transform.position;
    
}