using UnityEngine;

public class NeedleMovement : MonoBehaviour
{
	[SerializeField]
	private GameObject needle;
	private bool canShootCircle;
	private bool canFireNeedle;
	private bool touchedCircle = false;
	private float speed = 10f;
	public Rigidbody2D body;

	void Initialize()
	{
		//body = needle.GetComponent<Rigidbody2D>();
		needle.SetActive(false);
		//GameManager.instance.ShootNeedle();
	}
	void Awake()
	{
		Initialize();
		FireNeedle();
	}
	// Update is called once per frame
	void Update()
	{
		if (canFireNeedle)
		{
			body.velocity = new Vector2(0, speed);
		}
	}

	public void FireNeedle()
	{
		needle.SetActive(true);
		body.isKinematic = false;
		canFireNeedle = true;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log(other.gameObject.name);
		if (touchedCircle)
			return;

		if (other.gameObject.name == "Circle")
		{
			canFireNeedle = false;
			touchedCircle = true;
			body.isKinematic = true;
		}
	}
}
