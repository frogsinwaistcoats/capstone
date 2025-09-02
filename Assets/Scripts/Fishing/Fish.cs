using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public static Fish instance;

    private SpriteRenderer sr;

    public Transform finalTargetPos;

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
            FishingManager.instance.EndGame();
        }
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
        // this is wrong, fix later
        Vector3 start = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Vector3 end = new Vector3(-finalTargetPos.position.x, finalTargetPos.position.y, finalTargetPos.position.z);

        Vector3 targetPos = Vector3.Lerp(start, end, 1 / movesLeft);

        StartCoroutine(MoveFish(start, targetPos));
        movesLeft++;
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
