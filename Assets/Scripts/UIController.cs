using System;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject GameScreen;
    [SerializeField] private GameObject EndScreen;

    [SerializeField] private Text score;
    [SerializeField] private Text endScore;
    public string distanceType = "m";
    private float currentScore;
    private float bestOfRunScore;
    private float topOfAllTimeScore;

    public void SetScore(float amount)
    {
        currentScore += amount;
        if(bestOfRunScore < currentScore) bestOfRunScore = currentScore;
        score.text = "Score: " + currentScore.ToString("0.00") + distanceType + "\nTop Score: " + bestOfRunScore.ToString("0.00") + distanceType;
    }
    public void SetResetScore(float amount)
    {
        currentScore = amount;
        score.text = "Score: " + currentScore.ToString("0.00") + distanceType + "\nTop Score: " + bestOfRunScore.ToString("0.00") + distanceType;
    }
    public void SetEndScreen()
    {
        if (topOfAllTimeScore < bestOfRunScore) topOfAllTimeScore = bestOfRunScore;
        endScore.text = "Best Of Run Score: " + bestOfRunScore.ToString("0.00") + distanceType + "\nAll Time Top Score: " + topOfAllTimeScore.ToString("0.00") + distanceType;
    }
    public void GameStarted()
    {
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
