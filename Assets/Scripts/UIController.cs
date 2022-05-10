using System;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject MainScreen;
    [SerializeField] private GameObject GameScreen;
    [SerializeField] private GameObject PauseScreen;
    [SerializeField] private GameObject RestartScreen;

    [SerializeField] private Text score;
    [SerializeField] private Text endScore;
    [SerializeField] private string distanceType = "m";
    private float currentScore;
    private float bestRunScore;
    private float topOfAllTimeScore;

    public void SetScore(float amount)
    {
        currentScore += amount;
        if(bestRunScore < currentScore)
        {
            bestRunScore = currentScore;
            score.color = Color.green;
        }
        if (topOfAllTimeScore < bestRunScore)
        {
            score.color = Color.blue;
        }
        score.text = "Score: " + currentScore.ToString("0.00") + distanceType + "\nBest Score: " + bestRunScore.ToString("0.00") + distanceType;
    }
    public void SetResetScore(float amount)
    {
        currentScore = amount;
        score.text = "Score: " + "0m" + "\nBest Score: " + bestRunScore.ToString("0.00") + distanceType;
    }
    public void SetEndScreen()
    {
        if (topOfAllTimeScore < bestRunScore) topOfAllTimeScore = bestRunScore;
        endScore.text = "Best Run Score: " + bestRunScore.ToString("0.00") + distanceType + "\nAll Time Top Score: " + topOfAllTimeScore.ToString("0.00") + distanceType;
    }
    public void BackToMainMenu()
    {
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
        ActivateCanvas(RestartScreen);
        SetEndScreen();
    }

    private void ActivateCanvas(GameObject chosenCanvas)
    {
        GameScreen.SetActive(false);
        RestartScreen.SetActive(false);
        MainScreen.SetActive(false);
        PauseScreen.SetActive(false);

        chosenCanvas.SetActive(true);
    }
}
