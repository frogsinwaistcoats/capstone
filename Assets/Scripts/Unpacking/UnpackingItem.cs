using UnityEngine;
using UnityEngine.InputSystem;

public class UnpackingItem : MonoBehaviour
{
    public bool isBeingHeld;
    public GameObject outline;
    public LayerMask targetLayer;
    public Sprite placedSprite;

    private void Update()
    {
        if (isBeingHeld)
        {
            MoveWithMouse();
            outline.SetActive(true);
        }
        else
        {
            outline.SetActive(false);
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

        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector3(0f, 0f, 10f), Mathf.Infinity, targetLayer);
        if (hit.collider != null)
        {
            Debug.Log(gameObject.name + " is over " + hit.collider.gameObject.name);

            isBeingHeld = false;
            transform.position = hit.collider.transform.position;

            if(placedSprite != null)
            {
                GetComponent<SpriteRenderer>().sprite = placedSprite;
            }
        }
    }
}
