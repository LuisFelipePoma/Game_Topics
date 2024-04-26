using UnityEngine;

public class CeilCollider : MonoBehaviour
{
	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Needle")
		{
			Destroy(other.gameObject)
		}
	}
}