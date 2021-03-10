using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextReader : MonoBehaviour
{
    public Text text;
    AudioManager audioManager;
    public AudioClip audioClip;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            audioManager.PlaySound(audioClip);
            text.gameObject.SetActive(true);
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        text.gameObject.SetActive(false);
        audioManager.audioSource.Stop();
    }

}
