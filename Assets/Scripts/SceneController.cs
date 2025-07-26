using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject sceneCanvas;
    [SerializeField] private GameObject loadingScene;
    [SerializeField] private Slider loadingBar;

    public void LoadScene(string sceneName)
    {
        AudioManager.instance.PlaySound(AudioManager.instance._buttonClick);
        sceneCanvas.SetActive(false);
        loadingScene.SetActive(true);
        StartCoroutine(LoadAsync(sceneName));
    }

    IEnumerator LoadAsync(string sceneName)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneName);
        while (!loadOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(loadOperation.progress / 0.99f);
            loadingBar.value = progressValue;
            Time.timeScale = 1f;
            yield return null;
        }
    }

    public void QuitGame()
    {
        AudioManager.instance.PlaySound(AudioManager.instance._buttonClick);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
