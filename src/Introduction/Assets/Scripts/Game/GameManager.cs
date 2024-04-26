using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	// Start is called before the first frame update

	public static GameManager instance;
	private float elapsed = 0f;

	[SerializeField]
	private GameObject objScore;
	private Text txtScore;
	private int score = 0;

	void Start()
	{
		txtScore = objScore.GetComponent<Text>();
	}
	void Awake()
	{
		if (instance = null)
		{
			instance = this;
		}
	}

	// Update is called once per frame
	void Update()
	{
		elapsed += Time.deltaTime;
		if (elapsed >= 5f)
		{
			elapsed = 0f;
			// Do something every second
		}
	}

	void UpdateScore()
	{

	}
}
