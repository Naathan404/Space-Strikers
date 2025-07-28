using TMPro;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text highScoreText;

    private void Awake()
    {
        highScoreText.text = "Best: " + PlayerPrefs.GetFloat("HighScore", 0f).ToString("0.0");
    }
}
