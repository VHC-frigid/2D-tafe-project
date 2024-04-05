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

    [SerializeField] private ParticleSystem particleSystem1, particleSystem2;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 7f;

    private enum MovementState { idle, running, jumping, falling }

    private bool playerOrientationLeft = true;

    private float stunAmount = 0f;
    [SerializeField] private float stunIntensity = 2f; // Number of seconds it takes for player stun to completely ware off

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
        // Conditions for movement
        // If in normal bounds or 
        if (Mathf.Abs(rb.velocity.x) <= moveSpeed || Mathf.Abs(rb.velocity.x) > Mathf.Abs(rb.velocity.x + dirX) && stunIntensity - stunAmount > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x + Mathf.Clamp((dirX * moveSpeed * (Mathf.Clamp((stunIntensity-stunAmount - 0.5f)/(stunIntensity - 0.5f), 0f, stunIntensity))) - rb.velocity.x, -moveSpeed, moveSpeed), rb.velocity.y);
        }

        /*if (Input.GetAxis("Horizontal") != 0)
        {
            // NOTE: Update so that pressing in direction moving doesnt cap speed
            // if player is moving within normal movement speeds then allow move as normal
            if(Mathf.Abs(rb.velocity.x) <= moveSpeed) //do I add * dirX?
            {
                rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y); //normal movement allowed
            } else // else if the player is moving faster than normal bounds... special conditions
            {
                // if input is in same direction as player velocity
                if (Mathf.Abs(rb.velocity.x) < Mathf.Abs(rb.velocity.x + dirX))
                {
                    // !!! removed temporarily to test if we should be able to speed up in air or not !!!
                    //rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
                } else
                // If input is in opposite direction as player velocity
                //if (Mathf.Abs(rb.velocity.x) > Mathf.Abs(dirX * moveSpeed))
                {
                    // If input direction is in the opposite direction as player velocity
                    // (so if player is pressing the opposite direction theyre moving in)
                    if (Mathf.Abs(rb.velocity.x) > Mathf.Abs(rb.velocity.x + dirX))
                    {
                        rb.velocity = new Vector2(rb.velocity.x + (dirX * moveSpeed * Time.deltaTime / 10), rb.velocity.y);
                    }

                }
            }
            
        }*/

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        UpdateAnimationState();

        // Set player orientation bool
        if (Input.GetAxis("Horizontal") > 0) playerOrientationLeft = false;
        if (Input.GetAxis("Horizontal") < 0) playerOrientationLeft = true;

        if (stunAmount > 0)
        {
            stunAmount -= Time.deltaTime;
        }
        if (stunAmount < 0 ) stunAmount = 0;
        Debug.Log("stunAmount: " + stunAmount);
        Debug.Log("stunEFFECT: " + Mathf.Clamp((stunIntensity - stunAmount - 0.5f) / (stunIntensity - 0.5f), 0f, stunIntensity));
        ParticlesOn();
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
        if (!IsGrounded())
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
        
   }

   public bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
    public bool PlayerOrientationLeft()
    {
        return playerOrientationLeft;
    }

    public void StunLock()
    {
        stunAmount = stunIntensity + 0.5f;
    }

    private void ParticlesOn()
    {
        var mainModule1 = particleSystem1.main;
        var mainModule2 = particleSystem2.main;
        Color startColor1 = mainModule1.startColor.color;
        Color startColor2 = mainModule2.startColor.color;

        if (IsGrounded())
        {

            startColor1.a = 1;
            startColor2.a = 1;

        } else
        {
            startColor1.a = 0;
            startColor2.a = 0;
        }
        mainModule1.startColor = startColor1;
        mainModule2.startColor = startColor2;
    }


}
