using UnityEngine;

public class CookingFood : MonoBehaviour
{
    
    public GameObject tile;
    public Transform[] raycastTransforms;
    //public Collider2D objCollider;
    public SpriteRenderer sr;

    public Vector2 pivotOffset;
    public Transform[] gridTargets;

    private Vector3 startPos;

    public LayerMask boardLayer;
    public LayerMask foodLayer;

    public bool isBeingHeld;
    public bool isOnBoard;

    private void Start()
    {
        startPos = transform.position;
        //objCollider = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (isBeingHeld)
        {
            MoveWithMouse();
            //outline.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                transform.Rotate(0f, 0f, 90f);
            }
        }
        else
        {
            //outline.SetActive(false);
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
        isBeingHeld = true;
        isOnBoard = false;
        sr.sortingOrder = 10; // Bring to front while dragging
        tile.GetComponent<SpriteRenderer>().sortingOrder = 9; // Ensure tile is just behind the food

    }

    private void OnMouseUp()
    {
        bool isBlocked = false;

        foreach (Transform raycastTransform in raycastTransforms)
        {
            // Check for overlap with food items
            Collider2D hitFood = Physics2D.OverlapPoint(raycastTransform.position, foodLayer);
            if (hitFood != null && hitFood.gameObject != gameObject)
            {
                isBlocked = true;
                Debug.Log(gameObject.name + " " + raycastTransform.name + " is over " + hitFood.name + ", cannot be placed");
                break;
            }

            // Check for overlap with board
            Collider2D hitBoard = Physics2D.OverlapPoint(raycastTransform.position, boardLayer);
            if (hitBoard != null)
            {
                //Debug.Log(raycastTransform.name + " is over Board");
            }
            else
            {
                isBlocked = true;
                Debug.Log(gameObject.name + " " + raycastTransform.name + " is not over Board, cannot be placed");
                break;
            }
        }

        // Stop holding the item
        isBeingHeld = false;
        sr.sortingOrder = 5; // Reset sorting order
        tile.GetComponent<SpriteRenderer>().sortingOrder = 4; // Reset tile sorting order

        // if blocked, return to start position, else snap to grid
        if (isBlocked)
        {
            transform.position = startPos;
        }
        else
        {
            isOnBoard = true;
            SnapToGrid();
        }
    }

    private void SnapToGrid()
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector2 pivotPosition = (Vector2)transform.position + pivotOffset;

        foreach (Transform potentialTarget in gridTargets)
        {
            if (potentialTarget == null) continue;

            Vector2 direction = (Vector2)potentialTarget.position - pivotPosition;
            float distanceSqr = direction.sqrMagnitude;

            if (distanceSqr < closestDistanceSqr)
            {
                closestDistanceSqr = distanceSqr;
                bestTarget = potentialTarget;
            }
        }

        if (bestTarget != null)
        {
            transform.position = (Vector2)bestTarget.position - pivotOffset;
        }

    }
}
