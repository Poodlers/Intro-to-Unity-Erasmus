using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    PlayerInput playerInput;

    private SpriteRenderer spriteRenderer;

    private Animator animator;

    private Rigidbody2D rb;

    [SerializeField]
    private float maxSpeed;

    [SerializeField]
    private float deceleration;

    [SerializeField]
    private float acceleration;

    private Vector2 moveInput;

    private float smokeInterval = 1f;

    private float smokeCounter = 0;
    private bool isAnimLocked = false;
    // Start is called before the first frame update

    private ScoreController scoreController;
    void Start()
    {
        playerInput = new PlayerInput();
        scoreController = GameObject.Find("ScoreContainer").GetComponent<ScoreController>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerInput.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = playerInput.Player.Move.ReadValue<Vector2>();
        smokeCounter += Time.deltaTime;

    }
    void FixedUpdate()
    {
        if (isAnimLocked) return;
        GroundMove();

    }

    private void GroundMove()
    {
        float dynamicSpeedFactor = 1;

        if (moveInput != Vector2.zero)
        {
            //move

            Vector2 wishDir = moveInput.normalized;
            dynamicSpeedFactor = (Vector2.Dot(wishDir, transform.forward) + 1.0f) / 2.0f;
            rb.velocity += wishDir * acceleration * (1 + dynamicSpeedFactor);
            animator.SetBool("isMoving", true);
            spriteRenderer.flipX = wishDir.x < 0;
            if (smokeCounter > smokeInterval)
            {
                smokeCounter = 0;
                SpecialEffects.specialEffects.CreateSmoke(transform);
            }

        }
        else if (rb.velocity != Vector2.zero)
        {
            //decelerate
            animator.SetBool("isMoving", true);
            rb.velocity -= rb.velocity * deceleration;
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            scoreController.AddScore(1);
        }
    }
}
