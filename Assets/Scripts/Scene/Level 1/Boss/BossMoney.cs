using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMoney : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 5);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SpawnerMoney spawnerMoney = FindObjectOfType<SpawnerMoney>();
            spawnerMoney.moneyCount++;
            Destroy(gameObject);
        }
    }
}
