using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float maxSpeed;
	public float jumpPower;

	private Rigidbody2D rb2d;

	public Transform groundCheck;
	float groundRadius = 0.1f;
	public LayerMask whatIsGround;

	void Start()
	{
		rb2d = GetComponent<Rigidbody2D> ();
	}

	void Update()
	{
		if (IsGrounded() && Input.GetButtonDown("Jump")) 
		{
			rb2d.AddForce (new Vector2 (0f, jumpPower));
		}
	}

	void FixedUpdate()
	{
		rb2d.velocity = new Vector2(Input.GetAxisRaw ("Horizontal") * maxSpeed, rb2d.velocity.y);
	}
		
	bool IsGrounded()
	{
		return Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
	}
}
