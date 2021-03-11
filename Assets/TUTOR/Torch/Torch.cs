using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    LevelManager levelManager;
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        levelManager.CreateTorch();
    }


    public void Fire()
    {
        spriteRenderer.color = Color.red;
        levelManager.Light();
    }

}
