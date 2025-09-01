using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public static Fish instance;

    private SpriteRenderer sr;

    public Transform targetPos;

    public float duration;
    public float movesLeft = 5;
    public bool pullsBack = false;

    private void Awake()
    {
        instance = this;
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(movesLeft <= 0)
        {
            FishingManager.instance.CancelInvoke();
        }
    }

    public void MoveForward()
    {
        Vector3 start = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Vector3 end = new Vector3(targetPos.position.x + 7, targetPos.position.y, targetPos.position.z);

        transform.position = Vector3.Lerp(start, end, 1 / movesLeft);
        movesLeft--;
    }

    public void MoveBack()
    {
        Vector3 start = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Vector3 end = new Vector3(-targetPos.position.x + 7, targetPos.position.y, targetPos.position.z);

        transform.position = Vector3.Lerp(start, end, 1 / movesLeft);
        movesLeft++;
    }
}
