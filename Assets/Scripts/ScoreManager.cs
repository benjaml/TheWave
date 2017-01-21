using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public float score;
    public List<float> listHighScore;
    public Text scoreText;
    private int nbHighScore = 10;

	// Use this for initialization
	void Awake () {
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
        listHighScore.Add(0);
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
    }
}
