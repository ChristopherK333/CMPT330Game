using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class lootTable
{
    public GameObject prefab;
    [Range(0, 100)] public float dropChance;
}
