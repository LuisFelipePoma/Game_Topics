using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

	public static PlayerMovement instance;
	private Rigidbody2D body;
	private Animator animator;
	private float forceX, forceY;
	private float tresholdX = 7f;
	private float tresholdY = 14f;

	private bool setPower, diJump;

	void Awake()
	{
		MakeInstance();
		Initialize();
	}


	void Initialize()
	{
		body = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
	}


	void MakeInstance()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	void SetPower()
	{
		if (setPower)
		{
			forceX += tresholdX * Time.deltaTime;
			forceY += tresholdY * Time.deltaTime;
			forceX = Mathf.Min(forceX, 6.5f);
			forceY = Mathf.Min(forceY, 13.5f);
		}
	}
	public void SetPower(bool setPower)
	{
		this.setPower = setPower;
		if (!setPower)
			Jump();
	}

	void Jump()
	{
		body.velocity = new Vector2(forceX, forceY);
		forceX = forceY = 0f;
		diJump = true;
		animator.SetBool("Jump", diJump);
	}

	void OnTriggerEnter2D(Collider2D target)
	{
		if (diJump)
		{
			if (target.CompareTag("Platform"))
			{
				diJump = false;
				Debug.Log("Jump");
				animator.SetBool("Jump", diJump);
			}
		}
	}

	// Update is called once per frame
	void Update()
	{
		Debug.Log(this.setPower);
		SetPower();
	}
}
