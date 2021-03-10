using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;

public class Chan : MonoBehaviour
{
    public Light2D globight;
    public GameObject pressE;
    public Slider slider;

    public int maxMoney = 10;
    public int allMoney;


    private void Start()
    {
        slider.maxValue = maxMoney;
        slider.value = allMoney;
    }

    private void Update()
    {

        PressE();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            pressE.SetActive(true);
        }
        else
        {
            pressE.SetActive(false);
        }

    }


    void PressE()
    {
        if (Input.GetKey(KeyCode.E))
        {
            SpawnerMoney spawnerMoney = FindObjectOfType<SpawnerMoney>();
            allMoney += spawnerMoney.moneyCount;
            spawnerMoney.moneyCount = 0;

            HealthPlayer1 healthPlayer = FindObjectOfType<HealthPlayer1>();
            healthPlayer.PlusIntensity();

            if (slider.value > maxMoney/2)
            {
                BossOne bossOne = FindObjectOfType<BossOne>();
                bossOne.Attack();


                SpawnerMoney spawner = FindObjectOfType<SpawnerMoney>();
                spawner.StartRock();

            }
            

            PrintMoney();
        }
    }

    IEnumerator Load()
    {
        while (globight.intensity < 10)
        {
            yield return new WaitForSeconds(0.1f);
            globight.intensity += 5 * Time.deltaTime;
        }
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    void PrintMoney()
    {
        slider.value = allMoney;
        if (slider.maxValue < allMoney)
        {
            StartCoroutine(Load());
        }
    }
}
