﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Boss : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody2D Rigidbody2D;
    public Animator animator;
    public Animator animatorHand;
    public GameObject shieldBoss;


    [Header("Options")]
    public float life = 10;
    public float speed = 5f;

    private bool facingRight = true;

    [Header("Melee Attack")]
    float timerIsAttack = 4;
    float timerMeleeAttack = 4;

    [Header("Attack")]
    public GameObject shooting;

    public GameObject shockWave;
    public GameObject bullet;
    public Transform positionWave;

    public GameObject enemy;
    private float distToPlayer;

    public float meleeDist = 1.5f;

    public Transform attackCheck;
    private bool canAttack = true;
    public float dmgValue = 4;

    public BossState activState;
    public enum BossState
    {
        IDLE,
        MOVE,
        MELEEATTACK,
        MELEEEASYATTACK,
        MELEEHARDATTACK
    }
    private void Start()
    {
        activState = BossState.IDLE;
    }

    void FixedUpdate()
    {
        //print(Mathf.Abs(distToPlayer));

        if (life <= 0)
            StartCoroutine(DestroyEnemy());

        else if (enemy != null)
        {
            distToPlayer = enemy.transform.position.x - transform.position.x;

            if (Mathf.Abs(distToPlayer) < meleeDist && timerMeleeAttack <= 0)
            {
                activState = BossState.MELEEATTACK;
                timerMeleeAttack = 3;
            }
            else
            {
                timerMeleeAttack -= Time.deltaTime;
            }

            switch (activState)
            {
                case BossState.IDLE:
                    Idle();

                    if (timerIsAttack > 0)
                        timerIsAttack -= Time.deltaTime;
                    else
                    {

                        animator.SetBool("Attack", false);
                        animatorHand.SetBool("Attack", false);


                        int randomAct = Random.Range(0, 2);
                        if (randomAct == 0)
                        {
                            print("0");
                            
                            activState = BossState.MELEEEASYATTACK;
                        }
                        else if (randomAct == 1)
                        {
                            print("1");
                            
                            activState = BossState.MELEEHARDATTACK;
                        }
                        else if (randomAct == 2)
                        {
                            activState = BossState.MOVE;
                        }
                    }
                    break;


                case BossState.MOVE:



                    Run(distToPlayer);
                    if (Mathf.Abs(distToPlayer) > 0.25f && Mathf.Abs(distToPlayer) < meleeDist)
                        activState = BossState.MELEEATTACK;
                    break;



                case BossState.MELEEATTACK:
                    GetComponent<Rigidbody2D>().velocity = new Vector2(0f, Rigidbody2D.velocity.y);
                    if ((distToPlayer > 0f && transform.localScale.x < 0f) || (distToPlayer < 0f && transform.localScale.x > 0f))
                        Flip();
                    if (canAttack)
                        animator.SetBool("Attack", true);
                        animatorHand.SetBool("Attack", true);
                    // MeleeAttack();
                    break;



                case BossState.MELEEEASYATTACK:

                    animator.SetBool("Attack", true);
                    animatorHand.SetBool("Attack", true);

                    if ((distToPlayer > 0f && transform.localScale.x < 0f) || (distToPlayer < 0f && transform.localScale.x > 0f))
                        Flip();
                    shooting.SetActive(true);
                    timerIsAttack = 3;
                    break;


                case BossState.MELEEHARDATTACK:

                    animator.SetBool("Attack", true);
                    animatorHand.SetBool("Attack", true);

                    if ((distToPlayer > 0f && transform.localScale.x < 0f) || (distToPlayer < 0f && transform.localScale.x > 0f))
                        Flip();
                    ShotSpecialBullet();
                    timerIsAttack = 3;
                    break;
            }
        }
        else
        {
            enemy = GameObject.Find("DrawCharacter");
        }



        if (transform.localScale.x * Rigidbody2D.velocity.x > 0 && !facingRight && life > 0)
        {
            Flip();
        }
        else if (transform.localScale.x * Rigidbody2D.velocity.x < 0 && facingRight && life > 0)
        {
            Flip();
        }
    }

    void Flip()
    {

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    public void ApplyDamage(float damage)
    {
        // float direction = damage / Mathf.Abs(damage);
        damage = Mathf.Abs(damage);
        animator.SetBool("Hit", true);
        life -= damage;
        //transform.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        //transform.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction * 300f, 100f));
    }

    public void MeleeAttack()
    {
        Collider2D[] player = Physics2D.OverlapCircleAll(attackCheck.position, 0.9f);
        for (int i = 0; i < player.Length; i++)
        {
            if (player[i].gameObject.tag == "Player")
                player[i].gameObject.GetComponent<HealthPlayer>().ApplyDamage(2f, transform.position);
        }
        StartCoroutine(WaitToAttack(0.5f));
    }

    public void Run(float position)
    {
        animator.SetBool("IsWaiting", false);
        animatorHand.SetBool("IsWaiting", false);
        Rigidbody2D.velocity = new Vector2(position / Mathf.Abs(position) * speed, Rigidbody2D.velocity.y);
    }

    public void Idle()
    {
        Rigidbody2D.velocity = new Vector2(0f, Rigidbody2D.velocity.y);
        animator.SetBool("IsWaiting", true);
        animatorHand.SetBool("IsWaiting", true);
    }



    void ShotSpecialBullet()
    {
        GameObject wave = Instantiate(shockWave, positionWave.position, Quaternion.Euler(0, 0, 90));
        wave.GetComponent<ShockWave>().direction = -transform.up * transform.localScale.x;

        activState = BossState.MOVE;
    }

    IEnumerator WaitToAttack(float time)
    {
        canAttack = false;
        yield return new WaitForSeconds(time);
        canAttack = true;
        activState = BossState.IDLE;
    }




    IEnumerator DestroyEnemy()
    {
        CapsuleCollider2D capsule = GetComponent<CapsuleCollider2D>();
        capsule.size = new Vector2(1f, 0.25f);
        capsule.offset = new Vector2(0f, -0.8f);
        capsule.direction = CapsuleDirection2D.Horizontal;
        transform.GetComponent<Animator>().SetBool("IsDead", true);
        yield return new WaitForSeconds(0.25f);
        Rigidbody2D.velocity = new Vector2(0, Rigidbody2D.velocity.y);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            ApplyDamage(1);
            shieldBoss.SetActive(true);
            activState = BossState.MOVE;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (enemy != null)
        {
            Gizmos.DrawLine(transform.position, enemy.transform.position);
        }

    }
}
