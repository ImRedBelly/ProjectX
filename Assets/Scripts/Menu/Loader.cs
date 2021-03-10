using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{

    AudioManager audioManager;
    public AudioClip levelTheme;
    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.PlaySound(levelTheme);
    }
    public void LoadLevel(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Resume()
    {
        GameManager.Instance.LoadScene();
    }
}
