using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGun : MonoBehaviour
{
    public Rigidbody2D rb;
    void Start()
    {
        StartCoroutine(Shot());
    }
    IEnumerator Shot()
    {
        yield return new WaitForSeconds(0.1f);
        Vector2 direction = PlayerMovement.instance.transform.position - transform.position;
        rb.velocity = direction.normalized * 10;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Shield")
        {
            Destroy(gameObject);
        }
    }
}
