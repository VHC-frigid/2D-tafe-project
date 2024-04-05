using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    PointEffector2D explosionComponent;
    [SerializeField] CircleCollider2D circleCollider;

    [SerializeField] private float addTorqueAmountInDegrees; //may be unused now
    [SerializeField] private float blastForce = 100f;

    private float explosionRadius;
    // Start is called before the first frame update
    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        explosionComponent = GetComponent<PointEffector2D>();
        
        // This is so our bomb oesn't go off immediately
        explosionComponent.enabled = false;
        explosionRadius = circleCollider.radius;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            explosionComponent.enabled = true;

            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

            for (int i = 0; i< colliders.Length; i++)
            {
                //Apply stun to player
                PlayerMovement rigid2 = colliders[i].GetComponent<PlayerMovement>();
                if (rigid2 != null)
                {
                    rigid2.StunLock();
                    //Debug.Log("STUNNED");
                }
                // Calculate direction from bomb to player/collider
                Vector2 direction = collision.collider.transform.position - transform.position;
                direction.Normalize();
                // Log direction vector for debugging
                //Debug.Log("Direction Vector: " + direction);
                //Debug.DrawRay(transform.position, direction * 5f, Color.red, 1f); // Adjust length and color as needed

                Rigidbody2D rigid = colliders[i].GetComponent<Rigidbody2D>();
                //rigid.AddTorque(addTorqueAmountInDegrees * Mathf.Deg2Rad * rigid.inertia); breaks directional force

                
                rigid.AddForce(direction * blastForce + Vector2.down, ForceMode2D.Impulse); // Vector2.down is to make the horizontal force more apparent
                //Debug.Log("Force Applied: " + (direction * blastForce));

            }
            DestroyBombObject();
            //Invoke("DestroyBombObject", 0.1f);       
        
        } 
    }

    
    void DestroyBombObject()
    {
        Destroy(this.gameObject);
    }

}
