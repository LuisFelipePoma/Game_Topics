using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour
{
	[SerializeField]
	private AudioSource audioSource;

	public float health = 150f;

	public Sprite spriteHerido;

	private float changeSpringHealth;

	private bool isAlive = true;

	// Start is called before the first frame update
	void Start()
	{
		changeSpringHealth = health - 30f;
	}

	IEnumerator DestroyLater(float delay){
		yield return new WaitForSeconds(delay);
		Destroy(gameObject);
	}
	void OnCollisionEnter2D(Collision2D target)
	{

		if(!isAlive){
			return;
		}
		Rigidbody2D body = target.gameObject.GetComponent<Rigidbody2D>();
		if (body == null)
		{
			return;
		}

		if (target.gameObject.CompareTag("Bird"))
		{
			isAlive = false;
			audioSource.Play();
			StartCoroutine(DestroyLater(1.5f));
		}
		else
		{
			float damage = body.velocity.magnitude * 10f;
			if (damage >= 10)
			{
				audioSource.Play();
			}
			health -= damage;
			if (health > changeSpringHealth)
			{
				gameObject.GetComponent<SpriteRenderer>().sprite = spriteHerido;
			}
			if (health <= 0)
			{
				Destroy(gameObject);
			}
		}
	}
}
