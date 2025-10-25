using UnityEngine;
using UnityEngine.UI;

public class SpotlightController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (SpotlightAlex.instance.canStart)
        {
            MoveWithMouse();
            Cursor.visible = false;
        }
        else
        {
            Cursor.visible = true;
        }
        
    }

    private void MoveWithMouse()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        transform.position = mouseWorldPosition;
    }
}
