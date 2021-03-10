using UnityEngine;

public class Lamp : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            PuzleLevel2 puzleLevel2 = FindObjectOfType<PuzleLevel2>();
            int n = puzleLevel2.lamp.IndexOf(gameObject);
            puzleLevel2.lamp.RemoveAt(n);
            Destroy(gameObject);
        }
    }
}
