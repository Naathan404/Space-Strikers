using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private PlayerController playerController;
    private bool isGamePaused = false;
    private bool isGameOver = false;
    private float score = 0;
    private float highScore;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;

        playerController = FindAnyObjectByType<PlayerController>();
        highScore = PlayerPrefs.GetFloat("HighScore", 0f);
    }

    [SerializeField] private float worldSpeed;
    [SerializeField] private float increaseWorldSpeedMark;
    [SerializeField] private float timer = 0f;

    private void Update()
    {
        if (isGameOver || playerController.isLevelUp) return;
        // Pause and unpause
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Tab))
            GameManager.instance.PauseGame(true);
        timer += Time.deltaTime;
        if (timer >= increaseWorldSpeedMark)
        {
            timer = 0f;
            worldSpeed *= 1.05f;
        }
        score += GetWorldSpeed() / 1000f;
        if (score > highScore)
        {
            PlayerPrefs.SetFloat("HighScore", score);
        }
        UIController.instance.DisplayScore(score);
    }

    public void SetGameOver()
    {
        isGameOver = true;
        StartCoroutine(GameOver());
    }

    IEnumerator GameOver()
    {
        float gtimer = 0f;
        while (gtimer < 1.5f)
        {
            gtimer += Time.deltaTime;
            Time.timeScale = Mathf.Lerp(1f, 0.5f, gtimer / 1.5f);
            yield return null;
        }
        Time.timeScale = 0f;
        UIController.instance.ActivateGameOverPanel(isGameOver);
        AudioManager.instance.StopMusic();
    }

    public void Restart()
    {
        SceneManager.LoadScene("LevelTest");
        AudioManager.instance.PlaySound(AudioManager.instance._buttonClick);
        Time.timeScale = 1f;
    }

    public void PauseGame(bool isPaused)
    {
        if (isPaused)
        {
            AudioManager.instance.PlayAdjustedSound(AudioManager.instance._uiPause);
            Time.timeScale = 0f;
            isGamePaused = true;
        }
        else
        {
            AudioManager.instance.PlayAdjustedSound(AudioManager.instance._uiUnpause);
            Time.timeScale = 1f;
            isGamePaused = false;
            playerController.DeactivateBoost();
        }
        UIController.instance.ActivatePausePanel(isPaused);
    }

    public float GetWorldSpeed() => worldSpeed * playerController.GetBoost();
    public bool IsPaused() => isGamePaused;
    public bool IsGameOVer() => isGameOver;
    public float GetScore() => score;
    public Vector3 GetPlayerPosition() => playerController.transform.position;
}