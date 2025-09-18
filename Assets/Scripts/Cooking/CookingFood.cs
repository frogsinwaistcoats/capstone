using UnityEngine;

public class CookingFood : MonoBehaviour
{
    public bool isBeingHeld;
    public GameObject tile;
    public LayerMask boardLayer;
    public LayerMask foodLayer;
    public Transform[] raycastTransforms;

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
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector3(0f, 0f, 10f), Mathf.Infinity, foodLayer);
        if (hit.collider != null)
        {
            Debug.Log(gameObject.name + " is over " + hit.collider.gameObject.name);
        }
    }


    private void OnMouseDown()
    {       
        isBeingHeld = true;
        //tile.SetActive(true);    
    }

    private void OnMouseUp()
    {
        bool isBlocked = false;

        //checking for food underneath
        foreach (Transform raycastTransform in raycastTransforms)
        {
            RaycastHit2D hit = Physics2D.Raycast(raycastTransform.position, new Vector3(0f, 0f, 10f), Mathf.Infinity, foodLayer);
            if (hit.collider != null)
            {
                isBlocked = true;
                break;
            }
        }

        if (isBlocked)
        {
            Debug.Log(gameObject.name + " is blocked by other food");
            isBeingHeld = true;
            //transform.position = hits[0].collider.transform.position;
            //tile.SetActive(false);
        }
        if (!isBlocked)
        {
            isBeingHeld = false;
        }


        bool isOnBoard = false;
        //checking for board underneath

        foreach (Transform raycastTransform in raycastTransforms)
        {
            RaycastHit2D hit = Physics2D.Raycast(raycastTransform.position, new Vector3(0f, 0f, 10f), Mathf.Infinity, boardLayer);
            if (hit.collider == null)
            {
                isOnBoard = false;
                break;
            }
            isOnBoard = true;

        }

        if (isOnBoard)
        {
            isBeingHeld = false;
            //transform.position = hits[0].collider.transform.position;
            //tile.SetActive(false);
        }
        else
        {
            Debug.Log(gameObject.name + " is not over Board");
            isBeingHeld = true;
        }

    }
}
