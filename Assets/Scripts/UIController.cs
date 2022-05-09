using System;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text score;
    public string distanceType = "m";
    private float currentScore;
    private float topScore;

    public void SetScore(float amount)
    {
        currentScore += amount;
        if(topScore < currentScore) topScore = currentScore;
        score.text = "Score: " + currentScore.ToString("0.00") + distanceType + "\nTop Score: " + topScore.ToString("0.00") + distanceType;
    }
    public void ResetScore(float amount)
    {
        currentScore = amount;
        score.text = "Score: " + currentScore.ToString("0.00") + distanceType + "\nTop Score: " + topScore.ToString("0.00") + distanceType;
    }
}
