using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float horizontal;
    public float vertical;
    private Rigidbody rb;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        Vector3 forwardDirection = vertical * transform.forward;
        Vector3 sidewaysDirection = horizontal * transform.right;

        Vector3 moveDirection = forwardDirection + sidewaysDirection;
        moveDirection *= Time.deltaTime * moveSpeed;

        rb.MovePosition(moveDirection + transform.position);
    }
}
