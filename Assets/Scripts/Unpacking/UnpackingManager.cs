using System.Collections;
using UnityEngine;

public class UnpackingManager : MonoBehaviour
{
    public GameObject[] items;
    public int nextItemIndex = 0;
    public Sprite openBag;
    public Sprite closeBag;

    private void OnMouseDown()
    {
        StartCoroutine(OpenBag());

        if(nextItemIndex > items.Length - 1)
        {
            Debug.Log("no more items");
        }
        else
        {
            items[nextItemIndex].gameObject.SetActive(true);
            items[nextItemIndex].GetComponent<UnpackingItem>().isBeingHeld = true;
            nextItemIndex++;
        }
    }

    IEnumerator OpenBag()
    {
        GetComponent<SpriteRenderer>().sprite = openBag;
        yield return new WaitForSeconds(0.5f);
        GetComponent<SpriteRenderer>().sprite = closeBag;
    }
}
