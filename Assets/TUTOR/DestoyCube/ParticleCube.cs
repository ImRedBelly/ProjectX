using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCube : MonoBehaviour
{
    public Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        rb.AddForce(new Vector2(Random.Range(0, 1), Random.Range(0, 1)));
    }


}
