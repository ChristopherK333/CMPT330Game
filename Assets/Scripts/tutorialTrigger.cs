using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialTrigger : MonoBehaviour
{
    public GameObject tutorialText;

    void OnTriggerStay2D(Collider2D other)
    {
        // enables tutorial popup if player is within trigger
        if (other.CompareTag("Player"))
        {
            tutorialText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // destroys tutorial popup if player leaves trigger
        if (other.CompareTag("Player"))
        {
            tutorialText.SetActive(false);
            Destroy(gameObject);
        }
    }
}
