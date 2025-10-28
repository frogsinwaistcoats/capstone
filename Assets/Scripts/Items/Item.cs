using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private Sprite itemDescriptionSprite;

    public GameObject photograph;
    public Image photoFlower;
    public TextMeshProUGUI photoText;

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
            FindAnyObjectByType<AudioManager>().Play("CameraClick");
            StartCoroutine(ShowPhoto());
        }

        if (LoadYarnVariables.instance.GetBool("$canUseCamera"))
        {
            
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
        PlayerMovement.instance.canMove = false;
        Debug.Log("Show photo");

        if (InventoryManager.instance.CheckForAllFlowers() == true)
        {
            LoadYarnVariables.instance.SetYarnVariable("$hasGotFlowers", true);
            Debug.Log("all flowers collected");
        }

        InventoryManager.instance.newPhotosToSee = true;
        photograph.SetActive(true);
        photoFlower.sprite = itemDescriptionSprite;
        photoText.text = itemName;
        yield return new WaitForSeconds(1f);
        photograph.SetActive(false);
        PlayerMovement.instance.canMove = true;
    }


    private void OnTriggerEnter(Collider other)
    {

        if (!collected)
        {
            playerFound = true;
            prompt.SetActive(true);
        }
        if (LoadYarnVariables.instance.GetBool("$canUseCamera"))
        {
            
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
