using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(ParticleSystem))]
//var emission = new ParticleSystem();

public class DustVisiblity : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    private ParticleSystem.EmissionModule emission;

    // Start is called before the first frame update
    void Start()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        
        emission = ps.emission;
        emission.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        Visibility(player);
    }

    

    private void Visibility(PlayerMovement player)
    {
        if (player.IsGrounded()) //&& emission.enabled == false
        {
            // enable emission on ground
            emission.enabled = true;
            Debug.Log("grounded and emission off");

        }
        else //if(emission.enabled == true)
        {
            // disable emission in air
            emission.enabled = false;
            Debug.Log("not grounded and emission on");
        }
    }
}
