using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : MonoBehaviour
{
    Material material;

    public bool isDead = false;
    float fade = 1f;
    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    private void FixedUpdate()
    {
        DeadPlayer();
    }
    void DeadPlayer()
    {
        if (isDead)
        {
            fade -= Time.deltaTime;
            if(fade <= 0)
            {
                fade = 0f;
                isDead = false;
                GameManager.Instance.LoadScene();
            }
            material.SetFloat("_Fade", fade);
        }
    }
}
