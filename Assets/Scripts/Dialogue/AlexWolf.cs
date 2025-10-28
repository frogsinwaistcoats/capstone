using UnityEngine;

public class AlexWolf : MonoBehaviour
{
    public Sprite wolf;

    // Update is called once per frame
    void Update()
    {
        if (DayManager.instance.dayCount == 5)
        {
            GetComponent<Animator>().enabled = false;
            transform.localScale = transform.localScale / 2;
            GetComponent<SpriteRenderer>().sprite = wolf;
        }
    }
}
