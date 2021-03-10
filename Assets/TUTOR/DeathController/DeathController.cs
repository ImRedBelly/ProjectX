using UnityEngine;

public class DeathController : MonoBehaviour
{
    public static DeathController deathController;
    public GameObject deathCopy;

    public int deathCount;

    public float PositionX;
    public float PositionY;

    public static string positionPlayerX = "PositionPlayerX";
    public static string positionPlayerY = "PositionPlayerY";
    public static string count = "DeatchCount";

    private void Awake()
    {
        //Reset();
    }

    void Start()
    {
        if (deathController != null)
            Destroy(gameObject);
        else
            deathController = this;

        if (PlayerPrefs.GetInt(count) > 0)
            Instantiate(deathCopy, new Vector2(PlayerPrefs.GetFloat(positionPlayerX), PlayerPrefs.GetFloat(positionPlayerY)), Quaternion.identity);


        DontDestroyOnLoad(gameObject);
    }

    public void SavePosition()
    {
        deathCount++;

        PlayerPrefs.SetFloat(positionPlayerX, PositionX = PlayerMovement.instance.transform.position.x);
        PlayerPrefs.SetFloat(positionPlayerY, PositionY = PlayerMovement.instance.transform.position.y);
        PlayerPrefs.SetInt(count, deathCount);
    }
    private void Reset()
    {
        PlayerPrefs.DeleteAll();
    }
}
