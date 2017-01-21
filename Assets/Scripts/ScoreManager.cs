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
        listHighScore = new List<float>(nbHighScore + 1);
        scoreText.text = "Score : " + score;
        
        for(int i = 0; i < nbHighScore; i++)
        {
            listHighScore[i] = PlayerPrefs.GetFloat("HighScore"+(i+1) , 0);
        }
	}
    ILeaderboard leaderBoard = Social.CreateLeaderboard();
    public string LeaderBoardId = "TheWeaveLeaderboard";
    public float score;
    public List<float> listHighScore;
    public Text scoreText;
    private int nbHighScore = 10;

    private void Start()
    {
        leaderBoard.id = LeaderBoardId;
        leaderBoard.LoadScores(result =>
        {
            Debug.Log("Received " + leaderBoard.scores.Length + " scores");
            foreach (IScore score in leaderBoard.scores)
                Debug.Log(score);
        });
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

    private void SaveScores()
    {
        for(int i = 0; i < nbHighScore; i++)
        {
            PlayerPrefs.SetFloat("HighScore" + i, listHighScore[i]);

        }
        /*Social.LoadScores(LeaderBoardId, scores =>
        {
            addScoreInLeaderBoard(new CustomScore());
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
