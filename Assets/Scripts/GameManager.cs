using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Snake sn;
    public Food food;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;

    private int score;
    private int highscore;

    private void Start()
    {
        highscore = PlayerPrefs.GetInt("highscore", 0);
        highscoreText.text = highscore.ToString();
    }

    public void RestartGame()
    {
        for(int i = 1; i < sn.segments.Count; i++)
        {
            Destroy(sn.segments[i]);
        }

        sn.segments.RemoveRange(1, sn.segments.Count - 1);
        sn.transform.position = Vector2.zero;

        food.RandomFoodPosition();
        sn.SetRandomDirection();

        score = 0;
        scoreText.text = score.ToString();

        SaveHighScore();
    }

    public void IncreaseScore(int points)
    {
        score = score + points;
        scoreText.text = score.ToString();

        if(score > highscore)
        {
            highscore =  score;
            highscoreText.text = highscore.ToString();
        }
    }

    private void SaveHighScore()
    {
        if(highscore > PlayerPrefs.GetInt("highscore", 0))
        {
            PlayerPrefs.SetInt("highscore", highscore);
        }
        
    }
}
