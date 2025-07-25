using System.Collections;
using System.Xml.Serialization;
using JetBrains.Annotations;
using UnityEngine;

public class FlashEffect : MonoBehaviour
{
    private Color flashColor = new Color(5f, 5f, 5f);

    public static FlashEffect instance;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;
    }

    public void CallFlashEffect(SpriteRenderer spriteRenderer)
    {
        spriteRenderer.color = flashColor;
        StartCoroutine(ExitFlashEffect(spriteRenderer));
    }

    IEnumerator ExitFlashEffect(SpriteRenderer spriteRenderer)
    {
        yield return new WaitForSecondsRealtime(0.05f);
        spriteRenderer.color = Color.white;
    }
}
