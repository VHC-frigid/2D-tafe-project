using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;

    public float timer;
    public Transform player;

    public Text subtitle;
    public GameObject goPanel;

    bool gameOver;

    public float boostTimer;
    public bool Die;

    // Start is called before the first frame update
    void Start()
    {
        gm = this;
        gameOver = false;
        goPanel.SetActive (false);
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {

        if (goPanel)
        {
            //game over
            Debug.Log("Gameover");
            gameOver = true;
            goPanel.SetActive(true);
            Time.timeScale = 0;

            if(score > PlayerPrefs.GetInt("highscore", 0))
            {
                subtitle.text = "Congrats!";
                PlayerPrefs.SetInt("highscore", score);
            }
            else
            {
                subtitle.text = "Skill issue >:)";
            }

            goScore.text = score.ToString();
            goHighScore.text = PlayerPrefs.GetInt("highscore",0).ToString();

        }
        else if(!gameOver)
        {
            timer -= Time.deltaTime;
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
