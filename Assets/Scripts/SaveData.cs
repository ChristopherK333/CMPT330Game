using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public Vector3 playerPosition; //the position of the player
    public List<InventorySaveData> inventorySaveData; //the inventory data of the player
    public List<InventorySaveData> hotbarSaveData;
}
