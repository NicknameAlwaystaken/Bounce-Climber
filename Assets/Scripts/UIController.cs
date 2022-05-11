using System;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject MainScreen;
    [SerializeField] private GameObject GameScreen;
    [SerializeField] private GameObject PauseScreen;
    [SerializeField] private GameObject RestartScreen;

    [SerializeField] private Image MoveStick;
    [SerializeField] private Image JumpButton;

    [SerializeField] private Text score;
    [SerializeField] private Text endScore;
    [SerializeField] private Text gameTitle;
    [SerializeField] private Text currentTop;
    [SerializeField] private string distanceType = "m";
    private float currentScore;
    private float bestRunScore;
    private float topScore;

    private void Awake()
    {
        MoveStick.enabled = true;
        JumpButton.enabled = true;
        /*
        if (Application.platform == RuntimePlatform.Android)
        {
            Stick.enabled = true;
            Button.enabled = true;
        }
        else
        {
            Stick.enabled = false;
            Button.enabled = false;
        }
        */
        LoadGame();
    }
    public class TextForScore
    {

        private TextForScore(string value) { Value = value; }

        public string Value { get; private set; }

        public static TextForScore None { get { return new TextForScore(""); } }
        public static TextForScore BestRun { get { return new TextForScore("NEW BEST IN RUN!"); } }
        public static TextForScore AllTimeBest { get { return new TextForScore("NEW ALL TIME BEST!!!"); } }
    }

    public void SetScore(float amount)
    {
        currentScore += amount;
        string milestone = TextForScore.None.Value;
        if(currentScore > bestRunScore)
        {
            bestRunScore = currentScore;
            score.color = Color.green;
            milestone = TextForScore.BestRun.Value;
            if(currentScore > topScore)
            {
                topScore = currentScore;
                score.color = Color.blue;
                milestone = TextForScore.AllTimeBest.Value;
            }
        }
        ScoreText(currentScore, milestone);
    }

    private void ScoreText(float newScore, string scoreText)
    {
        score.text = "Score: " + newScore.ToString("0.00") + distanceType + "\n" + scoreText;
    }
    private void EndText(float newScore, float newBestScore)
    {
        endScore.text = "Best Run Score: " + newScore.ToString("0.00") + distanceType + "\nAll Time Top Score: " + newBestScore.ToString("0.00") + distanceType;
    }

    public void SetGameTitle(string title, string version)
    {
        gameTitle.text = title + "\n " + version;
    }
    public void SetResetScore(float amount)
    {
        currentScore = amount;
        ScoreText(currentScore, TextForScore.None.Value);
    }
    public void SetEndScreen()
    {
        if (topScore < bestRunScore)
        {
            currentScore = 0f;
            topScore = bestRunScore;
            SaveTopScore(topScore);
        }
        EndText(bestRunScore, topScore);
    }
    public void BackToMainMenu()
    {
        EndSession();
        score.color = Color.black;
        ActivateCanvas(MainScreen);
    }
    public void GameStarted()
    {
        score.color = Color.black;
        ActivateCanvas(GameScreen);
    }
    public void GamePaused()
    {
        ActivateCanvas(PauseScreen);
    }
    public void GameResumed()
    {
        ActivateCanvas(GameScreen);
    }
    public void PlayerDied()
    {
        SetEndScreen();
        ActivateCanvas(RestartScreen);
    }

    private void ActivateCanvas(GameObject chosenCanvas)
    {
        GameScreen.SetActive(false);
        RestartScreen.SetActive(false);
        MainScreen.SetActive(false);
        PauseScreen.SetActive(false);

        chosenCanvas.SetActive(true);
    }
    public void SaveTopScore(float amount)
    {
        if (PlayerPrefs.HasKey("TopScore") && PlayerPrefs.GetFloat("TopScore") < topScore)
        {
            PlayerPrefs.SetFloat("TopScore", amount);
            PlayerPrefs.Save();
            currentTop.text = "All Time Best Score\n" + amount.ToString("0.00") + "m";
        }
        else
        {
            currentTop.text = "All Time Best Score\n" + topScore.ToString("0.00") + "m";
        }
    }
    private void LoadGame()
    {
        if(PlayerPrefs.HasKey("TopScore"))
            topScore = PlayerPrefs.GetFloat("TopScore");
        currentTop.text = "All Time Best Score\n" + topScore.ToString("0.00") + "m";

    }
    public void ResetAllScores()
    {
        currentScore = 0f;
        ScoreText(currentScore, TextForScore.None.Value);
        bestRunScore = 0f;
    }
    public void EndSession()
    {
        SaveTopScore(topScore);
        currentScore = 0f;
        bestRunScore = 0f;
    }
}
