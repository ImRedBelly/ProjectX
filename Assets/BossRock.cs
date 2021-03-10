using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRock : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 4);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HealthPlayer1 healthPlayer = FindObjectOfType<HealthPlayer1>();
            healthPlayer.MinusIntensity(0.4f);
            Destroy(gameObject);
        }
        if (!collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
