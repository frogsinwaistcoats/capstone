using UnityEngine;

public class FallCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement.instance.ResetPos();
            MainDialogueManager.instance.FallDialogue();
        }
    }
}
