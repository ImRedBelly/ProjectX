using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBoss : MonoBehaviour
{
    public Transform boss;
    public float timer;
    void Update()
    {
        if (timer <= 0)
        {
            timer = 5;
            gameObject.SetActive(false);
        }
        else
        {
            timer -= Time.deltaTime;

            Vector2 position = boss.position;
            if (boss.localScale.x == 2)
                position.x += 2;
            else
                position.x -= 2;
            transform.position = position;
        }
    }
}
