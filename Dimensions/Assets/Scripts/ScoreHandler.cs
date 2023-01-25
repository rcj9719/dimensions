using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

public class ScoreHandler : MonoBehaviour
{
    int score = 0;
    int wallCnt = 0;

    int highScore = 0;
    int maxWalls = 0;

    [SerializeField] TextMeshProUGUI GameOverScoreText;
    [SerializeField] TextMeshProUGUI GameOverWallsText;

    [SerializeField] TextMeshProUGUI GameOverHighScoreText;
    [SerializeField] TextMeshProUGUI GameOverMaxWallsText;

    [SerializeField] Button playAgainBtn;

    private void Awake()
    {
        highScore = 0;
        wallCnt = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("Score")) score = PlayerPrefs.GetInt("Score");
        if (PlayerPrefs.HasKey("Walls")) wallCnt = PlayerPrefs.GetInt("Walls");
        if (PlayerPrefs.HasKey("HighScore")) highScore = PlayerPrefs.GetInt("HighScore");
        if (PlayerPrefs.HasKey("MaxWalls")) maxWalls = PlayerPrefs.GetInt("MaxWalls");

        Debug.Log("[GameOver] score:" + score);
        Debug.Log("[GameOver] highscore:" + highScore);
        if (score > highScore)
        {
            highScore = score;
        }

        if (wallCnt > maxWalls)
        {
            maxWalls = wallCnt;
        }

        GameOverScoreText.text = "Score: " + score;
        GameOverWallsText.text = "Walls: " + wallCnt;
        GameOverHighScoreText.text = "High Score: " + highScore;
        GameOverMaxWallsText.text = "Max Walls: " + maxWalls;

        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.SetInt("MaxWalls", maxWalls);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("GamePlay");
    }

    public void ResetScores()
    {
        PlayerPrefs.DeleteAll();
        GameOverHighScoreText.text = "High Score: " + 0;
        GameOverMaxWallsText.text = "Max Walls: " + 0;
        highScore = 0;
        wallCnt = 0;
    }

    public void loadStartMenu()
    {
        SceneManager.LoadScene("GameStart");
    }
}
