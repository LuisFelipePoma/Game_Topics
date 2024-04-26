using UnityEngine;

public class CircleRotate : MonoBehaviour
{
	private bool canRotate = false;
	private float rotationSpeed;
	private float angle;
	

	void Awake()
	{
		canRotate = true;
	}

	// Update is called once per frame
	void Update()
	{
		if (canRotate)
		{

			RotateCircle();
		}
	}

	void RotateCircle()
	{
		// angle = transform.rotation.eulerAngles.z;
		angle += rotationSpeed + Time.deltaTime;
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
	}
}
