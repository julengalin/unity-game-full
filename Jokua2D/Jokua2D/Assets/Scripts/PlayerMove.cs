using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

	public float runSpeed=2;

	public float jumpSpeed = 3;

	public float doubleJumpSpeed = 2.5f;

	private bool canDoubleJump;

	Rigidbody2D rb2D;


	public bool betterJump = false;

	public float fallMultiplier = 0.5f;

	public float lowJumpMultiplier = 1f;

	public SpriteRenderer spriteRenderer;

	public Animator animator;

	public GameObject dustLeft;
	public GameObject dustRight;

	public float dashCooldown;

	public float dashForce=30;

	public GameObject dashParticle;


	bool isTouchingFront = false;
	bool wallSliding;

	public float wallSlidingSpeed = 0.75f;

	bool isTouchingDerecha;
	bool isTouchingIzquierda;

	void Start()
    {
		rb2D = GetComponent<Rigidbody2D>();
    }

	private void Update()
	{
		dashCooldown -= Time.deltaTime;

		if (Input.GetKey("up") && wallSliding==false)
		{
			if (CheckGround.isGrounded)
			{
				canDoubleJump = true;
				rb2D.velocity = new Vector2(rb2D.velocity.x, jumpSpeed);
			}
			else
			{
				if (Input.GetKeyDown("up"))
				{
					if (canDoubleJump)
					{
						animator.SetBool("DoubleJump", true);
						rb2D.velocity = new Vector2(rb2D.velocity.x, doubleJumpSpeed);
						canDoubleJump = false;

					}
				}
			}
		}

		if (CheckGround.isGrounded == false)
		{
			animator.SetBool("Jump", true);
			animator.SetBool("Run", false);
			dustLeft.SetActive(false);
			dustRight.SetActive(false);
		}
		if (CheckGround.isGrounded == true)
		{
			animator.SetBool("Jump", false);
			animator.SetBool("DoubleJump", false);
			animator.SetBool("Falling", false);
		}

		if (rb2D.velocity.y<0)
		{
			animator.SetBool("Falling", true);
		}
		else if (rb2D.velocity.y > 0)
		{
			animator.SetBool("Falling", false);
		}


        if (isTouchingFront==true && CheckGround.isGrounded==false)
        {
			wallSliding = true;
        }
        else
        {
			wallSliding = false;
        }


        if (wallSliding)
        {
			animator.Play("Wall");
			rb2D.velocity = new Vector2(rb2D.velocity.x, Mathf.Clamp(rb2D.velocity.y,-wallSlidingSpeed,float.MaxValue));
        }




	}

	void FixedUpdate()
    {

		if (Input.GetKey("right") && isTouchingDerecha == false)
		{
			rb2D.velocity = new Vector2(runSpeed, rb2D.velocity.y);
			spriteRenderer.flipX = false;
			animator.SetBool("Run", true);

			if (CheckGround.isGrounded == true)
			{
				dustLeft.SetActive(true);
				dustRight.SetActive(false);
			}

			


		}

		

		else if (Input.GetKey("left") && isTouchingIzquierda == false)
		{
			rb2D.velocity = new Vector2(-runSpeed, rb2D.velocity.y);
			spriteRenderer.flipX = true;
			animator.SetBool("Run", true);

			if (CheckGround.isGrounded == true)
			{
				dustLeft.SetActive(false);
				dustRight.SetActive(true);
			}
			
		}

		else
		{
			rb2D.velocity = new Vector2(0, rb2D.velocity.y);
			animator.SetBool("Run", false);

			dustLeft.SetActive(false);
			dustRight.SetActive(false);

		}


		if (betterJump)
		{
			if (rb2D.velocity.y<0)
			{
				rb2D.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier) * Time.deltaTime;
			}

			if (rb2D.velocity.y>0 && !Input.GetKey("up")) 
			{
				rb2D.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier) * Time.deltaTime;
			}


		}

    }

	

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ParedDerecha"))
        {
			isTouchingFront = true;
			isTouchingDerecha = true;
        }

		if (collision.gameObject.CompareTag("ParedIzquierda"))
		{
			isTouchingFront = true;
			isTouchingIzquierda = true;
		}
	}

    private void OnCollisionExit2D(Collision2D collision)
    {
		isTouchingFront = false;
		isTouchingDerecha = false;
		isTouchingIzquierda = false;
    }
}
