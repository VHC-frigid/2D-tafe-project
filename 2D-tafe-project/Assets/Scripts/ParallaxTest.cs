using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxTest : MonoBehaviour
{
    [SerializeField] private GameObject objectToFollow;
    [SerializeField] private Vector2 scrollFactor;

    void Update()
    {
        Vector3 followPosition = objectToFollow.transform.position;
        transform.position = new Vector3(followPosition.x * scrollFactor.x, followPosition.y * scrollFactor.y, 0);
    }
}
