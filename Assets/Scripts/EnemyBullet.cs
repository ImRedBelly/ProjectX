using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    public Rigidbody2D rb;
    public float bulletSpeed;


    private void Start()
    {
        if (PlayerMovementT.Instance != null)
        {
            Vector2 directionToPlayer = PlayerMovementT.Instance.transform.position - transform.position;
            rb.velocity = directionToPlayer.normalized * bulletSpeed;
        }
        else
        {
            Destroy(gameObject);
        }
    }

  

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerShield"))
        {
            print("эт щит");
        }
        if (collision.gameObject.CompareTag("Lamp"))
        {
            print("Win");
        }

    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
