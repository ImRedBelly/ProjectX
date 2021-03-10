using UnityEngine;

public class Bullet : MonoBehaviour
{
	public int speed;
	public Rigidbody2D rb;

	void Start()
	{
		Transform shotObject = PlayerMovementT.Instance.shotPosition.transform;
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mousePosition.z = 0;
		Vector3 direction = mousePosition - shotObject.position;

		float angleCheck = Vector3.SignedAngle(direction, shotObject.up, Vector3.forward);

		if (PlayerMovementT.Instance.looksToRight)
		{
			if (angleCheck < 0)
			{
				rb.velocity = Vector2.up * speed;
				return;
			}
			if (angleCheck > 90)
			{
				rb.velocity = Vector2.right * speed;
				return;
			}
			rb.velocity = direction.normalized * speed;
		}
		else
		{
			if (angleCheck > 0)
			{
				rb.velocity = Vector2.up * speed;
				return;
			}
			if (angleCheck < -90)
			{
				rb.velocity = Vector2.left * speed;
				return;
			}
			rb.velocity = direction.normalized * speed;
		}
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision)
		{
			Destroy(gameObject, 0.1f);
		}
	}

}
