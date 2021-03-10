using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Glowworm : MonoBehaviour
{
	public float xPos;
	public float yPos;
	public float t;
	public float deltaT;
	Vector3 offset;

	[Header("Random movement")]
	[Space]
	public bool randomPosition;
	public bool isMovingUp;

	public float speed;
	public float speedUp;
	public float radiusFly;

	private Vector3 direction;
	private Vector3 startPosition;


	Rigidbody2D rb;


	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}


	private void Start()
	{
		offset = transform.position;

		if (randomPosition)
		{
			startPosition = transform.position;
			direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
			rb.AddForce(direction * speed);
		}

	}
	void Update()
	{
		if (randomPosition)
		{
			MoveRandom();
			return;
		}
		Move();
	}

	private void MoveRandom()
	{
		direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

		if (isMovingUp)
		{
			startPosition += Vector3.up * Time.deltaTime * speedUp;
		}

		if (Vector2.Distance(startPosition, transform.position) > radiusFly)
		{
			direction = startPosition - transform.position;
		}
		
		rb.AddForce(direction * speed);
	}

	void Move()
	{
		t += deltaT;
		xPos = Mathf.Cos(t);
		yPos = Mathf.Sin(2 * t) / 2;
		transform.position = new Vector3(xPos, yPos, 0.0f) + offset;
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			HealthPlayer1 healthPlayer = FindObjectOfType<HealthPlayer1>();
			healthPlayer.PlusIntensity();
			Destroy(gameObject);
		}
	}

}

