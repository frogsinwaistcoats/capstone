using UnityEngine;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public StatToChange statToChange = new StatToChange();
    public int amountToChangeStat;


    public void UseItem()
    {

        Debug.Log("Used item");
        if (statToChange == StatToChange.health)
        {
            //apply changes here
            
        }

        if (statToChange == StatToChange.speed)
        {
            //apply changes here
        }
    }

    public enum StatToChange
    {
        none, 
        health, 
        speed
    };
}
