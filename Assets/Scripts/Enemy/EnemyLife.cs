using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    int health = 5;

    public Material material;
    public bool isDead = false;
    float fade = 1f;
    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    void Update()
    {
        Dead();
    }
    void Dead()
    {
        if (isDead)
        {
            fade -= Time.deltaTime;
            if (fade <= 0)
            {
                fade = 0f;
                isDead = false;
                Destroy(gameObject);
            }
            material.SetFloat("_Fade", fade);
        }
    }

    public void Damage()
    {
        health--;
        if (health <= 0)
        {
            isDead = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            health--;
            Destroy(collision.gameObject);
            if(health <= 0)
            {
                isDead = true;
            }
        }
    }
}
