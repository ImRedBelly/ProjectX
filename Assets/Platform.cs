using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public int Y;
    void Update()
    {
        transform.position = new Vector3(transform.position.x, -Y + Mathf.Sin(Time.fixedTime) * 5f, transform.position.z);
    }
}

