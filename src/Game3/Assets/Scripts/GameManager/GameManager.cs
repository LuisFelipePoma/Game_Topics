using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public CameraFollow cameraFollow;

	int currentBirdIndex;
	public Honda honda;
	public static GameState gameState;

	public List<GameObject> bricks;

	public List<GameObject> birds;

	public List<GameObject> pigs;

	void Awake()
	{
		gameState = GameState.Start;
		honda.enabled = false;
		bricks = new List<GameObject>(GameObject.FindGameObjectsWithTag("Brick"));
		birds = new List<GameObject>(GameObject.FindGameObjectsWithTag("Bird"));
		pigs = new List<GameObject>(GameObject.FindGameObjectsWithTag("Pig"));
	}

	void AnimateBirdToHonda()
	{
		gameState = GameState.BirdMovingToHonda;
		GameObject bird = birds[currentBirdIndex];
		float distance = Vector2.Distance(bird.transform.position / 10, honda.transform.position) / 10;
		bird.transform.DOMove(honda.birdPosition.position, distance)
		.OnComplete(onMoveComplete);
	}

	void onMoveComplete()
	{
		honda.enabled = true;
		gameState = GameState.Playing;
		honda.birdToLanzar = birds[currentBirdIndex];
	}

	// Update is called once per frame
	void Update()
	{
		switch (gameState)
		{
			case GameState.Start:
				if (Input.GetMouseButtonUp(0))
				{
					AnimateBirdToHonda();
				}
				break;
			case GameState.Playing:
				if (honda.hondaState == HondaState.BirdFlying
				&& (BricksBirdPigsStoppedMoving() || Time.time - honda.timeSinceLanzar > 5f))
				{
					honda.enabled = false;
					AnimateCameraToStartPosition();
					gameState = GameState.BirdMovingToHonda;
				}
				break;
			case GameState.Won:
			case GameState.Lost:
				if (Input.GetMouseButtonDown(0))
				{
					SceneManager.LoadScene(SceneManager.GetActiveScene().name);
				}
				
			break;
		}
	}

	bool AllPigsAreDestroyed()
	{
		return pigs.All(x => x == null);
	}

	bool BricksBirdPigsStoppedMoving()
	{
		foreach (var item in bricks.Union(birds).Union(pigs))
		{
			if (item != null && item.GetComponent<Rigidbody2D>().velocity.sqrMagnitude > GameVariables.minVelocity)
				return false;
		}
		return true;
	}

	void AnimateCameraToStartPosition()
	{
		float duration = Vector2.Distance(cameraFollow.transform.position, Vector3.zero) / 10f;
		if (duration == 0.0f)
		{
			duration = 0.1f;
		}
		Camera.main.transform.DOMove(cameraFollow.startingPosition, duration).OnComplete(onCompleteCamera);
	}

	void onCompleteCamera()
	{
		cameraFollow.isFollowing = false;
		if (AllPigsAreDestroyed())
		{
			gameState = GameState.Won;
		}
		else if (currentBirdIndex == birds.Count - 1)
		{
			gameState = GameState.Lost;
		}
		else
		{
			honda.hondaState = HondaState.Idle;
			currentBirdIndex++;
			AnimateBirdToHonda();
		}
	}

	void OnEnable()
	{
		honda.birdLanzar += HondaBirdLanzar;
	}

	void OnDisable()
	{
		honda.birdLanzar -= HondaBirdLanzar;
	}
	private void HondaBirdLanzar()
	{
		cameraFollow.BirdToFollow = birds[currentBirdIndex].transform;
		cameraFollow.isFollowing = true;
	}
}
