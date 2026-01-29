using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    private bool canDash = true;

    [SerializeField] private float startDashTime = 0.2f;
    [SerializeField] private float dashSpeed = 15f;
    float currentDashTime;

    public GameObject Trail;

    // Start is called before the first frame update
    void Start()
    {
        Trail.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = moveInput * moveSpeed;

        if (canDash && Input.GetKeyDown(KeyCode.Space))
        {
            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
            {
                StartCoroutine(Dash(new Vector2(1f, 1f)));
            }

            else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
            {
                StartCoroutine(Dash(new Vector2(1f, -1f)));
            }

            else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W))
            {
                StartCoroutine(Dash(new Vector2(-1f, 1f)));
            }

            else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S))
            {
                StartCoroutine(Dash(new Vector2(-1f, -1f)));
            }

            else if (Input.GetKey(KeyCode.W))
            {
                StartCoroutine(Dash(Vector2.up));
            }

            else if (Input.GetKey(KeyCode.A))
            {
                StartCoroutine(Dash(Vector2.left));
            }

            else if (Input.GetKey(KeyCode.S))
            {
                StartCoroutine(Dash(Vector2.down));
            }

            else if (Input.GetKey(KeyCode.D))
            {
                StartCoroutine(Dash(Vector2.right));
            }
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private IEnumerator Dash(Vector2 direction)
    {
        Trail.SetActive(true);
        canDash = false;
        currentDashTime = startDashTime;

        while (currentDashTime > 0f)
        {
            currentDashTime -= Time.deltaTime;
            rb.velocity = direction * dashSpeed;
            yield return null;
        }

        rb.velocity = new Vector2(0f, 0f);
        canDash = true;
        Trail.SetActive(false);
    }
}
