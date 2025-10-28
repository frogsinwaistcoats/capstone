using UnityEngine;

public class CampBorder : MonoBehaviour
{
    public static CampBorder instance;
    public bool canEnter = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        NotEnter();
    }

    public void CanEnter()
    {
        canEnter = true;
    }

    public void NotEnter()
    {
        canEnter = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!canEnter)
            {
                MainDialogueManager.instance.CantSneakYet();
            }
            else if (canEnter)
            {

                GameManager.instance.LoadSneaking();

            }
            
        }
    }
}
