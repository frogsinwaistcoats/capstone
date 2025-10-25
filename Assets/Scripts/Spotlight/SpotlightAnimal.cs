using System.Collections;
using UnityEngine;

public class SpotlightAnimal : MonoBehaviour
{
    public Transform[] hidingSpots;

    [SerializeField] private Transform previousSpot;
    [SerializeField] private Transform selectedSpot;

    public float fadeDuration;
    private SpriteRenderer spriteRenderer;

    public float waitTime;
    public float moveOffset;

    public void StartSpotlightAnimals()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        StartCoroutine(FadeMoveLoop());
    }

    private IEnumerator FadeMoveLoop()
    {
        yield return new WaitForSeconds(moveOffset);
        while (!SpotlightAlex.instance.hasFound)
        {
            yield return StartCoroutine(FadeOut());

            ChooseHidingSpot();
            transform.position = selectedSpot.position;

            yield return StartCoroutine(FadeIn());

            yield return new WaitForSeconds(waitTime);
        }
    }

    private void ChooseHidingSpot()
    {
        previousSpot = selectedSpot;

        int selected;
        do
        {
            selected = Random.Range(0, hidingSpots.Length);
        }
        while (hidingSpots[selected] == previousSpot);

        selectedSpot = hidingSpots[selected];
    }

    private IEnumerator FadeOut()
    {
        float elapsed = 0f;
        Color color = spriteRenderer.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(color.a, 0f, elapsed / fadeDuration);
            spriteRenderer.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        spriteRenderer.color = new Color(color.r, color.g, color.b, 0f);
    }

    private IEnumerator FadeIn()
    {
        float elapsed = 0f;
        Color color = spriteRenderer.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(color.a, 1f, elapsed / fadeDuration);
            spriteRenderer.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        spriteRenderer.color = new Color(color.r, color.g, color.b, 1f);

    }
}
