using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlatform : MonoBehaviour
{
    
    void Update()
    {
        transform.localRotation *= Quaternion.Euler(0, 0, 40 * Time.deltaTime);
    }
}
