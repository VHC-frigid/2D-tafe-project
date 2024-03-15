using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    [Tooltip("Name of the scene file to go to after reaching this, excluding file extension")]
    [SerializeField] private string targetScene;

    private BoxCollider2D trigger;

    void Start()
    {
        trigger = GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != "Player") Debug.LogWarning("Whatever collided wasn't player"); return;

        if (targetScene == "" || targetScene == null) Debug.LogWarning("No target scene set!"); return;

        SceneHandler.sh.ChangeScene(targetScene);
    }
}
