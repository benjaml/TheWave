using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LeaderboardManager : MonoBehaviour
{

    public static LeaderboardManager instance = null;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
            instance = this;
        if (instance != this)
            Destroy(gameObject);
    }
    bool offline = false;
    List<float> HighscoreList = new List<float>(5);

    [Header("Connection")]
    public GameObject start;
    public Text userNameInput;
    public string defaultPassword;

    [Header("Highscore Display")]
    public GameObject end;
    public int ScoreCount;
    public Text outputDataUsername;
    public Text outputDataScores;

    void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "EndScreen")
        {
            end.SetActive(true);
            GetLeaderboard();
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*// TODO: remove test input
        if (Input.GetKeyDown(KeyCode.A))
        {
            SubmitScore(Random.Range(0,9999));
        }
        if (Input.GetKeyDown(KeyCode.P))
            GetLeaderboard();*/
    }

    public void SubmitScore(long score)
    {
        if (offline)
        {
            // updateList
            int listHighscoreIndex = 0;
            for (int i = 0; i < 5; i++)
            {
                float nextHighScore = PlayerPrefs.GetFloat("Highscore" + listHighscoreIndex);
                if (score > nextHighScore)
                    HighscoreList[i] = score;
                else
                {
                    HighscoreList[i] = nextHighScore;
                    listHighscoreIndex++;
                }
            }
        }

        new GameSparks.Api.Requests.LogEventRequest().SetEventKey("SUBMIT_SCORE").SetEventAttribute("SCORE", score.ToString()).Send((response) =>
        {
            if (!response.HasErrors)
            {
                Debug.Log("Score Posted Successfully...");
            }
            else
            {
                Debug.Log("Error Posting Score...");
            }
        });
    }

    public void PlayOffline()
    {
        offline = true;
        start.SetActive(false);
        SceneManager.LoadScene(1);

    }
    void GetLeaderboard()
    {
        if (offline)
        {
            outputDataUsername.text += "ME" + "\n"; // add the username to the output username text
            outputDataScores.text += "" + PlayerPrefs.GetFloat("Highscore") + "\n"; // add the score to the output score text
            for (int i = 0; i < 5; i++)
            {

                outputDataScores.text += "" + HighscoreList[i] + "\n"; // add the score to the output score text
                PlayerPrefs.SetFloat("Highscore" + i, HighscoreList[i]);
            }
        }

        Debug.Log("Fetching Leaderboard Data ...");
        new GameSparks.Api.Requests.LeaderboardDataRequest()
            .SetLeaderboardShortCode("SCORE_LEADERBOARD")
            .SetEntryCount(ScoreCount) // we need to parse this text input, since the entry count only take long
            .Send((response) =>
            {
                if (!response.HasErrors)
                {
                    Debug.Log("Found Leaderboard Data ...");
                    outputDataScores.text = System.String.Empty; // first clear all data from the output
                    outputDataUsername.text = System.String.Empty; // first clear all data from the output
                    foreach (GameSparks.Api.Responses.LeaderboardDataResponse._LeaderboardData entry in response.Data) // iterate throug the leaderboard data
                    {
                        int rank = (int)entry.Rank; // we can get the rank directly
                        string playerName = entry.UserName;
                        string score = entry.JSONData["SCORE"].ToString(); // we need to get the key, in order to get the score
                        outputDataUsername.text += "" + playerName + "\n"; // add the username to the output username text
                        outputDataScores.text += "" + score + "\n"; // add the score to the output score text
                    }
                }
            });
    }

    public void RegisterPlayerBttn()
    {
        Debug.Log("Registering Player...");
        new GameSparks.Api.Requests.RegistrationRequest()
            .SetDisplayName(userNameInput.text)
            .SetUserName(userNameInput.text)
            .SetPassword(defaultPassword)
            .Send((response) =>
            {

                if (!response.HasErrors)
                {
                    Debug.Log("Player Registered \n User Name: " + response.DisplayName);
                }
                else
                {
                    Debug.Log("Error Registering Player... \n " + response.Errors.JSON.ToString());
                }

            });

    }

    public void connect()
    {
        RegisterPlayerBttn();
        connectUser();
        start.SetActive(false);
        SceneManager.LoadScene(1);
    }
    private void connectUser()
    {
        new GameSparks.Api.Requests.AuthenticationRequest().SetUserName(userNameInput.text).SetPassword(defaultPassword).Send((AR) =>
        {

            if (!AR.HasErrors)
            {
                Debug.Log("Worked");
            }
            else
            {
                Debug.Log("Didnt Work");
            }
        });
    }
}
