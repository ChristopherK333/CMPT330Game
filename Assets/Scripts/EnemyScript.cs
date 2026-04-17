using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] floatingHealth healthBar;
    public float damage;
    public float enemyHP = 80f;

    public List<lootTable> lootDrops;

    public AudioSource hurtSFX;
    public AudioSource hitSFX;

    public Transform player;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        healthBar = GetComponentInChildren<floatingHealth>();
    }

    void Start()
    {
        healthBar.updateHealthBar(enemyHP);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // checks if player x axis is greater to flip sprite
        if (player.position.x > transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // checks if collides with the player, if so, reduce player hp
        if (other.gameObject.CompareTag("Player"))
        {
            playSFX();
            other.gameObject.GetComponent<PlayerController>().health -= damage;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // checks if the object has the projectle tag, if so, updated enemy hp bar
        if (other.CompareTag("Projectile"))
        {
            playSFX2();
            enemyHP -= 10f;
            healthBar.updateHealthBar(enemyHP);

            // if enemy dies, call the spawnloot function
            if (enemyHP <= 0)
            {
                spawnLoot();
                Destroy(gameObject);
            }
        }
    }

    public void spawnLoot()
    {
        // go through the list of loot drops
        foreach (lootTable item in lootDrops)
        {
            // roll a number then spawn the corresponding loot
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
        hurtSFX.Play();
    }

    public void playSFX2()
    {
        hitSFX.Play();
    }
}
