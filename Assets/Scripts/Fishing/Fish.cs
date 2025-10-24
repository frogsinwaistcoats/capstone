using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public static Fish instance;

    private SpriteRenderer sr;

    public Vector3 startPos;
    public Transform finalTargetPos;

    public float duration;
    public float movesLeft;
    public bool pullsBack = false;

    private void Awake()
    {
        instance = this;
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        if(movesLeft <= 0)
        {
            movesLeft = 6;
            StartCoroutine(FishingManager.instance.WinGame());
        }
        if (movesLeft >= 7)
        {
            movesLeft = 6;
            
            StartCoroutine(FishingManager.instance.FailGame());
        }
    }

    public void ResetBobber()
    {
        transform.position = startPos;
    }

    public void MoveForward()
    {
        Vector3 start = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Vector3 end = new Vector3(finalTargetPos.position.x, finalTargetPos.position.y, finalTargetPos.position.z);

        Vector3 targetPos = Vector3.Lerp(start, end, 1 / movesLeft);

        StartCoroutine(MoveFish(start, targetPos));
        movesLeft--;
    }

    public void MoveBack()
    {
        movesLeft++;
        Vector3 start = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        float startx = transform.position.x;
        float endx = finalTargetPos.position.x;
        float xdistance = Mathf.Abs(startx - endx);
        Vector3 end = new Vector3(startx + xdistance, transform.position.y, transform.position.z);

        Vector3 targetPos = Vector3.Lerp(start, end, 1 / movesLeft);

        StartCoroutine(MoveFish(start, targetPos));
        
    }

    //makes fish move smoothly
    IEnumerator MoveFish(Vector3 start, Vector3 targetPos)
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            transform.position = Vector3.Lerp(start, targetPos, t);
            yield return null;
        }
        transform.position = targetPos;
    }
}
