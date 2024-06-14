using UnityEngine;

public class Honda : MonoBehaviour
{
	[HideInInspector]
	public HondaState hondaState;

	public Transform HondaLeft, HondaRight;

	private Vector3 hondaMiddleVector;

	public LineRenderer lineRenderer1, lineRenderer2, trajectoryRenderer;

	[HideInInspector]
	public GameObject birdToLanzar;
	public Transform birdPosition;
	public float lanzarSpeed;
	public float timeSinceLanzar;

	public delegate void BirdLanzar();

	public event BirdLanzar birdLanzar;

	void Awake()
	{
		lineRenderer1.sortingLayerName = "Foreground";
		lineRenderer2.sortingLayerName = "Foreground";
		trajectoryRenderer.sortingLayerName = "Foreground";
		hondaState = HondaState.Idle;
		lineRenderer1.SetPosition(0, HondaLeft.position);
		lineRenderer2.SetPosition(0, HondaRight.position);
		hondaMiddleVector = new Vector3(
			(HondaLeft.position.x + HondaRight.position.x) / 2,
			(HondaLeft.position.y + HondaRight.position.y) / 2,
			0
		);
	}
	// Update is called once per frame
	void Update()
	{
		switch (hondaState)
		{
			case HondaState.Idle:
				InitilizeBird();
				DisplayLineRenderes();
				if (Input.GetMouseButtonDown(0))
				{
					Vector3 location = Camera.main.ScreenToWorldPoint(Input.mousePosition);
					if (birdToLanzar.GetComponent<CircleCollider2D>()
					== Physics2D.OverlapPoint(location))
					{
						hondaState = HondaState.UserPulling;
					}
				}
				break;
			case HondaState.UserPulling:
				DisplayLineRenderes();
				if(Input.GetMouseButton(0)){
					Vector3 location = Camera.main.ScreenToWorldPoint(Input.mousePosition);
					location.z = 0f;
					if(Vector3.Distance(location,hondaMiddleVector) > 1.5f){
						var maxPosition = (location - hondaMiddleVector).normalized * 1.5f + hondaMiddleVector;
						birdToLanzar.transform.position = maxPosition;
					}else{
						birdToLanzar.transform.position = location;
					}
					var distance = Vector3.Distance(hondaMiddleVector, birdToLanzar.transform.position);
					DisplayTrajectoryLineRenderer(distance);
				}else{
					DisplayLineRenderes();
					var distance = Vector3.Distance(hondaMiddleVector, birdToLanzar.transform.position);
					if(distance > 1){
						SetHondaLineRenderesActive(false);
						hondaState = HondaState.BirdFlying;
						LanzarBird(distance);
					}else{
						// birdToLanzar.transform.positionTo()
						InitilizeBird();
					}
				}
			break;
		}
	}

	void InitilizeBird()
	{
		birdToLanzar.transform.position = birdPosition.position;
		hondaState = HondaState.Idle;
		SetHondaLineRenderesActive(true);
	}

	void SetHondaLineRenderesActive(bool active)
	{
		lineRenderer1.enabled = true;
		lineRenderer2.enabled = true;
	}

	void DisplayLineRenderes()
	{
		lineRenderer1.SetPosition(1, birdToLanzar.transform.position);
		lineRenderer2.SetPosition(1, birdToLanzar.transform.position);
	}

	void SetTrajectoryRendererActive(bool active)
	{
		trajectoryRenderer.enabled = active;
	}

	void DisplayTrajectoryLineRenderer(float distance)
	{
		SetTrajectoryRendererActive(true);
		Vector3 v2 = hondaMiddleVector - birdToLanzar.transform.position;
		int segmentCount = 15;
		Vector2[] segments = new Vector2[segmentCount];
		segments[0] = birdToLanzar.transform.position;
		Vector2 segVelocity = new Vector2(v2.x, v2.y) * lanzarSpeed * distance;
		for (int i = 1; i < segmentCount; i++)
		{
			float time = i * Time.fixedDeltaTime;
			segments[i] = segments[0] + segVelocity * time + 0.5f * Physics2D.gravity * Mathf.Pow(time, 2);
		}
		trajectoryRenderer.positionCount = segmentCount;
		for (int i = 0; i < segmentCount; i++)
		{
			trajectoryRenderer.SetPosition(i, segments[i]);
		}
	}

	private void LanzarBird(float distance)
	{
		Vector3 velocity = hondaMiddleVector - birdToLanzar.transform.position;
		Bird bird = birdToLanzar.GetComponent<Bird>();
		Rigidbody2D birdBody = birdToLanzar.GetComponent<Rigidbody2D>();

		bird.OnLanzar();
		birdBody.velocity = new Vector2(velocity.x, velocity.y) * lanzarSpeed;
		if (birdLanzar != null)
		{
			birdLanzar();
		}
	}
}
