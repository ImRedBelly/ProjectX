using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int torchCount;
    public GameObject portal;

    public void CreateTorch()
    {
        torchCount++;
    }
    public void Light()
    {
        torchCount--;
        if (torchCount <= 0)
        {
            portal.SetActive(true);
        }
    }

}
