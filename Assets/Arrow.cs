using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    void Update()
    {
        //    Vector2 posotion = transform.position;
        //    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //    Vector2 direction = mousePosition - posotion;
        //    transform.right = direction;

        Vector3 direction = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
       
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

    }
}


