using UnityEngine;

public class CampTriggers : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement.instance.ResetPos();
            MainDialogueManager.instance.OutOfBoundsDialogue();
        }
    }
}
