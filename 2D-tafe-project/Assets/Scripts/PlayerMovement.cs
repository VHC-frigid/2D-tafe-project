using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private Animator anim;
    private SpriteRenderer sprite;

    [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 7f;

    private enum MovementState { idle, running, jumping, falling }

    private bool playerOrientationLeft = true;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxis("Horizontal");
        if (Input.GetAxis("Horizontal") != 0)
        {
            // NOTE: Update so that pressing in direction moving doesnt cap speed
            // if player is moving within normal movement speeds then allow move as normal
            if(Mathf.Abs(rb.velocity.x) <= moveSpeed) //do I add * dirX?
            {
                rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
            } else
            {
                // if input is in same direction as player velocity
                if (Mathf.Abs(rb.velocity.x) < Mathf.Abs(rb.velocity.x + dirX))
                {
                    rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
                }
                // If input is in opposite direction as player velocity
                if (Mathf.Abs(rb.velocity.x) > Mathf.Abs(dirX * moveSpeed))
                {
                    // If input direction is in the opposite direction as player velocity
                    // (so if player is pressing the opposite direction theyre moving in)
                    if (Mathf.Abs(rb.velocity.x) < Mathf.Abs(rb.velocity.x + dirX))
                    {
                        rb.velocity = new Vector2(rb.velocity.x + (dirX * moveSpeed * Time.deltaTime / 10), rb.velocity.y);
                    }

                }
            }
            
        }

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        UpdateAnimationState();

        // Set player orientation bool
        if (Input.GetAxis("Horizontal") > 0) playerOrientationLeft = false;
        if (Input.GetAxis("Horizontal") < 0) playerOrientationLeft = true;
        
   }
   
   private void UpdateAnimationState()
   {
        MovementState state;
        
        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
        
   }

   private  bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
    public bool PlayerOrientationLeft()
    {
        return playerOrientationLeft;
    }

 
}
