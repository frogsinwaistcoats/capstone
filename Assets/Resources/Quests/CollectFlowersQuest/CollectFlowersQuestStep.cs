using UnityEngine;

public class CollectFlowersQuestStep : QuestStep
{
    private int dandelionsCollected = 0;
    private int dandelionsToComplete = 1;
    private int poppiesCollected = 0;
    private int poppiesToComplete = 1;
    private int daisiesCollected = 0;
    private int daisiesToComplete = 1;

    private InventoryManager inventoryManager;

    private void OnEnable()
    {
        inventoryManager = GameObject.Find("MenuCanvas").GetComponent<InventoryManager>();

        dandelionsCollected = inventoryManager.GetItemCount("Dandelion");
        poppiesCollected = inventoryManager.GetItemCount("Poppy");
        daisiesCollected = inventoryManager.GetItemCount("Daisy");

        CheckForFinish();
    }

    public void ItemCollected(string itemName)
    {
        if(itemName == "Poppy")
        {
            poppiesCollected++;
        }

        if (itemName == "Dandelion")
        {
            dandelionsCollected++;
        }

        if (itemName == "Daisy")
        {
            daisiesCollected++;
        }

        CheckForFinish();
    }

    private void CheckForFinish()
    {
        if ((dandelionsCollected >= dandelionsToComplete) && (poppiesCollected >= poppiesToComplete) && (daisiesCollected >= daisiesToComplete))
        {
            FinishQuestStep();
        }
    }
}
