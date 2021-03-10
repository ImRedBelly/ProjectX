using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Boss : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody2D m_Rigidbody2D;
    public Animator animator;


    [Header("Options")]
    public float life = 10;
    public float speed = 5f;
    [SerializeField] private float m_DashForce = 25f;

    private bool facingRight = true;
    private bool m_FacingRight = true;


    [Header("Attack")]
    public GameObject shooting;

    public GameObject bullet;

    public GameObject enemy;
    private float distToPlayer;
    private float distToPlayerY;
    public float meleeDist = 1.5f;
    public float rangeDist = 10f;
    private bool canAttack = true;
    public Transform attackCheck;
    public float dmgValue = 4;


    private float randomDecision = 0;
    private bool doOnceDecision = true;
    private bool endDecision = false;

    [Header("Teleport")]
    public SpriteRenderer spriteRenderer;
    Material material;
    float fade = 1f;
    Coroutine start;
    Coroutine end;
    Coroutine Teleport;

    float attack = 5;
    float nextTeleport = 0;
    private void Start()
    {
        material = spriteRenderer.material;
    }
    void FixedUpdate()
    {
        //print(Mathf.Abs(distToPlayer));
        if (life <= 0)
            StartCoroutine(DestroyEnemy());

        else if (enemy != null)
        {
            distToPlayer = enemy.transform.position.x - transform.position.x;
            distToPlayerY = enemy.transform.position.y - transform.position.y;



            if (Mathf.Abs(distToPlayer) > rangeDist)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0f, m_Rigidbody2D.velocity.y);
                animator.SetBool("IsWaiting", true);
            }

            else if (Mathf.Abs(distToPlayer) > meleeDist && Mathf.Abs(distToPlayer) < rangeDist)
            {
                // стою и стреляю 

                if (nextTeleport <= 0)
                {
                    StartCoroutine(Portal());
                    nextTeleport = attack;
                }
                if (nextTeleport > 0)
                {
                    nextTeleport -= Time.deltaTime;
                }


                shooting.SetActive(true);
                animator.SetBool("IsWaiting", true);
                GetComponent<Rigidbody2D>().velocity = new Vector2(0f, m_Rigidbody2D.velocity.y);
                StartCoroutine(ShotSpecialBullet());

                if (Mathf.Abs(distToPlayerY) < 2f)
                {
                    // просто бегу
                    shooting.SetActive(false);
                    Run(distToPlayer / Mathf.Abs(distToPlayer));
                    if (nextTeleport <= 0)
                    {
                        RangeAttack();
                        nextTeleport = attack;
                    }
                    if (nextTeleport > 0)
                    {
                        nextTeleport -= Time.deltaTime;
                    }
                }
            }

            else if (Mathf.Abs(distToPlayer) > 0.25f && Mathf.Abs(distToPlayer) < meleeDist && Mathf.Abs(distToPlayerY) < 2f) // Melee Damage
            {
                shooting.SetActive(false);
                GetComponent<Rigidbody2D>().velocity = new Vector2(0f, m_Rigidbody2D.velocity.y); // бью и стою
                if ((distToPlayer > 0f && transform.localScale.x < 0f) || (distToPlayer < 0f && transform.localScale.x > 0f))
                    Flip();
                if (canAttack)
                {
                    MeleeAttack();
                }
            }
        }
        else
        {
            enemy = GameObject.Find("DrawCharacter");
        }



        if (transform.localScale.x * m_Rigidbody2D.velocity.x > 0 && !m_FacingRight && life > 0)
        {
            Flip();
        }
        else if (transform.localScale.x * m_Rigidbody2D.velocity.x < 0 && m_FacingRight && life > 0)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void ApplyDamage(float damage)
    {
        float direction = damage / Mathf.Abs(damage);
        damage = Mathf.Abs(damage);
        animator.SetBool("Hit", true);
        life -= damage;
        transform.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        transform.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction * 300f, 100f));
    }

    public void MeleeAttack()
    {
        transform.GetComponent<Animator>().SetBool("Attack", true);
        Collider2D[] collidersEnemies = Physics2D.OverlapCircleAll(attackCheck.position, 0.9f);
        for (int i = 0; i < collidersEnemies.Length; i++)
        {
            if (collidersEnemies[i].gameObject.tag == "Enemy" && collidersEnemies[i].gameObject != gameObject)
            {
                if (transform.localScale.x < 1)
                {
                    dmgValue = -dmgValue;
                }
                collidersEnemies[i].gameObject.SendMessage("ApplyDamage", dmgValue);
            }
            else if (collidersEnemies[i].gameObject.tag == "Player")
            {
                collidersEnemies[i].gameObject.GetComponent<HealthPlayer>().ApplyDamage(2f, transform.position);
            }
        }
        StartCoroutine(WaitToAttack(0.5f));
    }

    public void RangeAttack()
    {
            GameObject throwableProj = Instantiate(bullet, transform.position + new Vector3(transform.localScale.x * 0.5f, -0.2f), Quaternion.identity) as GameObject;
            throwableProj.GetComponent<ThrowableProjectile>().owner = gameObject;
            Vector2 direction = new Vector2(transform.localScale.x, 0f);
            throwableProj.GetComponent<ThrowableProjectile>().direction = direction;
        
    }

    public void Run(float positionX)
    {
        //distToPlayer / Mathf.Abs(distToPlayer) 
        animator.SetBool("IsWaiting", false);
        m_Rigidbody2D.velocity = new Vector2(positionX * speed, m_Rigidbody2D.velocity.y);
    }

    public void Idle()
    {
        m_Rigidbody2D.velocity = new Vector2(0f, m_Rigidbody2D.velocity.y);
        if (doOnceDecision)
        {
            animator.SetBool("IsWaiting", true);
            StartCoroutine(NextDecision(1f));
        }
    }

    public void EndDecision()
    {
        randomDecision = Random.Range(0.0f, 0.6f);
        endDecision = true;
    }

    IEnumerator ShotSpecialBullet()
    {
        yield return new WaitForSeconds(3);
        // стрелять особенной пулей
    }

    IEnumerator WaitToAttack(float time)
    {
        canAttack = false;
        yield return new WaitForSeconds(time);
        canAttack = true;
    }


    IEnumerator NextDecision(float time)
    {
        doOnceDecision = false;
        yield return new WaitForSeconds(time);
        EndDecision();
        doOnceDecision = true;
        animator.SetBool("IsWaiting", false);
    }

    IEnumerator DestroyEnemy()
    {
        CapsuleCollider2D capsule = GetComponent<CapsuleCollider2D>();
        capsule.size = new Vector2(1f, 0.25f);
        capsule.offset = new Vector2(0f, -0.8f);
        capsule.direction = CapsuleDirection2D.Horizontal;
        transform.GetComponent<Animator>().SetBool("IsDead", true);
        yield return new WaitForSeconds(0.25f);
        m_Rigidbody2D.velocity = new Vector2(0, m_Rigidbody2D.velocity.y);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }





    IEnumerator Portal()
    {
        TeleportMinus();

        yield return new WaitForSeconds(1);

        Vector2 teleportPosition = enemy.transform.position;
        teleportPosition.y += 5;
        transform.position = teleportPosition;

        TeleportPlus();
    }
    public void TeleportMinus()
    {
        start = StartCoroutine(Minus());
    }
    IEnumerator Minus()
    {
        while (fade >= 0)
        {
            yield return new WaitForSeconds(0.1f);
            fade -= 0.1f;
            if (fade < 0)
            {
                StopCoroutine(start);
                fade = 0;
            }
            material.SetFloat("_Fade", fade);
        }
    }
    public void TeleportPlus()
    {
        end = StartCoroutine(Plus());
    }
    IEnumerator Plus()
    {
        while (fade <= 1)
        {
            yield return new WaitForSeconds(0.1f);
            fade += 0.2f;
            if (fade > 1)
            {
                StopCoroutine(end);
                fade = 1;
            }
            material.SetFloat("_Fade", fade);
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
