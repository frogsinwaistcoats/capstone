using UnityEngine;

public class UnpackingManager : MonoBehaviour
{
    public GameObject[] items;
    public int nextItemIndex = 0;

    private void OnMouseDown()
    {
        if(nextItemIndex > items.Length - 1)
        {
            Debug.Log("no more items");
        }
        else
        {
            Instantiate(items[nextItemIndex]);
            nextItemIndex++;
        }

        
    }
}
