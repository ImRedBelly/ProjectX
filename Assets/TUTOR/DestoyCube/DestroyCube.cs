using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCube : MonoBehaviour
{
    public GameObject particleCube;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Destroy()
    {
        Instantiate(particleCube, transform.position, Quaternion.identity);
        Destroy(gameObject, 0.1f);
    }
}
