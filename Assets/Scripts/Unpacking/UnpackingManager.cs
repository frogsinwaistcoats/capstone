using System.Collections;
using UnityEngine;
using Yarn;
using Yarn.Unity;

public class UnpackingManager : MonoBehaviour
{
    public static UnpackingManager instance;

    public GameObject[] items;
    public int nextItemIndex = 0;
    public Sprite openBag;
    public Sprite closeBag;

    public GameObject instructionScreen;
    public GameObject winScreen;

    public bool canStart;
    public bool finished;

    private void Awake()
    {
        instance = this;
    }

    private void OnMouseDown()
    {
        if (canStart && !finished)
        {
            StartCoroutine(OpenBag());

            if (nextItemIndex > items.Length - 1)
            {
                Finished();
            }
            else
            {
                items[nextItemIndex].gameObject.SetActive(true);
                items[nextItemIndex].GetComponent<UnpackingItem>().isBeingHeld = true;
                nextItemIndex++;
            }
        }
    }

    public void CheckForFinished()
    {
        Debug.Log("Checking for finished unpacking");

        int itemsPlaced = 0;
        foreach (GameObject item in items)
        {
            if (item.GetComponent<UnpackingItem>().isPlaced)
            {
                itemsPlaced++;
            }
        }

        if (itemsPlaced == items.Length)
        {
            finished = true;
            StartCoroutine(OpenWinScreen());
        }
    }

    public IEnumerator OpenWinScreen()
    {
        yield return new WaitForSeconds(0.5f);
        winScreen.SetActive(true);
    }

    public void Finished()
    {
        LoadYarnVariables.instance.SetYarnVariable("$hasUnpacked", true);
        GameManager.instance.LoadCampScene();
    }


    IEnumerator OpenBag()
    {
        GetComponent<SpriteRenderer>().sprite = openBag;
        yield return new WaitForSeconds(0.5f);
        GetComponent<SpriteRenderer>().sprite = closeBag;
    }

    public void LoadCampScene()
    {
        GameManager.instance.LoadCampScene();
    }

    public void StartUnpacking()
    {
        instructionScreen.SetActive(false);
        canStart = true;
    }
}
