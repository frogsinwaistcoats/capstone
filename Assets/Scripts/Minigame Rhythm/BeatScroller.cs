using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    public float beatTempo;

    public bool hasStarted;
    public GameObject arrow;

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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject newArrow = Instantiate(arrow, new Vector3(0f, -3.5f, 0f), Quaternion.identity);

            newArrow.transform.SetParent(transform);
        }
    }
}
