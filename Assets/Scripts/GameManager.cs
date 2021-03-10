using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool pauseActiv;

    public static GameManager Instans;
    public static string keySaveScene = "saveScene";
    public int scene;

    public GameObject tutor;


    void Awake()
    {
        Instance = this;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Pause();
        }

        if(tutor != null)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                tutor.gameObject.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                tutor.gameObject.SetActive(false);
                Time.timeScale = 1f;
            }
        }
        
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);

        //if(Instance != null)
        //{
        //    Destroy(gameObject);
        //}
        print(scene);
    }
    public void Pause()
    {
        if (pauseActiv)
        {
            Time.timeScale = 1f;
            pauseActiv = false;
        }
        else
        {
            Time.timeScale = 0f;
            pauseActiv = true;
        }
    }

    public void LoadScene()
    {
        
        SceneManager.LoadScene(PlayerPrefs.GetInt(keySaveScene, scene));
    }
    public void SaveScene()
    {
        PlayerPrefs.SetInt(keySaveScene, scene);
        scene = SceneManager.GetActiveScene().buildIndex + 1;
    }
}
