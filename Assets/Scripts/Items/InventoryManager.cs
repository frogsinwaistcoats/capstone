using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public GameObject inventory;
    public GameObject itemsMenu;
    public GameObject questsMenu;
    public GameObject settingsMenu;
    public bool menuActivated;
    public ItemSlot[] itemSlot;

    public ItemSO[] itemSOs;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && menuActivated)
        {
            Time.timeScale = 1;
            inventory.SetActive(false);
            menuActivated = false;

            FindAnyObjectByType<AudioManager>().Play("OpenBook");
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && !menuActivated)
        {
            Time.timeScale = 0;
            inventory.SetActive(true);
            menuActivated = true;

            FindAnyObjectByType<AudioManager>().Play("OpenBook");
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && menuActivated)
        {
            Time.timeScale = 1;
            inventory.SetActive(false);
            menuActivated = false;

            FindAnyObjectByType<AudioManager>().Play("OpenBook");
        }
    }

    public void OpenItems()
    {
        itemsMenu.SetActive(true);

        settingsMenu.SetActive(false);
        questsMenu.SetActive(false);
    }

    public void OpenSettings()
    {
        settingsMenu.SetActive(true);

        questsMenu.SetActive(false);
        itemsMenu.SetActive(false);
    }

    public void OpenQuests()
    {
        questsMenu.SetActive(true);

        settingsMenu.SetActive(false);
        itemsMenu.SetActive(false);
    }

    /*
    public void UseItem(string itemName)
    {
        for (int i = 0; i < itemSOs.Length; i++)
        {
            if (itemSOs[i].itemName == itemName)
            {
                itemSOs[i].UseItem();
            }
        }
    }
    */


    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if(itemSlot[i].isFull == false && itemSlot[i].itemName == itemName || itemSlot[i].quantity == 0)
            {
                int leftOverItems = itemSlot[i].AddItem(itemName, quantity, itemSprite, itemDescription);
                if (leftOverItems > 0)
                {
                    leftOverItems = AddItem(itemName, leftOverItems, itemSprite, itemDescription);
                }
                return leftOverItems;
            }
        }

        return quantity;
    }

    public void DeselectAllSlots()
    {
        for (int i = 0;i < itemSlot.Length; i++)
        {
            itemSlot[i].selectedShader.SetActive(false);
            itemSlot[i].thisItemSelected = false;
        }
    }

    

    //when a quest is started, it gets the current count of the items needed
    public int GetItemCount(string itemName)
    {
        int quantity = 0;
        foreach(ItemSlot i in itemSlot)
        {
            if (i.itemName == itemName)
            {
                quantity += i.quantity;
            } 
        }

        return quantity;
    }
}
