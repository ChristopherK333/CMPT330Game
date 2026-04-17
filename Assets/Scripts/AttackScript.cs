using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public GameObject bowWeapon;
    private Camera mainCam;
    private Vector3 mousePos;
    public GameObject arrow;
    public Transform arrowTransform;
    public bool canFire;
    private float timer;
    public float timeBetweenFiring;

    public GameObject Melee;
    bool isAttacking = false;
    float atkDuration = 0.3f;
    float atkTimer = 0f;

    public AudioSource shootSFX;

    private float bowTimer = 0f;
    public float bowDuration = 0.2f;



    public float meleeCooldown = 1.5f;
    private float meleeTimer = 0f;
    private bool canMelee = true;
    public GameObject menuCanvas;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        Melee.SetActive(false);
        bowWeapon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {



        // melee rotation
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        CheckMeleeTimer();

        // Melee cooldown timer
        if (!canMelee)
        {
            meleeTimer += Time.deltaTime;
            if (meleeTimer >= meleeCooldown)
            {
                meleeTimer = 0;
                canMelee = true;
            }
        }

        // checks ranged weapon cooldown
        if (!canFire)
        {
            timer += Time.deltaTime;
            if (timer > timeBetweenFiring)
            {
                canFire = true;
                timer = 0;
            }
        }

        // fire ranged weapon
        //if (!menuCanvas.activeSelf && Input.GetMouseButton(0) && canFire)
        if (Input.GetMouseButton(0) && canFire)
        {
            playSFX();
            bowWeapon.SetActive(true); // enable visual
            canFire = false;
            bowTimer = bowDuration;
            Instantiate(arrow, arrowTransform.position, Quaternion.identity); // spawn arrow
        }

        // melee attack
        //if (!menuCanvas.activeSelf && Input.GetMouseButton(1))
        if(Input.GetMouseButton(1))
            {
            OnAttack();
        }

        if (bowWeapon.activeSelf)
        {
            bowTimer -= Time.deltaTime;
            if (bowTimer <= 0)
            {
                bowWeapon.SetActive(false);
            }
        }
    }

    void OnAttack()
    {
        if (!isAttacking && canMelee)
        {
            Melee.SetActive(true); // enables melee visual
            isAttacking = true;
            canMelee = false; //sets a cooldown to our attacks
        }
    }

    // melee cooldown
    void CheckMeleeTimer()
    {
        if (isAttacking)
        {
            atkTimer += Time.deltaTime;
            if (atkTimer >= atkDuration)
            {
                atkTimer = 0;
                isAttacking = false;
                Melee.SetActive(false);
            }
        }
    }

    public void playSFX()
    {
        shootSFX.Play();
    }
}
