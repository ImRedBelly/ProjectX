using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldParticle : MonoBehaviour
{
    public ParticleSystem shot;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Instantiate(shot,transform.position, Quaternion.identity);
        }
    }
}
