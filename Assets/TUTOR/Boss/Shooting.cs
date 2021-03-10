using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Shooting : MonoBehaviour
{
    [Header("SwitchShot")]
    bool switchShot;
    public GameObject pointShotOne;
    public GameObject pointShotTwo;


    [Header("ShootPoint")]
    public bool shotObject;
    public GameObject bullet;
    public GameObject[] pointsShot;
    float nextAttack;

    void Update()
    {
        if (shotObject)
        {
            StartCoroutine(ShootOne());
        }
        else
        {
            SwichShot();
        }

    }

    private void SwichShot()
    {
        float attack = 0.7f;
        if (nextAttack <= 0)
        {
            switchShot = !switchShot;
            nextAttack = attack;
        }
        if (nextAttack > 0)
        {
            nextAttack -= Time.deltaTime;
        }

        if (switchShot)
        {
            pointShotOne.SetActive(true);
            pointShotTwo.SetActive(false);
        }
        else
        {
            pointShotOne.SetActive(false);
            pointShotTwo.SetActive(true);
        }


    }

    IEnumerator ShootOne()
    {
        yield return new WaitForSeconds(0.5f);
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            for (int i = 0; i < pointsShot.Length; i++)
            {
                GameObject throwable = Instantiate(bullet, pointsShot[i].transform.position, Quaternion.identity);
                throwable.GetComponent<ThrowableProjectile>().direction = pointsShot[i].transform.up;
            }
            StopAllCoroutines();
        }
    }



}
//void ShootOne()
//{
//    if (nextAttack <= 0)
//    {
//        for (int i = 0; i < pointsShot.Length; i++)
//        {
//            GameObject throwable = Instantiate(bullet, pointsShot[i].transform.position, Quaternion.identity);
//            throwable.GetComponent<ThrowableProjectile>().direction = pointsShot[i].transform.up;
//        }
//        nextAttack = attackTime;
//    }

//    if (nextAttack > 0)
//    {
//        nextAttack -= Time.deltaTime;
//    }

//}



