using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEditor.Progress;

public class CookingFood : MonoBehaviour
{
    
    public GameObject tile;
    public Transform[] raycastTransforms;
    //public Collider2D objCollider;
    public SpriteRenderer sr;

    public Vector2 pivotOffset;
    private Vector2 originalOffset;
    [SerializeField] private Quaternion originalRotation;

    public Transform[] gridTargets;
    [SerializeField] private List<CookingGrids> claimedGrids = new List<CookingGrids>();

    private Vector3 startPos;

    public LayerMask boardLayer;
    public LayerMask foodLayer;

    public bool isBeingHeld;
    public bool isOnBoard;

    private void Start()
    {
        startPos = transform.position;
        originalRotation = transform.rotation;
        //objCollider = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
        originalOffset = pivotOffset;
    }

    private void Update()
    {
        if (isBeingHeld)
        {
            MoveWithMouse();
            //outline.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                RotateFood();
            }
        }
        else
        {
            //outline.SetActive(false);
        }

    }

    private void RotateFood()
    {
        transform.Rotate(0f, 0f, 90f);

        pivotOffset = new Vector2(pivotOffset.y, -pivotOffset.x);
    }

    

    private void MoveWithMouse()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        transform.position = mouseWorldPosition;
    }




    private void OnMouseDown()
    {
        Debug.Log(name + " was clicked");

        isBeingHeld = true;
        isOnBoard = false;
        sr.sortingOrder = 10; // Bring to front while dragging
        tile.GetComponent<SpriteRenderer>().sortingOrder = 9; // Ensure tile is just behind the food

        //free up previously claimed grids
        foreach (CookingGrids grid in claimedGrids)
        {
            if (grid != null)
            {
                grid.isAvailable = true;
            }
        }

        claimedGrids.Clear();
    }

    private void OnMouseUp()
    {
        // Stop holding the item
        isBeingHeld = false;
        sr.sortingOrder = 5; // Reset sorting order
        tile.GetComponent<SpriteRenderer>().sortingOrder = 4; // Reset tile sorting order

        SnapToGrid();

        bool isBlocked = false;

        foreach (Transform raycastTransform in raycastTransforms)
        {
            Collider2D hit = Physics2D.OverlapBox(raycastTransform.position, new Vector2(1, 1), 0f, boardLayer);

            if (hit != null && hit.gameObject != gameObject)
            {
                CookingGrids grid = hit.GetComponent<CookingGrids>();

                if (grid != null && grid.isAvailable)
                {
                    grid.isAvailable = false;
                    claimedGrids.Add(grid);
                }
                else
                {
                    isBlocked = true;
                    break;
                }
            }
            else
            {
                isBlocked = true;
                break;
            }
        }

        // if blocked, return to start position, else snap to grid
        if (isBlocked)
        {
            ReturnToBoard();
        }
        else
        {
            isOnBoard = true;
        }
    }

    public void ReturnToBoard()
    {
        pivotOffset = originalOffset;
        transform.position = startPos;
        transform.rotation = originalRotation;

        foreach (CookingGrids grid in claimedGrids)
        {
            if (grid != null)
            {
                grid.isAvailable = true;
            }
        }
        claimedGrids.Clear();
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