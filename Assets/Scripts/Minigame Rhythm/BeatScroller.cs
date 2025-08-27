using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    public float beatTempo;

    public bool hasStarted;
    public GameObject leftArrow;
    public GameObject rightArrow;
    public GameObject upArrow;
    public GameObject downArrow;

    void Start()
    {
        beatTempo = beatTempo / 60f;
    }

    void Update()
    {
        if (!hasStarted)
        {
            
        }
        else
        {
            transform.position -= new Vector3(0f, beatTempo * Time.deltaTime, 0f);
        }

        /*
        if (Input.GetKeyDown(KeyCode.A))
        {
            GameObject newArrow = Instantiate(leftArrow, new Vector3(0f, -3f, 0f), Quaternion.identity);

            newArrow.transform.SetParent(transform);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            GameObject newArrow = Instantiate(rightArrow, new Vector3(0f, -3f, 0f), Quaternion.identity);

            newArrow.transform.SetParent(transform);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            GameObject newArrow = Instantiate(upArrow, new Vector3(0f, -3f, 0f), Quaternion.identity);

            newArrow.transform.SetParent(transform);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            GameObject newArrow = Instantiate(downArrow, new Vector3(0f, -3f, 0f), Quaternion.identity);

            newArrow.transform.SetParent(transform);
        }
        */
    }
}
