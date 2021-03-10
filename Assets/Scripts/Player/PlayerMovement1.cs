using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement1 : MonoBehaviour
{
    AudioManager audioManager;

    public static PlayerMovement1 Instance;
    public HealthPlayer1 healthPlayer;

    public LayerMask enemy;
    public GameObject shotPosition;
    public GameObject bulletPrefab;

    public bool isInfinitelyBullets;

    bool isAttack = false;

    [Header("Player Component")]
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public RuntimeAnimatorController[] playerSkins;



    public Rigidbody2D rb;

    [Header("Move Settings")]
    public float moveSpeed;
    public float jumpForce;
    public float dashSpeed;
    float moveX;


    public bool looksToRight = true;


    [Header("Jump Settings")]
    public LayerMask groundMask;
    public bool isGrounded;
    public bool isDoubleJump;



    [Header("Dash Settings")]
    float dashTime = 1;
    float currentDash;
    float dashВuration = 0.08f;





    [Header("AbilityClimb")] //абилка лазанья

    public bool isAbilityClimb;

    public bool isClimb;

    public bool wallSliding;
    public float wallSlidingSpeed = 5f;
    public bool isVerticalGrounded;
    public float timeClimb = 2f;
    private float nextClimb;


    State state;

    public enum State
    {
        Idle,
        Move,
        Jump,
        Dash
    }

    public enum Skins
    {
        Backpack,
        WithoutBackpack
    }


    public AudioClip attack;
    public AudioClip jump;
    public AudioClip dash;
    public AudioClip shot;
    public AudioClip climp;
    public AudioClip shield;
    public AudioClip death;



    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        animator.runtimeAnimatorController = playerSkins[(int)Skins.Backpack];

        if (SceneManager.GetActiveScene().buildIndex > 5)
        {
            animator.runtimeAnimatorController = playerSkins[(int)Skins.WithoutBackpack];
        }

        if (SceneManager.GetActiveScene().buildIndex < 5)
        {
            isInfinitelyBullets = true;
        }
        else
        {
            isInfinitelyBullets = false;
        }

        state = State.Move;

    }

    void Update()
    {
        if (looksToRight)
            isVerticalGrounded = Physics2D.Raycast(transform.position, transform.right, 1.5f, groundMask);

        else
            isVerticalGrounded = Physics2D.Raycast(transform.position, -transform.right, 1.5f, groundMask);

        isGrounded = Physics2D.Raycast(transform.position, -transform.up, 2, groundMask);
        animator.SetBool("Ground", isGrounded);

        Debug.DrawRay(transform.position, -transform.up * 2);
        if (healthPlayer.imLife)
        {
            ShotAttack();
            MeleeAttack();
            Climb();

            switch (state)
            {
                case State.Move:
                    if (isGrounded)
                    {
                        isDoubleJump = true;
                    }
                    if (Input.GetButtonDown("Jump") && isGrounded)
                    {
                        state = State.Jump;
                    }
                    else if (Input.GetButtonDown("Jump") && isDoubleJump)
                    {
                        isDoubleJump = false;
                        state = State.Jump;
                    }

                    if (Input.GetKeyDown(KeyCode.LeftShift) && currentDash <= 0)
                    {
                        currentDash = dashTime;
                        dashВuration = 0.08f;
                        state = State.Dash;
                    }
                    if (currentDash > 0)
                    {
                        currentDash -= Time.deltaTime;
                    }
                    animator.SetBool("Climb", isVerticalGrounded);
                    Move();
                    break;

                case State.Jump:
                    if (!isGrounded)
                    {
                        state = State.Move;
                    }

                    
                    Jump();

                    break;

                case State.Dash:
                    if (dashВuration < 0)
                    {
                        state = State.Move;
                    }

                    dashВuration -= Time.deltaTime;
                    Dash();
                    break;
            }
        }
        animator.SetFloat("ySpeed", rb.velocity.y);
    }


    private void Climb()
    {

        if (isAbilityClimb && !isGrounded && isVerticalGrounded && moveX != 0)
        {
            if (Input.GetKey(KeyCode.W) && (nextClimb <= 0))
            {
                nextClimb = timeClimb;

                rb.velocity = new Vector2(0, 20f); //прыжок
                
                animator.SetBool("Climb", isVerticalGrounded);
            }
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue)); //скольжение вниз

        }

        
        if (nextClimb > 0)
            nextClimb -= Time.deltaTime;
    }
    void Move()
    {
        moveX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);

        animator.SetFloat("Speed", rb.velocity.magnitude);

        if (moveX > 0 && !looksToRight)
            Flip();

        if (moveX < 0 && looksToRight)
            Flip();
    }

    void Jump()
    {
        audioManager.PlaySound(jump);

        animator.SetBool("Ground", false);
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    void Dash()
    {
        animator.SetTrigger("Dash");

        if (looksToRight)
            rb.AddForce(transform.right * dashSpeed, ForceMode2D.Impulse);
        else
            rb.AddForce(-transform.right * dashSpeed, ForceMode2D.Impulse);
    }

    void Flip()
    {
        looksToRight = !looksToRight;

        Vector2 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void MeleeAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isAttack = true;
            animator.SetBool("Attack", isAttack);
            RaycastHit2D hit;

            if (looksToRight)
                hit = Physics2D.Raycast(transform.position, Vector2.right, 3, enemy);
            else
                hit = Physics2D.Raycast(transform.position, -Vector2.right, 3, enemy);

            if (hit)
            {
                if (hit.collider.tag == "Enemy")
                {
                    EnemyLife enemyLife = hit.transform.GetComponent<EnemyLife>();
                    enemyLife.Damage();
                }
            }
        }
    }

    void ShotAttack()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Instantiate(bulletPrefab, shotPosition.transform.position, transform.rotation);

            if (!isInfinitelyBullets)
            {
                healthPlayer.MinusIntensity(0.1f);
            }

        }
    }


    public void IsAttack()
    {
        isAttack = false;
        animator.SetBool("Attack", isAttack);
    }

}
