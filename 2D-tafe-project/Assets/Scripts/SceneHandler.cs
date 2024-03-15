using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public static SceneHandler sh;

    void Start()
    {
        sh = this;
    }

    public void ChangeScene(string targetScene)
    {
        SceneManager.LoadSceneAsync(targetScene);
    }
}
