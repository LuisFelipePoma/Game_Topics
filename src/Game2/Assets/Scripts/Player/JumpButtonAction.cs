using UnityEngine;
using UnityEngine.EventSystems;

public class JumpButtonAction : MonoBehaviour, IPointerDownHandler,
IPointerUpHandler
{
	public void OnPointerDown(PointerEventData eventData)
	{
		Debug.Log("Button Down");

		if (PlayerMovement.instance != null)
		{
			PlayerMovement.instance.SetPower(true);
		}
	}
	public void OnPointerUp(PointerEventData eventData)
	{
		Debug.Log("Button Up");

		if (PlayerMovement.instance != null)
		{
			PlayerMovement.instance.SetPower(false);
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
