using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float jumpPower;

	private float distToGround;
	private Rigidbody2D rb2d;

	void Start()
	{
		distToGround = GetComponent<BoxCollider2D> ().bounds.extents.y;

		rb2d = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxisRaw ("Horizontal");

		Vector2 movement = new Vector2 (moveHorizontal, 0f);
		movement.Normalize ();
		movement = movement * speed * Time.deltaTime;

		rb2d.AddForce (movement);

		if (IsGrounded() && Input.GetAxisRaw ("Vertical") != 0) 
		{
			Debug.Log ("Jump!");

			Vector2 jump = new Vector2 (0f, 1f);
			jump = jump * jumpPower;
			rb2d.AddForce (jump);
		}
	}
		
	bool IsGrounded()
	{
		return Physics2D.Raycast((Vector2)transform.position, -Vector2.up, distToGround + 0.1f);
	}
}
