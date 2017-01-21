using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    public float score;
    public List<float> listHighScore;
    public Text scoreText;
    private int nbHighScore = 10;

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
    }
}
