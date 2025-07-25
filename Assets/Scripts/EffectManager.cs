using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager instance { get; private set; }

    [SerializeField] private ObjectPooler explosion01EffectPooler;
    [SerializeField] private ObjectPooler explosion02EffectPooler;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;
    }

    public void RunExplosion01Effect(Vector3 position)
    {
        GameObject effect = explosion01EffectPooler.GetPooledObject();
        SetDefaultState(effect, position);
    }

    public void RunExplosion02Effect(Vector3 position)
    {
        GameObject effect = explosion02EffectPooler.GetPooledObject();
        SetDefaultState(effect, position);
    }

    private void SetDefaultState(GameObject effect, Vector3 position)
    {
        effect.transform.position = position;
        if (effect.GetComponent<BaseEffect>() == null)
            effect.gameObject.AddComponent<BaseEffect>();
        effect.SetActive(true);
    }
}
