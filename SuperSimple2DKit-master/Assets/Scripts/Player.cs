using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxSpeed;

    Rigidbody2D myRB;
    Animator myAnim;
    bool isGrounded = false;
    bool facingRight;

    public ParticleSystem dust;

    [SerializeField]
    private float jumpVelocity = 20f;

    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        facingRight = true;
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        float move = Input.GetAxis("Horizontal");

        myAnim.SetFloat("speed", Mathf.Abs(move));
        myRB.velocity = new Vector2(move * maxSpeed, myRB.velocity.y);

        if (move > 0 && !facingRight)
        {
            Flip();
        }
        else if (move < 0 && facingRight)
        {
            Flip();
        }

        if (move != 0 && isGrounded == true)
        {
            dust.Play();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

        if (isGrounded == true)
        {
            dust.Play();
        }
    }

    void Jump()
    {
        if (isGrounded == true)
        {
            myRB.AddForce(transform.up * jumpVelocity * 100f, ForceMode2D.Force);
            myAnim.SetBool("IsJumping", true);
            dust.Play();
        }
    }

    public void Grounded()
    {
        isGrounded = true;
        dust.Play();
        Debug.Log("I am grounded");
    }

    public void Airborne()
    {
        isGrounded = false;
        myAnim.SetBool("IsJumping", false);
        Debug.Log("I am airborne");
    }
}