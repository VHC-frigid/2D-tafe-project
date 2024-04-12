using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicMovement : MonoBehaviour
{
    [SerializeField] private Vector3 deltaPosition;
    [SerializeField] private float speed;

    private Rigidbody2D rb;

    private Vector3 _startPos;
    private Vector3 _endPos;

    private Vector3 targetPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        _startPos = transform.position;
        _endPos = transform.position + deltaPosition;
        targetPosition = _endPos;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            // stupid way of doing it but im lazy and it works
            targetPosition = targetPosition == _endPos ? _startPos : _endPos;
        }
    }
}
