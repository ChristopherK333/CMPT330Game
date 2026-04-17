using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    Transform originalParent; //this is the original slot the item is in so we can return it if needed.
    CanvasGroup canvasGroup; //this is used to make the item invisible while dragging it.
    // Start is called before the first frame update


    public float minDropDistance = 1f;  //change this later if you want more of a radius of a drop
    public float maxDropDistance = 2f;
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent; //store the original parent of the item
        transform.SetParent(transform.root); //set the parent to the root so it can be dragged around the canvas 
        canvasGroup.blocksRaycasts = false; //make the item not block raycasts so we can drop it on other slots
        canvasGroup.alpha = 0.6f; //make the item semi-transparent while dragging
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position; //Follow the mouse
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        canvasGroup.blocksRaycasts = true; //make the item block raycasts again, this means we can click on it again
        canvasGroup.alpha = 1f; //make the item fully visible again

        Slot dropSlot = eventData.pointerEnter?.GetComponent<Slot>(); //? makes it nullable so if we drop it on something that doesn't have a slot component it won't throw an error, it will just return null
        if (dropSlot == null)
        {
            GameObject item = eventData.pointerEnter; //if we didn't drop it on a slot, we want to check if we dropped it on another item, if we did we want to get the slot of that item and drop it there
            if (item != null)
            {
                dropSlot = item.GetComponentInParent<Slot>(); //if we dropped it on another item, get the slot of that item
            }
        }

        Slot originalSlot = originalParent.GetComponent<Slot>();

        if (dropSlot != null)
        {
            if (dropSlot.currentItem != null)
            {
                dropSlot.currentItem.transform.SetParent(originalSlot.transform); //if the slot we dropped on already has an item, move that item back to the original slot
                originalSlot.currentItem = dropSlot.currentItem; //update the original slot's current item to the item that was in the drop slot
                dropSlot.currentItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero; //reset the position of the item in the drop slot

            }
            else
            {
                originalSlot.currentItem = null; //if the slot we dropped on doesn't have an item, set the original slot's current item to null
            }
            //whether there is or isnt a null we need to move our item in the slot
            transform.SetParent(dropSlot.transform); //set the parent of the item to the drop slot
            dropSlot.currentItem = gameObject; //update the drop slot's current item to the item we just dropped

        }
        //now if the player drops the item to somwhere that isn't a slot we want to snap it back to the original place. 
        else
        {

            //dropping not within the boundaries. 
            if (!IsWithinInventory(eventData.position))
            {
                DropItem(originalSlot);
                //Drop our item
            }
            else
            {
                //snap back to og slot
                transform.SetParent(originalParent);
            }



            transform.SetParent(originalParent); //if we didn't drop it on a slot, return it to the original slot 
        }
        GetComponent<RectTransform>().anchoredPosition = Vector2.zero; //reset the position of the item to the center of the slot



    }


    bool IsWithinInventory(Vector2 mousePosition)
    {
        RectTransform inventoryRect = originalParent.parent.GetComponent<RectTransform>();
        return RectTransformUtility.RectangleContainsScreenPoint(inventoryRect, mousePosition);
    }

    void DropItem(Slot originalSlot)
    {
        originalSlot.currentItem = null;


        //find player so we can drop items near our player position. 
        Transform playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (playerTransform == null)
        {
            Debug.LogError("Missing 'player' tag");
            return;
        }


        //random drop position
        Vector2 dropOffset = Random.insideUnitCircle.normalized * Random.Range(minDropDistance, maxDropDistance);
        Vector2 dropPosition = (Vector2)playerTransform.position + dropOffset; //here you have to typecast vector2 as player postion is vector 3 by default 
        //Instantiate drop item
        Instantiate(gameObject, dropPosition, Quaternion.identity); //make a new version of the game object which is going to appear in the game scene



        //Destroy the UI one
        Destroy(gameObject);
    }
}
