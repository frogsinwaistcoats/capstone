using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SneakingPlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform movePoint;

    public LayerMask whatStopsMovement;

    public bool playerTurn = true;

    public GameObject exitPos;

    void Start()
    {
        movePoint.parent = null;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if(Vector3.Distance(transform.position, movePoint.position) <= .05f)
        {
            if (playerTurn)
            {
                if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
                {
                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, whatStopsMovement))
                    {
                        movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal") * 2, 0f, 0f);
                        StartCoroutine(CallTeacher());
                    }

                }
                else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
                {
                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, whatStopsMovement))
                    {
                        movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical") * 2, 0f);
                        StartCoroutine(CallTeacher());
                    }

                }
            }
        }

        if (transform.position == exitPos.transform.position)
        {
            Debug.Log("Yay you win");
            SneakingManager.instance.Succeed();
        }
    }

    IEnumerator CallTeacher()
    {
        playerTurn = false;
        yield return new WaitForSeconds(0.7f);
        SneakingTeacherController.instance.TeacherMove();

    }
}
