using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightUP : MonoBehaviour
{
    float light = 0.5f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HealthPlayer1 healthPlayer = FindObjectOfType<HealthPlayer1>();
            healthPlayer.maxLight += light;
        }
    }
}
