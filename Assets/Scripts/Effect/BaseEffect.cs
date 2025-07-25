using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class BaseEffect : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        StartCoroutine(ReturnPool(animator.GetCurrentAnimatorStateInfo(0).length - 0.01f));
    }

    IEnumerator ReturnPool(float duration)
    {
        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
    }
}
