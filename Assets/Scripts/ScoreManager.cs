using System.Collections;
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

    }
    

    private void SaveScores()
    {
        LeaderboardManager.instance.SubmitScore((long)score);
    }
    
}
