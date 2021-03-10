using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spider : MonoBehaviour
{

	public GameObject bulletPrefab;


	public int health = 100;
	private bool isLive;

	public Transform positionSpawn;

	private float magnitudeX = 10f;
	Vector3 startPosition;
	IEnumerator cor;
	Animator animator;


	private void Awake()
	{
		animator = GetComponent<Animator>();
	}


	void Start()
	{
		isLive = true;
		startPosition = transform.position;
		cor = Shoot();
		StartCoroutine(cor);
	}

	void Update()
	{
		if (!isLive || PlayerMovementT.Instance == null)
		{
			return;
		}
	}

	public IEnumerator Shoot()
	{
		while (PlayerMovementT.Instance != null)
		{
			Instantiate(bulletPrefab, positionSpawn.position, Quaternion.identity);
			yield return new WaitForSeconds(1);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("EnemyBullet"))
		{
			Destroy(collision.gameObject);

			health--;

			CheckHealth();
		}
	}

	private void CheckHealth()
	{
		print($"здоровье паука: {health}");

		if (health <= 0)
		{
			isLive = false;
			StopCoroutine(cor);
			animator.SetBool("isLive", false); //TODO добавить анимацию
			SceneManager.LoadScene(9);
		}
	}
}



