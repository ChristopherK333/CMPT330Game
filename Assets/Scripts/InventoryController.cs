using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{

    public GameObject inventoryPanel;
    public GameObject slotPrefab;
    public int slotCount; //for future use, in case you want to upgrade bag slots
    public GameObject[] itemPrefabs;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < slotCount; i++)
        {
            Slot slot = Instantiate(slotPrefab, inventoryPanel.transform).GetComponent<Slot>();
            if(i < itemPrefabs.Length)
            {
                GameObject item = Instantiate(itemPrefabs[i], slot.transform);
                item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero; // Center the item in the slot vectore2 does this
                slot.currentItem = item;
            }   
        }
    }


}
