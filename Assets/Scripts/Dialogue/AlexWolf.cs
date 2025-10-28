using UnityEngine;

public class AlexWolf : MonoBehaviour
{
    public Sprite wolf;
    public bool wolfAnim = false;

    // Update is called once per frame
    void Update()
    {
        if (DayManager.instance.dayCount == 5)
        {
            if (!wolfAnim)
            {
                wolfAnim = true;
                GetComponent<Animator>().Play("Wolf_Idle");
            }
            
        }
    }
}
