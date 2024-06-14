using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{

	[SerializeField]
	private AudioSource audioSource;
	public float health = 70f;


	void OnCollisionEnter2D(Collision2D target)
	{
		Rigidbody2D body = target.gameObject.GetComponent<Rigidbody2D>();
		if (body == null) return;
		float damage = body.velocity.magnitude * 10;
		if (damage > 10)
		{
			audioSource.Play();
		}
		health -= damage;
		if (health <= 0)
		{
			Destroy(gameObject);
		}
	}
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}
}
