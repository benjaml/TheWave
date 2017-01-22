using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LeaderboardManager : MonoBehaviour {

    public static LeaderboardManager instance = null;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
            instance = this;
        if (instance != this)
            Destroy(gameObject);
    }

    [Header("Connection")]
    public GameObject start;
    public Text userNameInput;
    public string defaultPassword;

    [Header("Highscore Display")]
    public GameObject end;
    public Text outputData;
    public Text entryCount;
    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SubmitScore(1234);
        }
        if (Input.GetKeyDown(KeyCode.P))
            GetLeaderboard();
    }

    public void SubmitScore(long score)
    {
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

    void GetLeaderboard()
    {
        Debug.Log("Fetching Leaderboard Data ...");
        new GameSparks.Api.Requests.LeaderboardDataRequest()
            .SetLeaderboardShortCode("SCORE_LEADERBOARD")
            .SetEntryCount(int.Parse(entryCount.text)) // we need to parse this text input, since the entry count only take long
            .Send((response) =>
            {
                if(!response.HasErrors)
                {
                    Debug.Log("Found Leaderboard Data ...");
                    outputData.text = System.String.Empty; // first clear all data from the output
                    foreach(GameSparks.Api.Responses.LeaderboardDataResponse._LeaderboardData entry in response.Data) // iterate throug the leaderboard data
                    {
                        int rank = (int)entry.Rank; // we can get the rank directly
                        string playerName = entry.UserName;
                        string score = entry.JSONData["SCORE"].ToString(); // we need to get the key, in order to get the score
                        outputData.text += rank + "\t Name: " + playerName + "\t Score" + score + "\n"; // add the score to the output text
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
                    throw new System.Exception();
                    Debug.Log("Error Registering Player... \n " + response.Errors.JSON.ToString());
                }

            });

    }

    public void connect()
    {
        try
        {
            RegisterPlayerBttn();
        }
        catch (System.Exception registerException)
        {
            // maybe the user is already register ?
            try
            {
                connectUser();
            }
            catch (System.Exception connectionException)
            {
                Debug.Log("Connection failed, maybe try in offline mode");
            }
        }
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
                throw new System.Exception();
                Debug.Log("Didnt Work");
            }
        });
    }
}
