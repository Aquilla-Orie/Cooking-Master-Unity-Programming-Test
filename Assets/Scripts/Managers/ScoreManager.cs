using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int AddPlayerScore(int playerScore, int scoreToAdd)
    {
        int newScore = playerScore + scoreToAdd;
        return newScore;
    }
    public int DeductPlayerScore(int playerScore, int scoreToAdd)
    {
        int newScore = playerScore - scoreToAdd;
        if (newScore <= 0) newScore = 0;
        return newScore;
    }

}
