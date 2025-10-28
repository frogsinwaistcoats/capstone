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

    private float totalMovesNeeded = 6f;
    private float progress = 0f;

    private void Awake()
    {
        instance = this;
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        startPos = transform.position;
    }

    public void ResetBobber()
    {
        transform.position = startPos;
        progress = 0f;
    }

    public void MoveForward()
    {
        progress += 1f / totalMovesNeeded;
        progress = Mathf.Clamp01(progress);
        StartCoroutine(MoveFish(transform.position, Vector3.Lerp(startPos, finalTargetPos.position, progress)));
    }

    public void MoveBack()
    {
        progress -= 1f / totalMovesNeeded;
        progress = Mathf.Clamp01(progress);
        StartCoroutine(MoveFish(transform.position, Vector3.Lerp(startPos, finalTargetPos.position, progress)));
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

        if (progress >= 1f)
        {
            movesLeft = 6;
            StartCoroutine(FishingManager.instance.WinGame());
        }
        if (progress <= 0f)
        {
            movesLeft = 6;
            StartCoroutine(FishingManager.instance.FailGame());
        }
    }
}
