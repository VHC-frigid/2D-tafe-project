using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public float delay;

    public static SceneHandler instance;

    void Start()
    {
        instance = this;
    }

    public void ChangeScene(int targetScene)
    {
        Debug.Log("Attempting to load scene: " + targetScene.ToString());
        StartCoroutine(LoadScene(targetScene));
    }

    IEnumerator LoadScene(int targetScene)
    {
        animator.SetTrigger("TransitionTrigger");
        yield return new WaitForSecondsRealtime(delay);
        Debug.Log("Loading scene");
        SceneManager.LoadScene(targetScene);
    }
}
