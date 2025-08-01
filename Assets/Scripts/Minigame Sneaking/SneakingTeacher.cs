using System.Collections;
using UnityEngine;

public class SneakingTeacher : MonoBehaviour
{
    public float timer = 3;
    public bool isLooking = false;

    private void Start()
    {
        InvokeRepeating("Flip", timer, timer);
    }

    void Flip()
    {
        transform.Rotate(new Vector3(0, 180f));
        if(isLooking == true)
        {
            isLooking = false;
        }
        else if (isLooking == false)
        {
            isLooking = true;
        }
    }
}
