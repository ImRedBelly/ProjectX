﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public TrajectoryRenderer trajectoryRenderer;
    public GameObject grenadePrefab;
    public float power = 50;



    void Update()
    {
        float enter;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        new Plane(-Vector3.forward, transform.position).Raycast(ray, out enter);
        Vector3 mouseInWorld = ray.GetPoint(enter);

        Vector3 speed = (mouseInWorld - transform.position);

        if (Input.GetKey(KeyCode.G))
        {
            trajectoryRenderer.gameObject.SetActive(true);
            trajectoryRenderer.ShowTrajectory(transform.position, -speed);
        }


        if (Input.GetKeyDown(KeyCode.G))
        {

        }

        if (Input.GetKeyUp(KeyCode.G))
        {
            trajectoryRenderer.gameObject.SetActive(false);
            Rigidbody2D grenade = Instantiate(grenadePrefab, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
            grenade.AddForce(-speed, ForceMode2D.Impulse);
            StartCoroutine(IsTrigger(grenade.gameObject));
        }
    }

    IEnumerator IsTrigger(GameObject grenade)
    {
        grenade.GetComponent<CircleCollider2D>().isTrigger = true;
        yield return new WaitForSeconds(0.1f);
        grenade.GetComponent<CircleCollider2D>().isTrigger = false;
    }
}
