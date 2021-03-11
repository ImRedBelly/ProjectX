using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillZone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            GameManagerTutor.instance.SavePosition();


            SceneManager.LoadScene(0);
        }
        else
        {
            Destroy(col.gameObject);
        }
    }
}
