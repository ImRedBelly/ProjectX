using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public CharacterController2D controller2D;
    public Animator animator;

    void Update()
    {
        if (controller2D.isWallSliding)
        {
            animator.SetBool("IsWallSliding", true);
            //transform.localPosition = new Vector3(transform.localPosition.x * -1, transform.localPosition.y, 0);
        }
        else
        {
            animator.SetBool("IsWallSliding", false);
            //transform.localPosition = new Vector3(transform.localPosition.x * -1, transform.localPosition.y, 0);
        }

    }
}
