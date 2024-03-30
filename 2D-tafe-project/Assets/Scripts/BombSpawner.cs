using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : MonoBehaviour
{


    [SerializeField] public GameObject bombPrefab; // Bomb prefab used to spawn new bombs
    Vector3 playerPos; // For storing the player position

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check conditions for spawning a bomb prefab
        // This is where you would add additional conditions such as a cooldown or ammo counter
        // Currently the player can spawn infinite bombs
        if (Input.GetButtonDown("SpawnBomb"))
        {
            spawnBomb();
        }
    }

    // spawns a new bomb prefab at player position
    public void spawnBomb()
    {

        // Stores the player position
        playerPos = transform.position;
        // Spawns the bomb at player position
        Instantiate(bombPrefab, playerPos, Quaternion.identity);
    }

}
