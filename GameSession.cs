using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSession : MonoBehaviour
{
    // Configuration parameters
    [Range(0.1f, 10f)] [SerializeField] float gameSpeed = 1f;
    [SerializeField] int pointsPerBlockDestroyed = 50;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] bool isAutoPlayEnabled;

    // State variables
    [SerializeField] int currentScore = 0;

    // Determins if there is more than one GameSession object in the heiarchy and if 
    // so destroys the new one leaving the original one to keep the current score.
    public void Awake()
    {
        int gameStatusCount = FindObjectsOfType<GameSession>().Length;
        if(gameStatusCount > 1)
        {
            // Line used to prevent any bugs from ocuring before object is destroyed.
            gameObject.SetActive(false);    
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Displays the current score at the start of the game.
    public void Start()
    {
        DisplayCurrentScore();
    }

    // Controls the ball speed.
    void Update()
    {
        Time.timeScale = gameSpeed; 
    }

    // Adds points for the block destoryed to the players total score.
    public void AddToScore()
    {
        currentScore += pointsPerBlockDestroyed;
        DisplayCurrentScore();
    }

    // Displays the plays current score in the scoreText field.
    private void DisplayCurrentScore()
    {
        scoreText.text = currentScore.ToString();
    }

    // Destroys the GameSession object.
    public void ResetGame()
    {
        Destroy(gameObject);
    }

    // Allows developers to allow the game to be playes using a bool in the Unity editor.
    public bool IsAutoPlayEnabled()
    {
        return isAutoPlayEnabled;
    }
}
