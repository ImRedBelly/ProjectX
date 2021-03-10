using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunInHand : MonoBehaviour
{
    public GameObject shotPosition;
    public GameObject throwableObject;

    Vector3 direction;
    void Update()
    {
        if (PlayerMovement.instance.transform.localScale.x > 0)
        {
            direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            direction.z = 0;
            float angle = Vector2.SignedAngle(PlayerMovement.instance.transform.right, direction);
            if (angle > 60)
            {
                transform.rotation = Quaternion.Euler(0, 0, 60);
                return;
            }

            if (angle < -60)
            {
                transform.rotation = Quaternion.Euler(0, 0, -60);
                return;
            }

            transform.right = direction;
        }
        else
        {
            direction = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            direction.z = 0;
            float angle = Vector2.SignedAngle(PlayerMovement.instance.transform.right, direction);
            if (angle > 60)
            {
                transform.rotation = Quaternion.Euler(0, 0, 60);
                return;
            }

            if (angle < -60)
            {
                transform.rotation = Quaternion.Euler(0, 0, -60);
                return;
            }

            transform.right = direction;
            direction *= -1;
        }



        if (Input.GetMouseButtonDown(2))
        {
            GameObject throwableWeapon = Instantiate(throwableObject, shotPosition.transform.position, Quaternion.identity);
            throwableWeapon.GetComponent<ThrowableWeapon>().direction = direction.normalized;
            throwableWeapon.name = "ThrowableWeapon";
        }
    }
    void LookGun()
    {
        direction.z = 0;
        float angle = Vector2.SignedAngle(PlayerMovement.instance.transform.right, direction);
        if (angle > 60)
        {
            transform.rotation = Quaternion.Euler(0, 0, 60);
            return;
        }

        if (angle < -60)
        {
            transform.rotation = Quaternion.Euler(0, 0, -60);
            return;
        }
    }
}
