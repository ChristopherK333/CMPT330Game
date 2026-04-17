using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lockScript : MonoBehaviour
{
    public GameObject doorObject;
    private Collider2D doorCollider;
    private SpriteRenderer spriteRenderer;
    public Sprite openedDoor;
    public Sprite closedDoor;

    public GameObject keyObject;
    private SpriteRenderer keyRenderer;
    public Sprite glowingKey;
    public Sprite defaultKey;

    public AudioSource doorSFX;

    void Start()
    {
        // references the key and door objects
        doorCollider = doorObject.GetComponent<Collider2D>();
        spriteRenderer = doorObject.GetComponent<SpriteRenderer>();

        keyRenderer = keyObject.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // opens door if key is on the pressure plate and change visual
        if (other.CompareTag("Key"))
        {
            Debug.Log("Open Sesame");
            doorCollider.isTrigger = true;

            spriteRenderer.sprite = openedDoor;
            keyRenderer.sprite = glowingKey;

            playSFX();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // closes door if key is on the pressure plate and change visual
        if (other.CompareTag("Key"))
        {
            Debug.Log("Closed");
            doorCollider.isTrigger = false;

            spriteRenderer.sprite = closedDoor;
            keyRenderer.sprite = defaultKey;
            
            playSFX();
        }
    }

    public void playSFX()
    {
        doorSFX.Play();
    }
}
