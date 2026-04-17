using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemCollector : MonoBehaviour
{
    private InventoryController inventoryController;
    public GameObject notification;

    public float timer = 3.0f;

    public AudioSource itemSFX;

    // Start is called before the first frame update
    void Start()
    {

        inventoryController = FindObjectOfType<InventoryController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            Item item = collision.GetComponent<Item>(); //get component here is an item script
            playSFX();

            if (item != null)
            {
                //add the item if not null
                bool itemAdded = inventoryController.AddItem(collision.gameObject);

                if (itemAdded)
                {
                    //destroy the item if added to inventory
                    Destroy(collision.gameObject);
                    notification.SetActive(true);
                    Debug.Log("item picked up");

                    StartCoroutine(hideNotification());
                }
            }
        }
    }

    IEnumerator hideNotification()
    {
        // timer for the notification to disappear
        yield return new WaitForSeconds(timer);
        notification.SetActive(false);
    }

    public void playSFX()
    {
        itemSFX.Play();
    }
}
