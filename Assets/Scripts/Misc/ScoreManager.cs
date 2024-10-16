using UnityEngine;
using TMPro;
//Rama's Code
//This script manages the players score and updates the UI

public class ScoreManager : MonoBehaviour
{
    //Singleton instance, so this class can be easily accessed from anywhere
    public static ScoreManager Instance;

    //Current score
    private int score = 0;

    //TMP UI element that displays the score
    public TextMeshProUGUI scoreText;

    private void Awake()
    {
        //If there is no current instance set this one as the singleton instance
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            //If another instance of ScoreManager already exists destroy this one
            Destroy(gameObject);
        }

        //Shows the initial score which should be 0
        UpdateScoreUI();
    }

    //Method to add points to the score
    public void AddScore(int points)
    {   //Increment the score by the number of points passed to this method
        score += points;

        //Update the score UI to the new score
        UpdateScoreUI();
    }

    //Method to update the TMP UI with the current score
    private void UpdateScoreUI()
    {   
        //Check if the scoreText asset is assigned to avoid null reference errors
        if (scoreText != null)
        {   
            //Update the text asset to show the current score
            scoreText.text = "Torture Points: " + score.ToString();
        }
    }
}


