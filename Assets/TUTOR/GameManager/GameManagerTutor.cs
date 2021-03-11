﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerTutor : MonoBehaviour
{
    public static GameManagerTutor instance;

    public int countDeath;
    public int countMoney;

    public float PositionX;
    public float PositionY;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void SavePosition()
    {
        PlayerMovement.instance.GetComponent<Purse>().GiveMoney();

        countDeath++;

        PositionX = PlayerMovement.instance.transform.position.x;
        PositionY = PlayerMovement.instance.transform.position.y;
    }
    public void GiveMoney(int money)
    {
        countMoney += money;
    }

}
