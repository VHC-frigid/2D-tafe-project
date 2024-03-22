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

    }

}
