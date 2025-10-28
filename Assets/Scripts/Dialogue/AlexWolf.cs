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
            transform.localScale = new Vector3(0.14f, 0.14f, 1f);
            GetComponent<SpriteRenderer>().sprite = wolf;
        }
    }
}
