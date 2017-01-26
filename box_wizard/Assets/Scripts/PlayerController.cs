using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float maxSpeed;
	public float jumpPower;
	private bool facingRight = false;

	private Rigidbody2D rb2d;

	public Transform groundCheck;
	float groundRadius = 0.1f;
	public LayerMask whatIsGround;
	public LayerMask whatIsBox;

	public GameObject boxPrefab;

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
			
		if (Input.GetButtonDown ("Delete")) 
		{
			Vector2 boxCheckLocation = new Vector2 (transform.position.x + BoxOffset (), transform.position.y);
			Collider2D foundBox = Physics2D.OverlapCircle(boxCheckLocation, 1f, whatIsBox);
			if (foundBox != null) 
			{
				Destroy (foundBox.gameObject);
				return;
			}
			
		}

		if (Input.GetButtonDown("Box")) 
		{
			Vector2 boxCheckLocation = new Vector2 (transform.position.x + BoxOffset (), transform.position.y);
			Collider2D foundBox = Physics2D.OverlapCircle(boxCheckLocation, 1f, whatIsBox);
			if (foundBox == null)
				Instantiate (
					boxPrefab, 
					new Vector3 (
						transform.position.x + BoxOffset(), 
						transform.position.y - 0.1f), 
					Quaternion.identity);
		}
	}

	void FixedUpdate()
	{
		float move = Input.GetAxisRaw ("Horizontal");

		rb2d.velocity = new Vector2(move * maxSpeed, rb2d.velocity.y);

		if (move > 0 && !facingRight)
			Flip ();
		else if (move < 0 && facingRight)
			Flip ();
	}
		
	bool IsGrounded()
	{
		return Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
	}

	void Flip()
	{
		facingRight = !facingRight;
		Vector3 myScale = transform.localScale;
		myScale.x *= -1;
		transform.localScale = myScale;
	}

	float BoxOffset()
	{
		float boxSpawnOffset = 0f;
		if (facingRight)
			boxSpawnOffset = 1.7f;
		else if (!facingRight)
			boxSpawnOffset = -1.7f;
		return boxSpawnOffset;
	}
}
