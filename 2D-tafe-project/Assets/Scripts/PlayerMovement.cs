using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private Animator anim;
    private SpriteRenderer sprite;
    [SerializeField] private Camera cam;

    [SerializeField] private LayerMask jumpableGround;

    [SerializeField] private ParticleSystem particleSystem1, particleSystem2;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 7f;

    private enum MovementState { idle, running, jumping, falling }

    private bool playerOrientationLeft = true;

    private float stunAmount = 0f;
    private float stunHard = 0.5f;
    [SerializeField] private float stunIntensity = 2f; // Number of seconds it takes for player stun to completely ware off
    private float stunFactor = 1;
    private Vector2 groundVelX;
    private Vector2 jumpVelX;
    private Vector2 cameraLock;
    private bool cameraLockBool = false;


    [SerializeField] private bool _active = true;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    private void Update()
    {
        Debug.Log("Player X speed: " + rb.velocity.x);
        //stunFactor - 0 locks controls to 1 which is free controls
        stunFactor = Mathf.Clamp(Mathf.InverseLerp(stunIntensity, 0f, stunAmount) * (1 + stunHard) - stunHard, 0f, stunIntensity);
        float origVelocity = rb.velocity.x;

        dirX = Input.GetAxis("Horizontal");

        groundVelX = new Vector2(rb.velocity.x + Mathf.Clamp((dirX * moveSpeed * (Mathf.Clamp((stunIntensity - stunAmount - stunHard) / (stunIntensity - stunHard), 0f, stunIntensity))) - rb.velocity.x, -moveSpeed, moveSpeed), rb.velocity.y);
        jumpVelX = new Vector2(rb.velocity.x + (dirX * moveSpeed * Time.deltaTime * 3 * stunFactor), rb.velocity.y);
        // Conditions for movement

        // If not stunned hard
        //if (stunAmount < stunIntensity - stunHard)
        //{
        // If in normal speed bounds        or      player holding opposite direction to movement       (check to see if applying velocity wouldn't push further past bounds)
        if (Mathf.Abs(rb.velocity.x) <= moveSpeed || Mathf.Abs(rb.velocity.x) > Mathf.Abs(rb.velocity.x + dirX))
            {
            if (IsGrounded())
            {
                //if hard stunned
                if (stunAmount > stunIntensity-stunHard)
                {
                    rb.velocity = jumpVelX;
                }
                else
                {
                    rb.velocity = groundVelX;
                }
            }
            if(!IsGrounded())
            {
                if(stunAmount > 0)
                {
                    rb.velocity = jumpVelX;
                }
                else
                {
                    rb.velocity = groundVelX;
                }
            }
            // if on the ground and hard stunned

            // Mathf.Lerp(startNumber, endNumber, interpolationFactor);

            //Debug.Log("Stun Lerp: " + Mathf.Clamp(Mathf.InverseLerp(stunIntensity, 0f, stunAmount) * (1+stunHard) - stunHard, 0f, stunIntensity));

            //                      add to current velocity, Clamp player input movement
            //rb.velocity = new Vector2(rb.velocity.x + Mathf.Clamp((dirX * moveSpeed * (Mathf.Clamp((stunIntensity-stunAmount - stunHard)/(stunIntensity - stunHard), 0f, stunIntensity))) - rb.velocity.x, -moveSpeed, moveSpeed), rb.velocity.y);
        }
            //if (stunAmount > stunHard) rb.velocity = new Vector2(origVelocity, rb.velocity.y);
        //}

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
            //Jump handler 
            if (Input.GetButtonDown("Jump") && IsGrounded()) //make jump up faster and feel tighter
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
            if (stunAmount < stunIntensity / 2 && IsGrounded() )
            {
                stunAmount = 0;
            }
        }
        if (stunAmount < 0 ) stunAmount = 0;
        //Debug.Log("stunAmount: " + stunAmount);
        //Debug.Log("stunEFFECT: " + Mathf.Clamp((stunIntensity - stunAmount - 0.5f) / (stunIntensity - 0.5f), 0f, stunIntensity));
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
        stunAmount = stunIntensity;
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

    private void MiniJump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce / 1);
    }
    
    public void Die()
    {
        _active = false;
        coll.enabled = false;
        cam.transform.position = cameraLock;
        cameraLockBool = true;

        GameManager gameManager = FindObjectOfType<GameManager>();

        if (gameManager != null)
        {
            gameManager.goPanel.SetActive(true);
        }

    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        Debug.LogError($"Is this happening? {col.gameObject.tag}");
        if(col.collider.CompareTag("InstantDeath"))
        {
            Die();
            MiniJump();
        }
    }
}
