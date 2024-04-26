using UnityEngine;

public class GameManager : MonoBehaviour
{

	[SerializeField] private GameObject player;
	[SerializeField] private GameObject platform;

	private float minX = -2.5f, maxX = 2.5f, minY = -4.7f, maxY = -3.7f;

	public static GameManager instance;


	void MakeInstance()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	void CreateInitialPlatform()
	{
		Vector3 temp = new Vector3(Random.Range(minX, minX + 1.2f),
		 Random.Range(minY, maxY), 0);
		Instantiate(platform, temp, Quaternion.identity);
		temp.y += 2f;
		Instantiate(player, temp, Quaternion.identity);

		temp = new Vector3(Random.Range(maxX, maxX - 1.2f),
		 Random.Range(minY, maxY), 0);
		Instantiate(platform, temp, Quaternion.identity);

	}
	void Awake()
	{
		MakeInstance();
		CreateInitialPlatform();
	}
}
