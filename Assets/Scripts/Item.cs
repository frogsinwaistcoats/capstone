using UnityEngine;
using Yarn.Unity;

public class Item : MonoBehaviour
{
    [SerializeField]
    private string itemName;

    [SerializeField]
    private int quantity;

    [SerializeField]
    private Sprite sprite;

    [TextArea]
    [SerializeField]
    private string itemDescription;

    private InventoryManager inventoryManager;
    private bool playerFound = false;
    [SerializeField] private GameObject prompt;

    void Start()
    {
        inventoryManager = GameObject.Find("MenuCanvas").GetComponent<InventoryManager>();
    }

    private void Update()
    {
        if (playerFound && Input.GetKeyDown(KeyCode.E))
        {
            prompt.SetActive(false);
            int leftOverItems = inventoryManager.AddItem(itemName, quantity, sprite, itemDescription);
            if (leftOverItems <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                quantity = leftOverItems;
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        playerFound = true;
        prompt.SetActive(true);
    }

    public void OnTriggerExit(Collider other)
    {
        playerFound = false;
        prompt.SetActive(false);
    }

}
