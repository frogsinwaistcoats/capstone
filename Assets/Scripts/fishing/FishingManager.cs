using UnityEngine;

public class FishingManager : MonoBehaviour
{
    public GameObject button;
    public Canvas canvas;

    private void Start()
    {
        Instantiate(button, canvas.transform);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(button, canvas.transform);
        }
    }
}
