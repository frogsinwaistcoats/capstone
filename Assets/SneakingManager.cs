using System;
using System.Collections;
using JetBrains.Annotations;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;

public class SneakingManager : MonoBehaviour
{
    public GameObject[] bushes;
    public Vector3 bushOffset;
    public int speed = 3;
    public bool canMove = false;
    [SerializeField] private Vector3 currentPos;
    public Vector3 targetPos;
    public bool isMoving = false;
    private Vector3 startingPos;

    public SneakingTeacher sneakingTeacher;

    private void Awake()
    {
        targetPos = transform.position;
        startingPos = transform.position;
    }

    void Update()
    {
        currentPos = transform.position;
        DetectObjectWithRaycast();

        if (canMove == true)
        {
            Vector3 directionToMove = targetPos - transform.position;
            directionToMove = directionToMove.normalized * Time.deltaTime * speed;
            float maxDistance = Vector3.Distance(transform.position, targetPos);
            transform.position = transform.position + Vector3.ClampMagnitude(directionToMove, maxDistance);
        }

        if (currentPos == targetPos)
        {
            isMoving = false;
        }
        else if (currentPos != targetPos)
        {
            isMoving = true;
        }

        if(isMoving && sneakingTeacher.isLooking)
        {
            Debug.Log("FAIL");
            transform.position = startingPos;
            targetPos = transform.position;
        }
    }

    public void DetectObjectWithRaycast()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log($"{hit.collider.name} Detected", hit.collider.gameObject);
                targetPos = hit.collider.gameObject.transform.position - bushOffset;
                canMove = true;
            }
        }
    }

    //private IEnumerator Move()
    //{
        //while(transform.position)
    //}
}
