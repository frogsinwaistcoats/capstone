using UnityEngine;

public class PlayerMovement : MonoBehaviour//, IDataPersistence
{
    public static PlayerMovement instance;

    public float moveSpeed;
    public float horizontal;
    public float vertical;
    private Rigidbody rb;
    public bool canMove;

    public Transform startPlayerPos;
    public Transform campPlayerPos;
    public Transform forestPlayerPos;
    public Transform solitairePlayerPos;

    public SpriteRenderer sr;

    public Animator animator;

    public GameObject whaeaNyrie;
    public GameObject mrWilson;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        if (!LoadYarnVariables.instance.GetBool("$hasDoneIntro"))
        {
            canMove = false;
        }
        else
        {
            canMove = true;
        }
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
        animator.SetBool("isMoving", value);
    }

    public void ResetPos()
    {
        transform.position = startPlayerPos.position; 
    }

    public Vector3 ReturnCurrentTransform()
    {
        return transform.position;
    }

    public void GoToCampPos()
    {
        transform.position = campPlayerPos.position;
    }

    public void GoToForestPos()
    {
        transform.position = forestPlayerPos.position;
    }

    public void MoveTeachers()
    {
        whaeaNyrie.transform.position = new Vector3(-3.6f, 0.8f, -14.61f);
        mrWilson.transform.position = new Vector3(-2.37f, 0.77f, -14.68f);
    }
}
