using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ResultPopup : MonoBehaviour
{

    public TextMeshProUGUI titleLabel;
    public TextMeshProUGUI scoreLabel;
    //public GameObject highScoreLabel;

    public GameObject highScorePopup;



    void OnEnable()
    {

        Time.timeScale = 0;

        if (GameManager.instance.isCleared)
        {
            //HighScore();

            SaveHighScore();

            titleLabel.text = "Clear!";
            scoreLabel.text = GameManager.instance.timeLimit.ToString("#.##");
        }
        else
        {
            titleLabel.text = "GameOver";
            scoreLabel.text = "";
        }

    }


    public void PlayAgainPressed()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene("GameScene");
    }


    public void HighScorePressed()
    {
        highScorePopup.SetActive(true);
    }


    void SaveHighScore()
    {
        float score = GameManager.instance.timeLimit;
        string currentScoreString = score.ToString("#.###");

        string savedScoreString = PlayerPrefs.GetString("HighScores", "");

        if (savedScoreString == "")
        {
            PlayerPrefs.SetString("HighScores", currentScoreString);
        }
        else
        {
            string[] scoreArray = savedScoreString.Split(",");
            List<string> scoreList = new List<string>(scoreArray);

            for (int i = 0; i < scoreList.Count; i++)
            {
                float savedScore = float.Parse(scoreList[i]);

                if (savedScore < score)
                {
                    scoreList.Insert(i, currentScoreString);
                    break;
                }
            }

            if (scoreArray.Length == scoreList.Count)
            {
                scoreList.Add(currentScoreString);
            }

            if (scoreList.Count > 10)
            {
                scoreList.RemoveAt(10);
            }

            string result = string.Join(",", scoreList);

            PlayerPrefs.SetString("HighScores", result);
        }

        PlayerPrefs.Save();
    }



    /*
    void HighScore()
    {
        float highScore = PlayerPrefs.GetFloat("HighScore", 0);

        if (highScore < GameManager.instance.timeLimit)
        {
            highScoreLabel.SetActive(true);

            PlayerPrefs.SetFloat("HighScore", GameManager.instance.timeLimit);
            PlayerPrefs.Save();
        }
        else
        {
            highScoreLabel.SetActive(false);
        }
    }
    */
}
