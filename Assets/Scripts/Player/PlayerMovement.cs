using UnityEngine;

public class PlayerMovement : MonoBehaviour//, IDataPersistence
{
    public static PlayerMovement current;

    public float moveSpeed;
    public float horizontal;
    public float vertical;
    private Rigidbody rb;
    bool canMove;
    public Transform startingPos;
    public SpriteRenderer sr;

    public Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        current = this;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        canMove = true;
    }
    /*
    public void LoadData(GameData data)
    {
        this.transform.position = data.playerPosition;
    }

    public void SaveData(GameData data)
    {
        data.playerPosition = this.transform.position;
    }
    */

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

            //changes anim state
            animator.SetBool("isMoving", moveDirection != Vector3.zero);

            //flips sprite direction
            if (horizontal > 0.1f)
            {
                sr.flipX = false;
            }
            else if (horizontal < -0.1f)
            {
                sr.flipX = true;
            }


                rb.MovePosition(moveDirection + transform.position);
        }
    }

    public void SetMovement(bool value)
    {
        canMove = value;
    }

    public void ResetPos()
    {
        this.transform.position = startingPos.position;
    }
}
