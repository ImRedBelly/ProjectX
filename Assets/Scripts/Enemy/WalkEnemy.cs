using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkEnemy : MonoBehaviour
{
    AudioManager audioManager;


    HealthPlayer1 healthPlayer;
    public Rigidbody2D rb;
    public float speed;
    float distanceToPlayer;

    [Header("Attack Settings")]
    public bool isAttack;
    public float timeAttack = 3;
    float currentAttack;

    [Header("Shot Settings")]
    public GameObject shotPosition;
    public GameObject bulletEnemy;
    public bool isShot;

    public Animator animator;
    public bool looksToRight = true;



    public AudioClip attack;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        speed = Random.Range(0.1f, 1.5f);
        healthPlayer = FindObjectOfType<HealthPlayer1>();


    }

    void Update()
    {
        if (healthPlayer.imLife)
        {
            distanceToPlayer = Vector3.Distance(transform.position, PlayerMovementT.Instance.transform.position);

            if (!isShot && isAttack && distanceToPlayer < 20)
            {
                
                Move();

                

            }
            if (distanceToPlayer < 3)
            {
                Attack();

            }
            if (isShot && !isAttack && distanceToPlayer < 15)
            {
                Shot();
            }


 
        }
    }
    void Move()
    {


        Vector3 enemyPosition = transform.position;
        Vector3 direction = PlayerMovementT.Instance.transform.position - enemyPosition;
        rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);
        animator.SetFloat("Speed", rb.velocity.magnitude);

        if (direction.x < 0 && !looksToRight)
            Flip();

        if (direction.x > 0 && looksToRight)
            Flip();




    }
    void Attack()
    {
        if (currentAttack <= 0)
        {
            print("damage");
            audioManager.PlaySound(attack);
            animator.SetTrigger("Attack");
            currentAttack = timeAttack;
            healthPlayer.MinusIntensity(0.3f);


            
        }
        if (currentAttack > 0)
        {
            currentAttack -= Time.deltaTime;
        }
    }
    void Shot()
    {
        if (currentAttack <= 0)
        {
            currentAttack = timeAttack;

            if (PlayerMovementT.Instance != null)
            {
                Instantiate(bulletEnemy,shotPosition.transform.position, Quaternion.identity);
            }
        }
        if (currentAttack > 0)
        {
            currentAttack -= Time.deltaTime;
        }
    }
    void Flip()
    {
        looksToRight = !looksToRight;

        Vector2 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }


    
}
