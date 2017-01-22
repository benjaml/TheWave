﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public static ScoreManager instance = null;

	// Use this for initialization
	void Awake () {
        if (instance == null)
            instance = this;
        if (instance != this)
            Destroy(gameObject);
        score = 0;
        scoreText.text = "Score : " + score;
	}
    ILeaderboard leaderBoard;
    public string LeaderBoardId = "TheWeaveLeaderboard";
    public float score;
    public Text scoreText;
    private int nbHighScore = 10;

    private void Start()
    {
    }

    // This function gets called when Authenticate completes
    // Note that if the operation is successful, Social.localUser will contain data from the server.
    private void ProcessAuthentication(bool success )
    {
        if (success)
            Debug.Log("Authenticated");
        else
            Debug.Log("Failed to authenticate");
    }

    public void AddScore(float _points)
    {
        score += _points;
        scoreText.text = "Score : " + score;
    }

    public void EndOfGame()
    {
        SaveScores();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            SaveScores();
        if (Input.GetKeyDown(KeyCode.Z))
            DisplayLeaderboard();
    }

    private void DisplayLeaderboard()
    {
        leaderBoard.LoadScores(result =>
        {
            Debug.Log("Received " + leaderBoard.scores.Length + " scores");
            foreach (IScore score in leaderBoard.scores)
                Debug.Log(score);
        });
    }

    private void SaveScores()
    {
        Social.ReportScore(Random.Range(0,10), LeaderBoardId, success =>
        {
            Debug.Log(success ? "Reported score successfully" : "Failed to report score");
        });
        /*for (int i = 0; i < nbHighScore; i++)
        {
            PlayerPrefs.SetFloat("HighScore" + i, listHighScore[i]);

        }
        Social.LoadScores(LeaderBoardId, scores =>
        {
            addScoreInLeaderBoard();
        });*/
    }

    private IScore[] addScoreInLeaderBoard(IScore lastScore, IScore[] highScores)
    {
        if (highScores.Length < 10)
            if (lastScore.value < highScores[highScores.Length - 1].value)
            {
                highScores[highScores.Length] = lastScore;
                return highScores;
            }
        int index = 0;
        IScore[] ret = new IScore[10];
        foreach(IScore score in highScores)
        {
            if (lastScore.value > score.value)
            {
                ret[index] = lastScore;
            }
            else
            {
                ret[index] = score;
            }
            index++;
            if (index == 9)
                return ret;
        }
        return ret;
    }
}
