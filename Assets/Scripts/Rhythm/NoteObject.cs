using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;

    public KeyCode keyToPress;
    public bool noteHit;
    
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            if (canBePressed)
            {
                RhythmManager.instance.NoteHit();
                noteHit = true;
                gameObject.SetActive(false);
                //gameObject.GetComponent<SpriteRenderer>().color = Color.black;

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Activator")
        {
            canBePressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Activator" && gameObject.activeSelf)
        {
            canBePressed = false;
            if (!noteHit)
            {
                RhythmManager.instance.NoteMissed();
            }
        }
    }
}
