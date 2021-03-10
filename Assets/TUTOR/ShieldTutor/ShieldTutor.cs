using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldTutor : MonoBehaviour
{
    
    void Update()
    {
        Move();
        Rotate();
    }
    private void Move()
    {
        transform.position = PlayerMovement.instance.transform.position;
    }
    private void Rotate()
    {
        Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        direction.z = 0;

        float angle = Vector2.SignedAngle(PlayerMovement.instance.transform.up, direction);
        if (angle > 90)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
            return;
        }

        if (angle < -90)
        {
            transform.rotation = Quaternion.Euler(0, 0, -90);
            return;
        }
        transform.up = direction;
    }
   
}
