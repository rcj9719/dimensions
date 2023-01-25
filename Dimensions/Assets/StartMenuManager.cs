using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenuManager : MonoBehaviour
{
    int highScore = 0;
    int maxWalls = 0;

    [SerializeField] TextMeshProUGUI StartHighScoreText;
    [SerializeField] TextMeshProUGUI StartMaxWallsText;

    [SerializeField] Button playBtn;


    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("HighScore")) highScore = PlayerPrefs.GetInt("HighScore");
        if (PlayerPrefs.HasKey("MaxWalls")) maxWalls = PlayerPrefs.GetInt("MaxWalls");

        StartHighScoreText.text = "High Score: " + highScore;
        StartMaxWallsText.text = "Max Walls: " + maxWalls;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GamePlay");
    }

    public void onPlayerColorChange()
    {
        PlayerPrefs.SetString("PlayerColor", "yellow");
    }

    public void exitGame() { 
        Application.Quit();
    }
}
