using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flogmovement : MonoBehaviour
{

    public float Charc_Speed;
    public Transform groundCheck; //to check if player is touching ground
    public float jumpForce = 100f; //jump height
    public LayerMask whatIsGround;

    bool facingRight = true; //shifting image left & right
    bool grounded = false; // for detecting ground
    bool onLadder = false; //for ladder

    private float climbSpeed = 0.6f; //speed when climbing up the ladder
    float groundRadius = 0.01f; //distance from player to ground (when grounded)

    Animator anim;
    private Rigidbody2D rgbd;

    // Use this for initialization
    void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (grounded && Input.GetButtonDown("Jump"))
        {
            anim.SetBool("jump", true);
            rgbd.AddForce(new Vector2(0, jumpForce));
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        anim.SetBool("jump", !grounded);
        anim.SetFloat("vSpeed", rgbd.velocity.y); //how fast we are moving up n down

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        rgbd.velocity = new Vector2(horizontal * Charc_Speed, rgbd.velocity.y);

        anim.SetFloat("walk", Mathf.Abs(horizontal));
        anim.SetFloat("climb", Mathf.Abs(vertical));

        if (horizontal > 0 && !facingRight)
            Flip();
        else if (horizontal < 0 && facingRight)
            Flip();

        if (onLadder)
        {
            rgbd.velocity = new Vector2(0, vertical * climbSpeed);
            rgbd.gravityScale = 0;
            if (Input.GetButton("Horizontal") && Input.GetButtonDown("Jump"))
            {
                onLadder = false;
                anim.SetBool("ladder", false);
                rgbd.gravityScale = 0.9f;
            }
        }
    }

    void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;

        theScale.x *= -1;

        transform.localScale = theScale;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Ladder" && Input.GetButton("Vertical"))
        {
            onLadder = true;
            anim.SetBool("ladder", true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Ladder")
        {
            onLadder = false;
            anim.SetBool("ladder", false);
            rgbd.gravityScale = 0.9f;
        }
    }
}
