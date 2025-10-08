using System.Collections;
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
    [SerializeField] private Sprite itemDescriptionSprite;

    public GameObject photograph;

    public InventoryManager inventoryManager;
    private bool playerFound = false;
    
    private bool collected = false;
    [SerializeField] private GameObject prompt;

    [SerializeField] private string id;

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }


    void Start()
    {
        //inventoryManager = InventoryManager.instance;
    }

    private void Update()
    {
        if (playerFound && Input.GetKeyDown(KeyCode.E) && !collected)
        {
            CollectItem();
            prompt.SetActive(false);
            StartCoroutine(ShowPhoto());
        }
    }

    public void CollectItem()
    {
        collected = true;
        int leftOverItems = inventoryManager.AddItem(itemName, quantity, sprite, itemDescription, itemDescriptionSprite);
        if (leftOverItems <= 0)
        {
            //Destroy(gameObject);
        }
        else
        {
            quantity = leftOverItems;
        }
    } 

    public IEnumerator ShowPhoto()
    {
        Debug.Log("Show photo");
        photograph.SetActive(true);
        yield return new WaitForSeconds(2);
        photograph.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!collected)
        {
            playerFound = true;
            prompt.SetActive(true);
        }
        
    }

    public void OnTriggerExit(Collider other)
    {
        if (!collected)
        {
            playerFound = false;
            prompt.SetActive(false);
        }
    }
}
