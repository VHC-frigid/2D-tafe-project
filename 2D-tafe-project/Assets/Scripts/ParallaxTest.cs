using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxTest : MonoBehaviour
{
    [SerializeField] private GameObject objectToFollow;
    [SerializeField] private Vector2 scrollFactor;
    [SerializeField] private float xOffset = 0;
    [SerializeField] private float yOffset = 0;

    void Update()
    {
        Vector3 followPosition = objectToFollow.transform.position;
        transform.position = new Vector3(xOffset + (followPosition.x * scrollFactor.x), yOffset + (followPosition.y * scrollFactor.y), 0);
    }
}
