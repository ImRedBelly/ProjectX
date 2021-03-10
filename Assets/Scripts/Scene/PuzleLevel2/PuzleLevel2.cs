using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzleLevel2 : MonoBehaviour
{
    public List<GameObject> lamp;
    public GameObject Loader;
    public bool isWin = false;

    void Update()
    {
        
        if (lamp.Count == 0)
        {
            isWin = true;
        }
        if (isWin)
        {
            Loader.SetActive(true);
        }
    }
   
}
