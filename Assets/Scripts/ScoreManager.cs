using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

    public float score;
    public List<float> listHighScore;
    private int nbHighScore = 10;

	// Use this for initialization
	void Awake () {
        score = 0;
        listHighScore = new List<float>(nbHighScore + 1);
        
        for(int i = 0; i < nbHighScore; i++)
        {
            listHighScore[i] = PlayerPrefs.GetFloat("HighScore"+(i+1) , 0);
        }
	}

    public void AddScore(float _points)
    {
        score += _points;
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
