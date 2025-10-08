using System.Collections;
using UnityEngine;

public class AnimatorTest : MonoBehaviour
{
    public Sprite[] sprites;
    public SpriteRenderer spriteRenderer;
    public float fps;

    void Start()
    {
        StartCoroutine(KeyAnim());
    }

    IEnumerator KeyAnim()
    {
        spriteRenderer.sprite = sprites[0];
        yield return new WaitForSeconds(fps);
        spriteRenderer.sprite = sprites[1];
        yield return new WaitForSeconds(fps);
        spriteRenderer.sprite = sprites[2];
        yield return new WaitForSeconds(fps);
        spriteRenderer.sprite = sprites[3];
        yield return new WaitForSeconds(fps);
        spriteRenderer.sprite = sprites[4];
        yield return new WaitForSeconds(fps);
        spriteRenderer.sprite = sprites[5];
        yield return new WaitForSeconds(fps);
        spriteRenderer.sprite = sprites[6];
        yield return new WaitForSeconds(fps);
        spriteRenderer.sprite = sprites[7];
        yield return new WaitForSeconds(fps);
        spriteRenderer.sprite = sprites[8];
        yield return new WaitForSeconds(fps);
        spriteRenderer.sprite = sprites[9];
        yield return new WaitForSeconds(fps);
        spriteRenderer.sprite = sprites[10];
        yield return new WaitForSeconds(fps);
        spriteRenderer.sprite = sprites[11];
        yield return new WaitForSeconds(fps);

        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
