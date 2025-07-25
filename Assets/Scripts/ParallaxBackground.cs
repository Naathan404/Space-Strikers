using System.Linq.Expressions;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private float scrollSpeed;
    private float backgroundWidth;
    private PlayerController playerController;

    private void Start()
    {
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        backgroundWidth = sprite.texture.width / sprite.pixelsPerUnit;

        playerController = FindAnyObjectByType<PlayerController>();
    }

    private void Update()
    {
        if(playerController)
            transform.position += new Vector3(scrollSpeed, 0f) * playerController.GetBoost() * Time.deltaTime;
        else 
            transform.position += new Vector3(scrollSpeed, 0f) * Time.deltaTime;
        if (Mathf.Abs(transform.position.x) - backgroundWidth > 0f)
            transform.position = Vector3.zero;
    }
}
