using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public ParticleSystem shield;
    public CircleCollider2D materialShield;

    public GameObject EnemyBoss;

    private void Start()
    {
        transform.position = PlayerMovementT.Instance.transform.position;
    }


    private void Update()
    {
        ActivShield();
        transform.position = PlayerMovementT.Instance.transform.position;

    }

    private void ActivShield()
    {
        if (Input.GetMouseButtonDown(2))
        {
            materialShield.enabled = true;
            shield.Play();
        }
        if (shield.isStopped)
        {
            materialShield.enabled = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.CompareTag("EnemyBullet"))
        //{
        //    print("эт щит");
        //    if (EnemyBoss != null)
        //    {
        //        Vector3 posBullet = collision.gameObject.transform.position;
        //        Vector3 dirToEnemy = EnemyBoss.transform.position - posBullet;
        //        collision.gameObject.GetComponent<Rigidbody2D>().velocity = dirToEnemy.normalized * 10f;
        //    }
        //}
    }

}
