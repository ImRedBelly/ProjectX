using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUN : MonoBehaviour
{
    public GameObject bullet;
    public GameObject bulletClone;
    public GameObject gun;
    public GameObject gunPosition;

    public LayerMask maskPlayer;
    public float radius = 7;
    bool isPlayer;
    void Start()
    {
        
    }
    void Update()
    {
        isPlayer = Physics2D.OverlapCircle(transform.position, radius, maskPlayer);
        if (isPlayer)
        {
            Vector2 direction = PlayerMovement.instance.transform.position - transform.position;
            gun.transform.up = -direction;

            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.7f);
            bulletClone = Instantiate(bullet, gunPosition.transform.position, Quaternion.identity);
            bulletClone.GetComponent<ThrowableProjectile>().direction = -gun.transform.up;
            StopAllCoroutines();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
