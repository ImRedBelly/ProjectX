using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOne : MonoBehaviour
{
    public Animator animator;
    public AudioManager audioManager;


    public AudioClip attack;

    public void Attack()
    {
        audioManager.PlaySound(attack);
        animator.SetTrigger("Attack");   
    }

}
