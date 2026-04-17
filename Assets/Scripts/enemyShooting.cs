using UnityEngine;

public class enemyShooting : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 2f;

    private float cooldown;

    public Transform player;
    public float attackRange = 6f;

    public AudioSource shootSFX;

    void Update()
    {
        // checks if player is within the enemy range, if so, start shooting
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange && Time.time >= cooldown)
        {
            playSFX();
            Shoot();
            cooldown = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        // logic for enemy shooting including the rotation so it faces players and spawning
        Vector2 direction = (player.position - firePoint.position).normalized;

        GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        proj.GetComponent<enemyProjectile>().SetDirection(direction);
    }

    public void playSFX()
    {
        shootSFX.Play();
    }
}
