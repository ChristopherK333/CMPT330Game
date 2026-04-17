using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chestOpening : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite chestSprite;
    public Sprite openedSprite;

    public List<lootTable> lootDrops;

    public AudioSource chestSFX;
    bool playerInRange = false;
    bool isOpened = false;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // checks if player is in chest range and if e key is pressed
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            // if opened already, do nothing
            if (isOpened)
            {
                return;
            }

            // set boolean to true and change sprite to opened sprite
            isOpened = true;
            spriteRenderer.sprite = openedSprite;
            playSFX();
            spawnLoot();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // checks if player collides with the chest
        if (collision.gameObject.CompareTag("Player"))
            playerInRange = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            playerInRange = false;
    }

    public void spawnLoot()
    {
        // goes through the loot drops
        foreach (lootTable item in lootDrops)
        {
            // roll a number then spawn corresponding loot
            float roll = Random.Range(0f, 100f);

            if (roll <= item.dropChance)
            {
                Instantiate(item.prefab, transform.position, Quaternion.identity);
            }

            break;
        }
    }

    public void playSFX()
    {
        chestSFX.Play();
    }
}
