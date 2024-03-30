using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    PointEffector2D explosionComponent;
    [SerializeField] CircleCollider2D circleCollider;

    [SerializeField] private float addTorqueAmountInDegrees;

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

            for(int i = 0; i< colliders.Length; i++)
            {
                Rigidbody2D rigid = colliders[i].GetComponent<Rigidbody2D>();
                rigid.AddTorque(addTorqueAmountInDegrees * Mathf.Deg2Rad * rigid.inertia);
            }

            Invoke("DestroyBombObject", 0.1f);       
        
        } 
    }

    
    void DestroyBombObject()
    {
        Destroy(this.gameObject);
    }

}
