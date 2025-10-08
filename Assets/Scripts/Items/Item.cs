using UnityEngine;
using UnityEngine.VFX;
using Yarn.Unity;

public class Item : MonoBehaviour//, IDataPersistence
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

    public InventoryManager inventoryManager;
    private bool playerFound = false;
    //private bool collected = false;
    [SerializeField] private GameObject prompt;

    [SerializeField] private string id;

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    /*
    public void LoadData(GameData data)
    {
        data.flowersCollected.TryGetValue(id, out collected);
        if (collected)
        {
            CollectItem();
        }
    }

    public void SaveData(GameData data)
    {
        if (data.flowersCollected.ContainsKey(id))
        {
            data.flowersCollected.Remove(id);
        }
        data.flowersCollected.Add(id, collected);
    }
    */

    void Start()
    {
        //inventoryManager = InventoryManager.instance;
    }

    private void Update()
    {
        if (playerFound && Input.GetKeyDown(KeyCode.E))
        {
            CollectItem();
            prompt.SetActive(false);
        }
    }

    public void CollectItem()
    {
        //collected = true;
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
