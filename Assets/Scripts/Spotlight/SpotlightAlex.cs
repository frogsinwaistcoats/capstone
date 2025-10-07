using System.Collections;
using UnityEngine;

public class SpotlightAlex : MonoBehaviour
{
    public Transform[] hidingSpots;
    
    [SerializeField] private Transform previousSpot;
    [SerializeField] private Transform selectedSpot;

    public bool hasFound;

    private void Start()
    {
        StartCoroutine(changeHidingSpots());
    }

    public IEnumerator changeHidingSpots()
    {
        while (hasFound == false)
        {
            ChooseHidingSpot();
            transform.position = selectedSpot.position;

            yield return new WaitForSeconds(3f);
        }
    }

    private void ChooseHidingSpot()
    {
        previousSpot = selectedSpot;
        
        int selected = Random.Range(0, hidingSpots.Length - 1);
        selectedSpot = hidingSpots[selected];
        
        if (selectedSpot == previousSpot)
        {
            ChooseHidingSpot();
        }
    }

    private void OnMouseDown()
    {
        hasFound = true;
        Debug.Log(gameObject.name + " was found");
    }
}
