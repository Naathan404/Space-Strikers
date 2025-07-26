using System.Collections.Generic;
using UnityEngine;

public class LevelUpPanel : MonoBehaviour
{
    [SerializeField] private List<GameObject> blueprints;

    private void Awake() 
    {
        foreach(var obj in blueprints)
        {
            obj.SetActive(false);
        }
    }

    private void OnEnable()
    {
        int i = 0;
        while (i < 3)
        {
            int rand = Random.Range(0, blueprints.Count);
            if (!blueprints[rand].activeInHierarchy)
            {
                blueprints[rand].SetActive(true);
                i++;
            }
        }
    }

    private void OnDisable()
    {
        foreach (var obj in blueprints)
        {
            obj.SetActive(false);
        }
    }
}
