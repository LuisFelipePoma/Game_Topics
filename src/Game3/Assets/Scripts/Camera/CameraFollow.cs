using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

	[HideInInspector]
	public Vector3 startingPosition;

	private float minCamerax = 0, maxCameraX = 14f;

	[HideInInspector]
	public bool isFollowing;

	[HideInInspector]
	public Transform BirdToFollow;
	// Start is called before the first frame update
	void Awake()
	{
		startingPosition = transform.position;
	}

	// Update is called once per frame
	void Update()
	{
		if (isFollowing)
		{
			if (BirdToFollow != null)
			{
				var birPosition = BirdToFollow.position;
				float x = Mathf.Clamp(birPosition.x, minCamerax, maxCameraX);
				transform.position = new Vector3(
					x, startingPosition.y, startingPosition.z
				);
			}else{
				isFollowing = false;
			}
		}

	}
}
