using UnityEngine;
using UnityEngine.InputSystem;

public class UnpackingItem : MonoBehaviour
{
    public bool isBeingHeld;

    private void Start()
    {
        isBeingHeld = true;
    }

    private void Update()
    {
        if (isBeingHeld)
        {
            MoveWithMouse();
        }
    }

    private void MoveWithMouse()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        transform.position = mouseWorldPosition;
    }

    private void OnMouseDown()
    {
        // can also use: isBeingHeld = !isBeingHeld;

        if (isBeingHeld == true)
        {
            isBeingHeld = false;
        }
        else
        {
            isBeingHeld = true;
        }
    }
}
