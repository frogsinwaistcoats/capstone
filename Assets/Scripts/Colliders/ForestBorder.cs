using UnityEngine;

public class ForestBorder : MonoBehaviour
{
    public static ForestBorder instance;

    private void Awake()
    {
        instance = this;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(GameManager.instance.ReturnToCampFromForest());
        }
    }
}
