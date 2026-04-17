using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // checks if melee collides with an enemy, if so, reduce enemy hp
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyScript>().enemyHP -= 5f;
            Debug.Log("enemy hit");
        }
    }
}
