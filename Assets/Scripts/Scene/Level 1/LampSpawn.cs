using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LampSpawn : MonoBehaviour
{
    public Transform positionSpawn;
    public GameObject glowwormPrefab;

    public Light2D onLamp;

    public float timeSpawn;
    private float time;

    private void Start()
    {
        time = timeSpawn;
    }

    void Update()
    {
        if(onLamp.gameObject.activeSelf != false)
        {
            time -= Time.deltaTime;

            if (time < 0)
            {
                Spawn();
                time = timeSpawn;
            }
        }
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            onLamp.gameObject.SetActive(true);
            Destroy(collision.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       
    }

    private void Spawn()
    {
        GameObject glowworm = Instantiate(glowwormPrefab, positionSpawn.position + Random.insideUnitSphere, Quaternion.identity);
        print("спавн");
    }
}
