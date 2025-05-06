using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement current;

    public float moveSpeed;
    public float horizontal;
    public float vertical;
    private Rigidbody rb;
    bool canMove;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        current = this;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            Vector3 forwardDirection = vertical * transform.forward;
            Vector3 sidewaysDirection = horizontal * transform.right;

            Vector3 moveDirection = forwardDirection + sidewaysDirection;
            moveDirection *= Time.deltaTime * moveSpeed;

            rb.MovePosition(moveDirection + transform.position);
        }
    }

    public void SetMovement(bool value)
    {
        canMove = value;
    }

    #region Save and Load

    public void Save(ref PlayerSaveData data)
    {
        data.position = transform.position;
    }

    public void Load(PlayerSaveData data)
    {
        transform.position = data.position;
    }

    #endregion
}

public struct PlayerSaveData
{
    public Vector3 position;
}
