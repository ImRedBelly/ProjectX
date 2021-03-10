using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Experimental.Rendering.Universal;

public class Key : MonoBehaviour
{
    public CinemachineVirtualCamera camera;

    public GameObject door;



    Material material;

    float fade = 1f;
    bool isDestroy = false;
    public bool isCam = false;

    private void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
    }
    private void Update()
    {
        if (isCam)
        {
            Shader();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            print("I");
            isCam = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    void Shader()
    {
        fade -= Time.deltaTime;
        if (fade <= 0)
        {
            fade = 0f;
        }
        material.SetFloat("_Fade", fade);
    }
}
