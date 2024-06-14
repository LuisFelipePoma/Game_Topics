using System.Collections;
using UnityEngine;

public class Bird : MonoBehaviour
{
	public BirdState birdState { set; get; }

	[SerializeField]
	private TrailRenderer lineRenderer;
	[SerializeField]
	private new Rigidbody2D rigidbody2D;
	[SerializeField]
	private new CircleCollider2D collider2D;
	[SerializeField]
	private AudioSource audioSource;

	void Awake()
	{
		lineRenderer.enabled = false;
		lineRenderer.sortingLayerName = "Foreground";
		rigidbody2D.isKinematic = true;
		collider2D.radius = GameVariables.birdColliderRadiusNormal;
		birdState = BirdState.BeforeLanzar;
	}

	void FixedUpdate(){
		if(birdState == BirdState.Lanzar && rigidbody2D.velocity.sqrMagnitude <=
			GameVariables.minVelocity
		){
			StartCoroutine(DestroyBefore(2f));
		}
	}

	public void OnLanzar()
	{
		audioSource.Play();
		lineRenderer.enabled = true;
		rigidbody2D.isKinematic = false;
		collider2D.radius = GameVariables.birdColliderRadiusBig;
		birdState = BirdState.Lanzar;
	}

	IEnumerator DestroyBefore(float delay)
	{
		yield return new WaitForSeconds(delay);
		Destroy(gameObject);
	}
}
