using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadItems : MonoBehaviour
{
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HealthPlayer1 healthPlayer = FindObjectOfType<HealthPlayer1>();
            healthPlayer.MinusIntensity(0.1f);
        }
    }
}
