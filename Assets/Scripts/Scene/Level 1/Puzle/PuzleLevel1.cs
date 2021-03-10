using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzleLevel1 : MonoBehaviour
{
    public AudioClip levelTheme;
    public List<GameObject> money;
    public GameObject door;
    public bool isWin = false;

    public Text moneyText;
    public AudioSource audioSource;
    private void Start()
    {
        audioSource.PlayOneShot(levelTheme);
    }
    void Update()
    {
       moneyText.text = "" + money.Count;

        if (money.Count == 0)
        {
            isWin = true;
        }
        if (isWin)
        {
            Destroy(door.gameObject);
        }
    }
}
