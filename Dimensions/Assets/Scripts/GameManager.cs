using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour    // creating a singleton
{
    int score = 0;
    int wallCnt = 0;

    //int highScore = 0;
    //int maxWalls = 0;

    int freezeCnt = 0;
    int invCnt = 0;

    bool firstWall = true;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI wallCntText;

    //[SerializeField] TextMeshProUGUI GameOverScoreText;
    //[SerializeField] TextMeshProUGUI GameOverWallsText;

    //[SerializeField] TextMeshProUGUI GameOverHighScoreText;
    //[SerializeField] TextMeshProUGUI GameOverMaxWallsText;

    //[SerializeField] Button playAgainBtn;

    [SerializeField] GameObject freezePowerUI;
    [SerializeField] GameObject invisiblePowerUI;

    [SerializeField] TextMeshProUGUI freezeCntUI;
    [SerializeField] TextMeshProUGUI invCntUI;

    public static GameManager inst;
    [SerializeField] PlayerMovement playerMovement;

    public void IncrementScore()
    {
        if (playerMovement.isAlive())
        {
            inst.score++;
            scoreText.text = "Score: " + score;
            //GameOverScoreText.text = "Score: " + score;
            //if (score > highScore)
            //{
            //    highScore = score;
            //    GameOverHighScoreText.text = "High Score: " + score;
            //}

            // Increase players speed per increase in point
            playerMovement.speed += playerMovement.speedIncreasePerPoint;
        }
    }

    public void IncrementWallCount()
    {
        if (firstWall)
        {
            firstWall = false;
            return;
        }

        if (playerMovement.isAlive())
        {
            inst.wallCnt++;
            wallCntText.text = "Walls: " + wallCnt;
            //GameOverWallsText.text = "Walls: " + wallCnt;

            //if (wallCnt > maxWalls)
            //{
            //    maxWalls = wallCnt;
            //    GameOverMaxWallsText.text = "Max Walls: " + wallCnt;
            //}
        }
    }

    public int getWallCount() {
        return wallCnt;
    }

    public void IncrementPower(PowerType powerType)
    {
        switch(powerType)
        {
            case PowerType.FREEZE:
                freezeCnt++;
                freezePowerUI.SetActive(true);
                freezeCntUI.text = freezeCnt.ToString();
                break;
            case PowerType.HIDE:
                invCnt++;
                invisiblePowerUI.SetActive(true);
                invCntUI.text = invCnt.ToString();
                break;
        }
    }

    public void playAgain()
    {
        //GameObject.FindGameObjectWithTag("PlayScreen").SetActive(true);
        //GameObject.FindGameObjectWithTag("Finish").SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Awake()
    {
        inst = this;
    }

}
