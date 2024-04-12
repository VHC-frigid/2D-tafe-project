using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Leveltransition : MonoBehaviour
{
    [SerializeField] private float delay;

    public int sceneBuildIndex;

    private void OnTriggerEnter2D(Collider2D other)
    {
        print("Trigger Entered");

        if(other.tag == "Player")
        {
            print("Switching Scene to " + sceneBuildIndex);

            SceneHandler.instance.ChangeScene(sceneBuildIndex);
        }
    }
}
