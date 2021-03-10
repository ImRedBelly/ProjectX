using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class СollectMoney : MonoBehaviour
{
    public int moneyRemaining = 4;
    public int moneyCurrent = 0;

    public float timeRemaining = 10f;
    public bool isTimerStart;

    private void Start()
    {

    }

    void Update()
    {
        if (isTimerStart)
        {
            if (timeRemaining <= 0)
            {
                isTimerStart = false;
                CheckLoosing();
                return;
            }

            timeRemaining -= Time.deltaTime;
            float minutes = Mathf.FloorToInt(timeRemaining / 60);
            float seconds = Mathf.FloorToInt(timeRemaining % 60);
            //print(string.Format("{0:00}:{1:00}", minutes, seconds));

        }
    }

    public void CheckLoosing()
    {
        if (moneyCurrent < moneyRemaining)
            Loosing();
        
    }

    public void CheckWin()
    {
        if (moneyCurrent >= moneyRemaining && isTimerStart)
            Win();
        
    }


    public void SetMoney(int money)
    {
        moneyCurrent = money;
    }

    public void AddMoney()
    {
        isTimerStart = true;
        moneyCurrent++;
        CheckWin();
    }

    private void Loosing()
    {
        SetMoney(0);
        isTimerStart = true;

        //print("не успел");
    }

    private void Win()
    {
        isTimerStart = false;
        //print("вовремя собрал!");
        //SceneManager.LoadScene(1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Money"))
        {
            AddMoney();
            Destroy(collision.gameObject);
        }
    }



}
