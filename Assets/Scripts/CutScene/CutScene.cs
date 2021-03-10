using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutScene : MonoBehaviour
{
    public GameObject text;
    public int loadScene;
    private void Start()
    {
        StartCoroutine(Text());
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene(loadScene);
        }
    }

    IEnumerator Text()
    {
        yield return new WaitForSeconds(20);
        text.SetActive(true);
    }
}
