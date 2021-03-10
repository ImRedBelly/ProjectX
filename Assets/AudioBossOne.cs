using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBossOne : MonoBehaviour
{

    public AudioSource audioSource;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            audioSource.Play();
        }
    }
}
