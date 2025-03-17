using UnityEngine;

public class Interactor : MonoBehaviour
{
    public Transform interactorSource;
    public float interactRange;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray r = new Ray(interactorSource.position, interactorSource.forward);

        }
    }
}
