using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HotbarController : MonoBehaviour
{

    public GameObject hotbarPanel;
    public GameObject slotPrefab;
    public int slotCount = 6;  //change this if you want more slots, for now we are going to assume 6

    private ItemDictionary itemDictionary;

    private Key[] hotbarKeys;


    private void Awake()
    {
        itemDictionary = FindObjectOfType<ItemDictionary>();
        hotbarKeys = new Key[slotCount];  // Initialize the hotbar keys (1-6)
        for (int i = 0; i < slotCount; i++)
        {
            hotbarKeys[i] = i < 5 ? (Key)((int)Key.Digit1 + i) : Key.Digit0;  // Assign keys 1-6 to the hotbar saying last digit is assigned to digit 0
        }
    }


    private void Update()
    {
        //The following is checking for key presses
        for (int i = 0; i < slotCount; i++)
        {
            if (Keyboard.current[hotbarKeys[i]].wasPressedThisFrame)
            {
                //Use the item stored in the hotbar slot
                UseItemInSlot(i);

            }
        }
    }

    void UseItemInSlot(int index)
    {
        Slot slot = hotbarPanel.transform.GetChild(index).GetComponent<Slot>();
        if (slot.currentItem != null)
        {
            Item item = slot.currentItem.GetComponent<Item>();
            item.UseItem();
        }

    }



    public List<InventorySaveData> GetHotbarItems()
    {
        List<InventorySaveData> hotbarData = new List<InventorySaveData>();
        foreach (Transform slotTransform in hotbarPanel.transform)
        {
            Slot slot = slotTransform.GetComponent<Slot>();
            if (slot.currentItem != null)
            {
                Item item = slot.currentItem.GetComponent<Item>();
                hotbarData.Add(new InventorySaveData { itemID = item.ID, slotIndex = slotTransform.GetSiblingIndex() }); //get sibling index gets the index of a game object in conjuction to other game objects on the same level. theres a sibling hiearchy basically


            }
        }
        return hotbarData;
    }
    public void SetHotbarItem(List<InventorySaveData> inventorySaveData)
    {
        foreach (Transform child in hotbarPanel.transform)
        {
            Destroy(child.gameObject);

        }
        //create new slots
        for (int i = 0; i < slotCount; i++)
        {
            Instantiate(slotPrefab, hotbarPanel.transform); //sets inventory panel to parent of slot prefabs

        }
        foreach (InventorySaveData data in inventorySaveData)
        {
            if (data.slotIndex < slotCount)
            {
                Slot slot = hotbarPanel.transform.GetChild(data.slotIndex).GetComponent<Slot>();
                GameObject itemPrefab = itemDictionary.GetItemPrefab(data.itemID);
                if (itemPrefab != null)
                {
                    GameObject item = Instantiate(itemPrefab, slot.transform); //create a new item of this item prefab from the item dictionary
                    item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                    slot.currentItem = item;
                }
            }

        }
    }

}

