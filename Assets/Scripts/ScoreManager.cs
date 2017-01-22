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
        listHighScore = new List<float>();
        scoreText.text = "Score : " + score;
        
        for(int i = 0; i < nbHighScore; i++)
        {
            listHighScore.Add(0);

            if (PlayerPrefs.HasKey("HighScore" + (i + 1)))
            {
                listHighScore[i] = PlayerPrefs.GetFloat("HighScore" + (i + 1), 0);
            }
            else
            {
                listHighScore[i] = 0;
                PlayerPrefs.SetFloat("HighScore" + (i + 1), 0);
            }
        }
	}
    ILeaderboard leaderBoard;
    public string LeaderBoardId = "TheWeaveLeaderboard";
    public float score;
    public List<float> listHighScore;
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
        for(int i = nbHighScore - 1 ; i > 0; i--)
        {
            if(listHighScore[i] > score)
            {
                listHighScore.Insert(i + 1, score);
            }
        }
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
