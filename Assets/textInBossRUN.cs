using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textInBossRUN : MonoBehaviour
{
    PuzleLevel1 puzleLevel1;
    public Text text;
    private void Start()
    {
        puzleLevel1 = FindObjectOfType<PuzleLevel1>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (puzleLevel1.money.Count > 1)
            {
                text.gameObject.SetActive(true);
            }


        }
        else
        {
            text.gameObject.SetActive(false);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            text.gameObject.SetActive(false);


        }
    }
}
