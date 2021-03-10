using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerMoney : MonoBehaviour
{
    public int moneyCount;

    public GameObject money;
    public GameObject rock;
    public GameObject enemy;

    
    public Transform positionOne;
    public Transform positionTwo;

    void Start()
    {
        StartCoroutine(SpawnMoney());
        
        StartCoroutine(SpawnerEnemyOne());
        StartCoroutine(SpawnerEnemyTwo());
    }

    IEnumerator SpawnMoney()
    {
        yield return new WaitForSeconds(1);


        while (true)
        {
            Instantiate(money, new Vector2(Random.Range(0, 40), 20), transform.rotation);
            yield return new WaitForSeconds(2);
        }
    }

    public void StartRock()
    {
        StartCoroutine(SpawnerRock());
    }
    public IEnumerator SpawnerRock()
    {
        yield return new WaitForSeconds(1);

        while (true)
        {
            Instantiate(rock, new Vector2(Random.Range(0, 40), 20), transform.rotation);
            yield return new WaitForSeconds(5);
        }
    }
    
    IEnumerator SpawnerEnemyOne()
    {
        yield return new WaitForSeconds(15);

        while (true)
        {
            Instantiate(enemy, positionOne.transform.position, transform.rotation);
            yield return new WaitForSeconds(15);
        }
    }
    IEnumerator SpawnerEnemyTwo()
    {
        yield return new WaitForSeconds(15);

        while (true)
        {
            Instantiate(enemy, positionTwo.transform.position, transform.rotation);
            yield return new WaitForSeconds(15);
        }
    }
}
