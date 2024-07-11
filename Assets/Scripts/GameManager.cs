using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public float timeLimit = 30f;
    public int lives = 3;

    public TextMeshProUGUI scoreLabel;
    public GameObject virtualCamera;
    public GameObject resultPopup;
    public Player player;
    public Lives lifeDisplayer;

    public bool isCleared;


    void Awake()
    {
        instance = this;
    }


    void Start()
    {
        lifeDisplayer.SetLives(lives);

        isCleared = false;
    }


    void Update()
    {
        timeLimit -= Time.deltaTime;

        scoreLabel.text = "Time Left " + ((int)timeLimit).ToString();
    }


    public void Addtime(float time)
    {
        timeLimit += time;
    }


    public void Die()
    {
        virtualCamera.SetActive(false);

        lives--;

        lifeDisplayer.SetLives(lives);

        Invoke("Restart", 1);
    }


    public void Restart()
    {
        if (lives == 0)
        {
            GameOver();
        }
        else
        {
            virtualCamera.SetActive(true);
            player.Restart();
        }
    }


    public void StageClear()
    {
        isCleared = true;

        resultPopup.SetActive(true);
    }


    public void GameOver()
    {
        isCleared = false;

        resultPopup.SetActive(true);
    }
}
