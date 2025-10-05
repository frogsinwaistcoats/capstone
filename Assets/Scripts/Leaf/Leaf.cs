using UnityEngine;

public class Leaf : MonoBehaviour
{
    public bool isBeingHeld;
    public bool canBeMoved = true;

    public GameObject outline;
    public LayerMask targetLayer;

    private Vector3 startPos;

    public LeafSlot leafSlot;

    private void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isBeingHeld && canBeMoved)
        {
            MoveWithMouse();
        }
    }

    private void OnMouseDown()
    {
        isBeingHeld = true;
    }

    private void MoveWithMouse()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        transform.position = mouseWorldPosition;
    }

    private void OnMouseUp()
    {
        // can also use: isBeingHeld = !isBeingHeld;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector3(0f, 0f, 10f), Mathf.Infinity, targetLayer);
        if (hit.collider != null && hit.collider.name == outline.name)
        {
            canBeMoved = false;
            isBeingHeld = false;
            transform.position = hit.collider.transform.position;

            outline.SetActive(false);
            leafSlot.isFilled = false;
            startPos = transform.position;
        }
        else
        {
            isBeingHeld = false;
            transform.position = startPos;
        }
    }
}
