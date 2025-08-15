using UnityEngine;

public class RhythmButtonController : MonoBehaviour
{
    private SpriteRenderer sr;
    public Sprite defaultImage;
    public Sprite pressedImage;

    public KeyCode keyToPress;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            sr.sprite = pressedImage;
        }

        if (Input.GetKeyUp(keyToPress))
        {
            sr.sprite = defaultImage;
        }
    }
}
