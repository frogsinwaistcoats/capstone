using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpriteShaker : MonoBehaviour
{
    public static SpriteShaker instance;

    public float shakeDistance = 10f;        // pixels left/right
    public float shakeSpeed = 200f;          // pixels per second
    public int shakeCount = 3;               // left+right pairs per burst
    public float pauseBetweenBursts = 1f;    // seconds
    public bool loop = true;                 // keep repeating bursts

    RectTransform rectTransform;
    Vector2 originalPos;
    bool running;

    void Awake()
    {
        instance = this;
        rectTransform = GetComponent<RectTransform>();
        // Optional: if this object is in a LayoutGroup, ignore layout so it won't snap back
        var le = GetComponent<LayoutElement>();
        if (le != null) le.ignoreLayout = true;
    }

    void OnEnable()
    {
        // Capture once enabled (safer than Awake if parents/layouts change on enable)
        originalPos = rectTransform.anchoredPosition;
    }

    public void ShakeMe()
    {
        if (running) return;
        StartCoroutine(ShakeRoutine());
    }

    public void StopShaking()
    {
        loop = false;
        running = false;
        StopAllCoroutines();
        rectTransform.anchoredPosition = originalPos;
    }

    IEnumerator ShakeRoutine()
    {
        running = true;

        do
        {
            // Do N left-right pairs
            for (int i = 0; i < shakeCount; i++)
            {
                yield return MoveToX(originalPos.x + shakeDistance); // right
                yield return MoveToX(originalPos.x - shakeDistance); // left
            }

            // Ease back to center on X
            yield return MoveToX(originalPos.x);

            if (pauseBetweenBursts > 0f)
                yield return new WaitForSeconds(pauseBetweenBursts);

        } while (loop);

        running = false;
    }

    IEnumerator MoveToX(float targetX)
    {
        // Lock Y to original, only move X
        float y = originalPos.y;
        while (Mathf.Abs(rectTransform.anchoredPosition.x - targetX) > 0.1f)
        {
            float newX = Mathf.MoveTowards(rectTransform.anchoredPosition.x, targetX, shakeSpeed * Time.deltaTime);
            rectTransform.anchoredPosition = new Vector2(newX, y);
            yield return null;
        }
        rectTransform.anchoredPosition = new Vector2(targetX, y);
    }
}