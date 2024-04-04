using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : MonoBehaviour
{


    [SerializeField] public GameObject bombPrefab; // Bomb prefab used to spawn new bombs
    Vector3 playerPos; // For storing the player position
    [SerializeField] public float bombSpawnPosition; // For storing the bomb spawn position
    PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
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
        Vector3 bombSpawnCoordinates;

        // Stores the player position
        playerPos = transform.position;
        // Sets bomb spawn coordinates to the correct side of player based on player orientation
        if (playerMovement.PlayerOrientationLeft())
        {
            bombSpawnCoordinates = new Vector3(bombSpawnPosition,-0.3f, 0f);
        } else
        {
            bombSpawnCoordinates = new Vector3(-bombSpawnPosition, -0.3f, 0f);
        }
        // Spawns the bomb at player position infront of where they're standing
        Instantiate(bombPrefab, playerPos + bombSpawnCoordinates, Quaternion.identity);
    }

}
