using System;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject GameScreen;
    [SerializeField] private GameObject EndScreen;

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
    public void GameStarted()
    {
        score.color = Color.black;
        EndScreen.SetActive(false);
        GameScreen.SetActive(true);
    }
    public void GameEnded()
    {
        GameScreen.SetActive(false);
        EndScreen.SetActive(true);
        SetEndScreen();
    }
}
