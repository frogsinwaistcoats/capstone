using UnityEngine;

public class CampBorder : MonoBehaviour
{
    public static CampBorder instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        EnableSolidCollider();
    }

    public void EnableSolidCollider()
    {
        GetComponent<BoxCollider>().isTrigger = false;
    }

    public void EnableTriggerCollider()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.LoadSneaking();
        }
    }
}
