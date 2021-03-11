using UnityEngine;

public class DeathController : MonoBehaviour
{
    public static DeathController deathController;

    public GameObject deathCopy;

    void Start()
    {
        if (deathController != null)
            Destroy(gameObject);
        else
            deathController = this;

        if (GameManagerTutor.instance.countDeath > 0)
            Instantiate(deathCopy, new Vector2(GameManagerTutor.instance.PositionX, GameManagerTutor.instance.PositionY), Quaternion.identity);
    }
}
